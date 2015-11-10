using Gtk;
using System;
using System.Collections;
using System.Data;
using SerpisAd;

namespace PArticulo
{
	public partial class ArticuloView : Gtk.Window
	{
		public ArticuloView () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
		
			QueryResult queryResult = PersisterHelper.Get ("select * from categoria");
			ComboBoxHelper.Fill (comboBoxCategoria, queryResult);

			saveAction.Activated += delegate {
				save();
			};
		}

		private void save () {
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "insert into articulo (nombre,categoria,precio) " +
				"values (@nombre, @categoria, @precio)";



			string nombre = entryNombre.Text;
			object categoria = ComboBoxHelper.GetId (comboBoxCategoria); //TODO Cogerlo del combobox
			decimal precio = Convert.ToDecimal (spinButtonPrecio.Value); 

			DbCommandHelper.AddParameter (dbCommand,"nombre",nombre);
			DbCommandHelper.AddParameter (dbCommand,"categoria",categoria);
			DbCommandHelper.AddParameter (dbCommand,"precio",precio);
			dbCommand.ExecuteNonQuery ();
		}



	}
}

