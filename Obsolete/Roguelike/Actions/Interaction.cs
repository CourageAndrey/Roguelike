using System;

using Roguelike.Objects;

namespace Roguelike.Actions
{
	/// <summary>
	/// Возможное взаимодействие.
	/// </summary>
	public class Interaction
	{
		#region Свойства

		/// <summary>
		/// Номер действия.
		/// </summary>
		public int Id
		{ get; private set; }

		/// <summary>
		/// Описание действия.
		/// </summary>
		public string Description
		{ get; private set; }

		// функция, которая будет проверять выполнимость действия
		private readonly Func<Actor, bool> isPossibleHandler;

		#endregion

		/// <summary>
		/// ctor.
		/// </summary>
		/// <param name="id">номер действия</param>
		/// <param name="description">описание действия</param>
		/// <param name="isPossible">функция, которая будет проверять выполнимость действия</param>
		public Interaction(int id, string description, Func<Actor, bool> isPossible)
		{
			Id = id;
			Description = description;
			isPossibleHandler = isPossible;
		}

		/// <summary>
		/// Можно ли выполнить действие в данный момент.
		/// </summary>
		/// <param name="actor">взаимодействующий</param>
		/// <returns><b>true</b>, если можно, иначе - <b>false</b></returns>
		public bool IsPossible(Actor actor)
		{
			return isPossibleHandler(actor);
		}
	}
}