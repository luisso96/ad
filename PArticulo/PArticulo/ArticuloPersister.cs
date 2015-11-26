using System;
using System.Data;
using SerpisAd;

namespace PArticulo
{
	public class ArticuloPersister
	{ 

		public static Articulo Load(object id) {
			Articulo articulo = new Articulo();
			articulo.Id = id;
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "select * from articulo where id = @id";
			DbCommandHelper.AddParameter (dbCommand, "id", id);
			IDataReader dataReader = dbCommand.ExecuteReader ();
			articulo.Nombre = (string)dataReader["nombre"];
			articulo.Categoria = dataReader["categoria"];
			if (articulo.Categoria is DBNull)
				articulo.Categoria = null;
			articulo.Precio = (decimal)dataReader["precio"];
			return articulo;
		}

		public static Articulo Insert(Articulo articulo) {
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "insert into articulo (nombre,categoria,precio) " +
				"values (@nombre, @categoria, @precio)";



			Articulo.Nombre = entryNombre.Text;
			Articulo.Categoria = ComboBoxHelper.GetId (comboBoxCategoria); //TODO Cogerlo del combobox
			Articulo.Precio = Convert.ToDecimal (spinButtonPrecio.Value); 

			DbCommandHelper.AddParameter (dbCommand,"nombre",Articulo.Nombre);
			DbCommandHelper.AddParameter (dbCommand,"categoria",Articulo.Categoria);
			DbCommandHelper.AddParameter (dbCommand,"precio",Articulo.Precio);
			dbCommand.ExecuteNonQuery ();
		}

		public static Articulo Update(Articulo articulo) {

		}
	}
}

