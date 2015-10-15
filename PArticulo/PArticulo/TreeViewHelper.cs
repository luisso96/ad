using System;
using System.Collections;
using Gtk;

namespace PArticulo
{
	public class TreeViewHelper
	{
		public TreeViewHelper ()
		{
		}

		public static void Fill(TreeView treeView, QueryResult queryResult){
			string[] columnNames = queryResult.ColumnNames;
			CellRendererText cellRendererText = new CellRendererText ();
			for (int i = 0; i < columnNames.Length; i++) {
				int column = i;
				treeView.AppendColumn (columnNames [i], cellRendererText,
				                       delegate (TreeViewColumn tree_column, CellRenderer cell, TreeModel tree_model, TreeIter iter) {
					IList row = (IList)tree_model.GetValue(iter,0);
					cellRendererText.Text =row[column].ToString();
				});
			}

			ListStore listStore = new ListStore (typeof(IList));
			//TODO rellenar el listStore
			foreach (IList row in queryResult.Rows)
				listStore.AppendValues (row);
			treeView.Model = listStore;
		}
	}
}
