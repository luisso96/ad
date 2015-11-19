using Gtk;
using System;
using System.Collections;

namespace SerpisAd
{
	public class ComboBoxHelper
	{
		public static void Fill (ComboBox comboBox, QueryResult queryResult, object id)
		{
			CellRendererText cellRendererText = new CellRendererText ();
			comboBox.PackStart (cellRendererText,false);
			comboBox.SetCellDataFunc (cellRendererText, delegate (CellLayout cell_layout, CellRenderer Cell, TreeModel Tree_Model, TreeIter iter){
				IList row = (IList)Tree_Model.GetValue(iter, 0);
				cellRendererText.Text = row[1].ToString();
			});
			ListStore listStore = new ListStore (typeof(IList));
			//TODO localizacion de "sin asignar"
			IList first = new object [] { null, "<sin asignar>" };
			TreeIter treeIterId = listStore.AppendValues (first);
			foreach (IList row in queryResult.Rows) {
				TreeIter treeIter = listStore.AppendValues(row);
				if (row[0].Equals (row [0]))
					treeIterId = treeIter;
			}
				
			comboBox.Model = listStore;
			//comboBox.Active = 0;
			comboBox.SetActiveIter (treeIterId);
		}

		public static object GetId (ComboBox comboBox){
			TreeIter treeIter;
			comboBox.GetActiveIter (out treeIter);
			IList row = (IList)comboBox.Model.GetValue (treeIter, 0);
			return row [0];
		}
	}
}

