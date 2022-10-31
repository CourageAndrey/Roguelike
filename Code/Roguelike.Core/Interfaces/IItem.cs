using System.Drawing;

using Roguelike.Core.Localization;

namespace Roguelike.Core.Interfaces
{
	public interface IItem : IRequireGravitation
	{
		ItemType Type
		{ get; }

		Color Color
		{ get; }

		Material Material
		{ get; }

		string GetDescription(LanguageItems language, IAlive forWhom);

		event EventHandler<IItem, IAlive> Picked;

		event EventHandler<IItem, IAlive> Dropped;

		void RaisePicked(IAlive who);

		void RaiseDropped(IAlive who);
	}

	public interface IWear : IItem
	{
		event EventHandler<IWear, IAlive> Equipped;

		event EventHandler<IWear, IAlive> Unequipped;

		void RaiseEquipped(IAlive who);

		void RaiseUnequipped(IAlive who);
	}

	public interface IWeapon : IItem
	{
		bool IsRange
		{ get; }

		Material Material
		{ get; }

		event EventHandler<IWeapon, IAlive> PreparedForBattle;

		event EventHandler<IWeapon, IAlive> StoppedBattle;

		void RaisePreparedForBattle(IAlive who);

		void RaiseStoppedBattle(IAlive who);
	}

	#region Wear interfaces

	public interface IHeadWear : IWear
	{ }

	public interface IUpperBodyWear : IWear
	{ }

	public interface ILowerBodyWear : IWear
	{ }

	public interface ICoverWear : IWear
	{ }

	public interface IHandWear : IWear
	{ }

	public interface IFootWear : IWear
	{ }

	public interface IJewelry : IWear
	{ }

	#endregion
}
