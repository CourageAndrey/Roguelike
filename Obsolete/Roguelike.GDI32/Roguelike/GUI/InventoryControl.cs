using System.Windows.Forms;

using Roguelike.Items;

namespace Roguelike.GUI
{
	/// <summary>
	/// Контрол списка вещей.
	/// </summary>
	public partial class InventoryControl : UserControl
	{
		#region Свойства

		private Inventory inventory;

		/// <summary>
		/// Привязанная инвенторь.
		/// </summary>
		public Inventory Inventory
		{
			get { return inventory; }
			set
			{
				inventory = value;
				itemBindingSource.DataSource = inventory.AllItems;
				//itemBindingSource.ResetBindings(false);
				labelWeightTotal.Text = inventory.TotalWeight.ToString(Item.WeightFormat);
			}
		}

		#endregion

		/// <summary>
		/// ctor.
		/// </summary>
		public InventoryControl()
		{
			InitializeComponent();
		}
	}
}
