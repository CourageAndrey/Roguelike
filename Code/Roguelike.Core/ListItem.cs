namespace Roguelike.Core
{
	public class ListItem
	{
		#region Properties

		public object ValueObject
		{ get; }

		public string Text
		{ get; }

		public bool IsAvailable
		{ get; set; }

		public bool IsSelected
		{ get; set; }

		#endregion

		public ListItem(object valueObject, string text, bool isAvailable = true)
		{
			ValueObject = valueObject;
			Text = text;
			IsAvailable = isAvailable;
		}
	}

	public class ListItem<T> : ListItem
	{
		#region Properties

		public T Value { get; }

		#endregion

		public ListItem(T value, string text, bool isAvailable = true)
			: base(value, text, isAvailable)
		{
			Value = value;
		}
	}
}
