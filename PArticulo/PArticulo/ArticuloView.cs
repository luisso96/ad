using Gtk;
using System;
using System.Collections;
using SerpisAd;

namespace PArticulo
{
	public partial class ArticuloView : Gtk.Window
	{
		public ArticuloView () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			entryNombre.Text = "nuevo";
		
			QueryResult queryResult = PersisterHelper.Get ("select * from categoria");
			CellRendererText cellRendererText = new CellRendererText ();
			comboBoxCategoria.PackStart (cellRendererText,false);
			comboBoxCategoria.SetCellDataFunc (cellRendererText, delegate (CellLayout cell_layout, CellRenderer Cell, TreeModel Tree_Model, TreeIter iter){
				IList row = (IList)Tree_Model.GetValue(iter, 0);
				cellRendererText.Text = string.Format("{1}", row[1]);
			});
			ListStore listStore = new ListStore (typeof(IList));
			foreach (IList row in queryResult.Rows)
				listStore.AppendValues(row);
			comboBoxCategoria.Model = listStore;

			spinButtonPrecio.Value = 1.5;
		}
	}
}

