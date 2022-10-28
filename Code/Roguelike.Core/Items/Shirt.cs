using System.Drawing;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core.Items
{
	public class Shirt : Wear, IUpperBodyWear
	{
		#region Properties

		public override double Weight
		{ get { return 1; } }

		#endregion

		public Shirt(Color clothColor)
			: base(clothColor)
		{ }
	}
}
