﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Roguelike.Core
{
	public class EventCollection<T> : ICollection<T>
	{
		#region Properties

		public event EventHandler<ItemEventArgs<T>> ItemAdded;

		public event EventHandler<ItemEventArgs<T>> ItemRemoved;

		public event EventHandler<CancelableItemEventArgs<T>> ItemAdding;

		public event EventHandler<CancelableItemEventArgs<T>> ItemRemoving;

		private readonly ICollection<T> _collection;

		#endregion

		#region Constructors

		public EventCollection()
			: this(Enumerable.Empty<T>())
		{ }

		public EventCollection(IEnumerable<T> items)
			: this(items.ToList())
		{ }

		public EventCollection(ICollection<T> items)
		{
			_collection = new List<T>(items);
		}

		#endregion

		#region Implementation of ICollection<T>

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public Boolean IsReadOnly
		{ get { return false; } }

		public void Add(T item)
		{
			var beforeHandler = Volatile.Read(ref ItemAdding);
			if (beforeHandler != null)
			{
				var eventArgs = new CancelableItemEventArgs<T>(item);
				beforeHandler(this, eventArgs);
				if (eventArgs.IsCanceled)
				{
					return;
				}
			}

			_collection.Add(item);

			Volatile.Read(ref ItemAdded)?.Invoke(this, new ItemEventArgs<T>(item));
		}

		public void Clear()
		{
			var itemsWhichCanNotBeRemoved = new List<T>();
			var beforeHandler = Volatile.Read(ref ItemRemoving);
			foreach (var item in this)
			{
				if (beforeHandler != null)
				{
					var eventArgs = new CancelableItemEventArgs<T>(item);
					beforeHandler(this, eventArgs);
					if (eventArgs.IsCanceled)
					{
						itemsWhichCanNotBeRemoved.Add(item);
					}
				}
			}
			if (itemsWhichCanNotBeRemoved.Count == 0)
			{
				var copy = new List<T>(this);

				_collection.Clear();

				var afterHandler = Volatile.Read(ref ItemRemoved);
				foreach (var item in copy)
				{
					afterHandler?.Invoke(this, new ItemEventArgs<T>(item));
				}
			}
			else
			{
				throw new ItemsCantBeRemovedException<T>(itemsWhichCanNotBeRemoved);
			}
		}

		public Boolean Remove(T item)
		{
			var beforeHandler = Volatile.Read(ref ItemRemoving);
			if (beforeHandler != null)
			{
				var eventArgs = new CancelableItemEventArgs<T>(item);
				beforeHandler(this, eventArgs);
				if (eventArgs.IsCanceled)
				{
					return false;
				}
			}

			Boolean result = _collection.Remove(item);

			Volatile.Read(ref ItemRemoved)?.Invoke(this, new ItemEventArgs<T>(item));
			return result;
		}

		#endregion

		#region Methods to override

		public IEnumerator<T> GetEnumerator()
		{
			return _collection.GetEnumerator();
		}

		public Int32 Count
		{ get { return _collection.Count; } }

		public Boolean Contains(T item)
		{
			return _collection.Contains(item);
		}

		public void CopyTo(T[] array, Int32 arrayIndex)
		{
			_collection.CopyTo(array, arrayIndex);
		}

		#endregion
	}

	#region Support classes

	public class ItemEventArgs<T>
	{
		public T Item
		{ get; }

		public ItemEventArgs(T item)
		{
			Item = item;
		}
	}

	public class CancelableItemEventArgs<T> : ItemEventArgs<T>
	{
		public Boolean IsCanceled
		{ get; set; }

		public CancelableItemEventArgs(T item)
			: base(item)
		{
			IsCanceled = false;
		}
	}

	public class ItemsCantBeRemovedException<T> : Exception
	{
		public ICollection<T> Items
		{ get; }

		public ItemsCantBeRemovedException(IEnumerable<T> items)
			: base("Some items can not be removed.")
		{
			if (items != null)
			{
				Items = new List<T>(items);
			}
			else
			{
				throw new ArgumentNullException(nameof(items));
			}
		}
	}

	#endregion
}
