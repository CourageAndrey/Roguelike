using System.Collections.Generic;

using Roguelike.Body;
using Roguelike.Items;
using Roguelike.Objects;

using Sef.Common;

namespace Roguelike.Worlds
{
  /// <summary>
  /// Стартовый бонус.
  /// </summary>
  public abstract class StartBonus
	{
		#region Свойства

		/// <summary>
    /// Наименование.
    /// </summary>
    public string Name
    { get; private set; }

		/// <summary>
		/// Наименование.
		/// </summary>
		public string Description
		{ get; private set; }

		#endregion

    /// <summary>
    /// ctor.
    /// </summary>
		/// <param name="name">наименование</param>
		/// <param name="description">описание</param>
    protected StartBonus(string name, string description)
    {
      Name = name;
	    Description = description;
    }

		/// <summary>
		/// Применить.
		/// </summary>
		/// <param name="actor">актёр, к которому будет применён бонус</param>
	  public abstract void Apply(Actor actor);

	  /// <summary>
	  /// Returns a string that represents the current object.
	  /// </summary>
	  /// <returns>
	  /// A string that represents the current object.
	  /// </returns>
	  /// <filterpriority>2</filterpriority>
	  public override string ToString()
	  {
		  return string.Format("{0} ({1})", Name, Description);
	  }

		/// <summary>
		/// Полный список.
		/// </summary>
		public static readonly List<StartBonus> All = new List<StartBonus>
		{
			new WeaponedBonus(),
			new FeaturesBonus("сильный", new Features(strength: 1)),
			new FeaturesBonus("ловкий", new Features(dexterity: 1)),
			new FeaturesBonus("умный", new Features(intelligence: 1)),
			new FeaturesBonus("твёрдый", new Features(willpower: 1)),
			new FeaturesBonus("зоркий", new Features(perception: 1)),
			new FeaturesBonus("красавец", new Features(appearance: 1)),
			new FeaturesBonus("очаровашка", new Features(charisma: 1)),
		};
  }

  #region Классы бонусов

	internal sealed class WeaponedBonus : StartBonus
	{
		public WeaponedBonus()
			: base("вооружённый", "начало с оружием")
		{ }

		/// <summary>
		/// Применить.
		/// </summary>
		/// <param name="actor">актёр, к которому будет применён бонус</param>
		public override void Apply(Actor actor)
		{
			actor.EquippedWeapons.Add(Weapon.All.GetRandom(World.Instance.God).ApplyRandomModifier());
		}
	}

	internal sealed class FeaturesBonus : StartBonus
	{
		private readonly Features features;

		public FeaturesBonus(string name, Features features)
			: base(name, features.GetBonusLine())
		{
			this.features = features;
		}

		/// <summary>
		/// Применить.
		/// </summary>
		/// <param name="actor">актёр, к которому будет применён бонус</param>
		public override void Apply(Actor actor)
		{
			actor.Properties.Merge(features);
		}
	}

  #endregion
}
