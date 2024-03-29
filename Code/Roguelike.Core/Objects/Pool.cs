﻿using Roguelike.Core.Aspects;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.Objects
{
	public class Pool : Object
	{
		#region Properties

		public override bool IsSolid
		{ get { return false; } }

		#endregion

		public Pool()
		{
			AddAspects(new WaterSource());
		}

		public override string GetDescription(Language language, IAlive forWhom)
		{
			return language.Objects.Pool;
		}
	}
}
