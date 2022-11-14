using System.Drawing;

using Roguelike.Core.ActiveObjects;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.StaticObjects
{
	public class Pool : Object, IWaterSource
	{
		#region Properties

		public override bool IsSolid
		{ get { return false; } }

		#endregion

		public ActionResult Drink(IAlive who)
		{
#warning Avoid typecast below.
			int water = ((State) who.State).WaterToFull;
			if (water > 0)
			{
				return who.Drink(new PoolDrink(water));
			}
			else
			{
#warning Warn about "You don't want to drink."
				return null;
			}
#warning Take vermins into account.
		}

		private class PoolDrink : IDrink
		{
			public decimal Weight
			{ get { throw new System.NotSupportedException(); } }

			public ItemType Type
			{ get { throw new System.NotSupportedException(); } }

			public Color Color
			{ get { throw new System.NotSupportedException(); } }

			public Material Material
			{ get { throw new System.NotSupportedException(); } }

			public string GetDescription(Language language, IAlive forWhom)
			{
#warning Objects also need localization.
				return ToString();
			}

			public event ValueChangedEventHandler<IRequireGravitation, decimal> WeightChanged;
			public event EventHandler<IItem, IAlive> Picked;
			public event EventHandler<IItem, IAlive> Dropped;

			public void RaisePicked(IAlive who)
			{
				throw new System.NotSupportedException();
			}

			public void RaiseDropped(IAlive who)
			{
				throw new System.NotSupportedException();
			}

			public int Nutricity
			{ get { return 0; } }

			public int Water
			{ get; }

			public PoolDrink(int value)
			{
				Water = value;
			}
		}
	}
}
