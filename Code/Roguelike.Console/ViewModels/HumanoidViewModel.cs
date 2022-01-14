﻿using Roguelike.Core.ActiveObjects;

namespace Roguelike.Console.ViewModels
{
	internal class HumanoidViewModel : ObjectViewModel<Humanoid>
	{
		public HumanoidViewModel(Humanoid o)
			: base(o)
		{ }

		#region Overrides

		public override string Text
		{ get { return "@"; } }

		public override System.ConsoleColor Foreground
		{ get { return (Object is Hero) ? System.ConsoleColor.White : System.ConsoleColor.Cyan; } }

		#endregion
	}
}
