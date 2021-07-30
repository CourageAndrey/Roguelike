using System.Drawing;
using System.Windows.Forms;

using Roguelike.Items;
using Roguelike.Objects;

namespace Roguelike.GUI
{
	/// <summary>
	/// Диалог списка вещей.
	/// </summary>
	public partial class InventoryDialog : Form
	{
		/// <summary>
		/// ctor.
		/// </summary>
		/// <param name="actor">владелец</param>
		public InventoryDialog(Actor actor)
		{
			InitializeComponent();

			double wTotal = 0;
			double wMax = actor.GetWeightMax();
			foreach (var inventory in actor.Inventories)
			{
				var inventoryPage = new TabPage { Text = inventory.Name };
				inventoryPage.Controls.Add(new InventoryControl
				{
					Inventory = inventory,
					Dock = DockStyle.Fill,
				});
				tabControl.TabPages.Add(inventoryPage);
				wTotal += inventory.TotalWeight;
			}

			labelWeightTotal.Text = wTotal.ToString(Item.WeightFormat);
			labelWeightMax.Text = wMax.ToString(Item.WeightFormat);
			if (wTotal > wMax)
			{
				labelWeightTotal.ForeColor = Color.Red;
			}
			else if (wTotal > actor.GetWeightOptimal())
			{
				labelWeightTotal.ForeColor = Color.Yellow;
			}
			else
			{
				labelWeightTotal.ForeColor = Color.Lime;
			}
		}
	}
}
