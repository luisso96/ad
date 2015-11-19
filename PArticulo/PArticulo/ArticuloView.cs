using Gtk;
using System;
using System.Collections;
using System.Data;
using SerpisAd;

namespace PArticulo
{
	public delegate void SaveDelegate();
	public partial class ArticuloView : Gtk.Window
	{
		private object id = null;
		private object categoria = null;
		private string nombre = "";
		private decimal precio = 0;
		private SaveDelegate save;

		public ArticuloView () : base(Gtk.WindowType.Toplevel)
		{

			init ();
			save = insert;

		}

		public ArticuloView(object id) : base(WindowType.Toplevel){
			this.id = id;
			load ();
			init ();
			save = update;
		}

		private void init () {
			this.Build ();
			entryNombre.Text = nombre;
			QueryResult queryResult = PersisterHelper.Get ("select * from categoria");
			ComboBoxHelper.Fill (comboBoxCategoria, queryResult, categoria);
			spinButtonPrecio.Value = Convert.ToDouble (precio);
			saveAction.Activated += delegate {save();};
		}

		private void load () {
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "select * from articulo where id = @id";
			DbCommandHelper.AddParameter (dbCommand, "id", id);
			IDataReader dataReader = dbCommand.ExecuteReader ();
			if (! dataReader.Read ())
				//TODO throw exception
				return;
			nombre = (string)dataReader["nombre"];
			categoria = dataReader["categoria"];
			if (categoria is DBNull)
				categoria = null;
			precio = (decimal)dataReader["precio"];
			dataReader.Close ();
		}

		private void insert() {
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "insert into articulo (nombre,categoria,precio) " +
				"values (@nombre, @categoria, @precio)";



			nombre = entryNombre.Text;
			categoria = ComboBoxHelper.GetId (comboBoxCategoria); //TODO Cogerlo del combobox
			precio = Convert.ToDecimal (spinButtonPrecio.Value); 

			DbCommandHelper.AddParameter (dbCommand,"nombre",nombre);
			DbCommandHelper.AddParameter (dbCommand,"categoria",categoria);
			DbCommandHelper.AddParameter (dbCommand,"precio",precio);
			dbCommand.ExecuteNonQuery ();
			Destroy ();
		}

		private void update () {

		}


	}
}

