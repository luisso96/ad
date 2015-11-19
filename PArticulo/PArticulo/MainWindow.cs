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
		Title = "Articulo";
		Console.WriteLine ("MainWindow ctor.");
		fillTreeView ();

		deleteAction.Activated += delegate {
			object id = TreeViewHelper.GetId(treeView);
			Console.WriteLine("Click en deleteAction id={0}", id);
			delete(id);
		};

		editAction.Activated += delegate {
			object id = TreeViewHelper.GetId(treeView);

			new ArticuloView(id);

		};

		treeView.Selection.Changed += delegate {
			Console.WriteLine("Han ocurrido treeView.Selection.Changed");
			bool isSelected = TreeViewHelper.IsSelected(treeView);
			deleteAction.Sensitive = isSelected;
			editAction.Sensitive = isSelected;
		};
	}
	private void delete (object id) {
		if (!WindowHelper.ConfirmDelete (this))
			return;
		IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
		dbCommand.CommandText = "delete from articulo where id = @id";
		DbCommandHelper.AddParameter (dbCommand, "id", id);
		dbCommand.ExecuteNonQuery ();

			
	}

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
