using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using Gtk;
using SerpisAd;
using PArticulo;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		Console.WriteLine ("MainWindow ctor.");
		fillTreeView ();
	}

	/**private string [] getColumnName(IDataReader dataReader){
		List<string> columnName = new List<string> ();
		int count = dataReader.FieldCount;
		for (int i = 0; i < count; i++)
			columnName.Add (dataReader.GetName (i));
		return columnName.ToArray ();
	}**/

	/**private Type[] getTypes (int count) {
		List<Type> types = new List<Type> ();
		for (int i = 0; i < count; i++)
			types.Add (typeof(string));
		return types.ToArray ();
	}

	private string [] getValues(IDataReader mysqlDataReader) {
		List<object> values = new List<object> ();
		int count = mysqlDataReader.FieldCount;
		for (int i = 0; i <count; i++)
			values.Add (mysqlDataReader [i].ToString ());
		return values.ToArray ();
	}**/

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	protected void OnNewActionActivated (object sender, EventArgs e)
	{
		new ArticuloView ();
	}

	protected void OnRefreshActionActivated (object sender, EventArgs e)
	{
		fillTreeView ();
	}

	private void fillTreeView (){
		QueryResult queryResult = PersisterHelper.Get ("select *from articulo");
		TreeViewHelper.Fill (treeView, queryResult);
	}
}
