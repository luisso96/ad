using System;
using System.Data;
using SerpisAd;
using Gtk;
using System.Collections;

namespace PArticulo
{
	public class ArticuloPersister
	{ 

		public static Articulo Load(object id) {
			Articulo articulo = new Articulo();
			articulo.Id = id;
			IDbCommand dbCommand =App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "select * from articulo where id = @id";
			DbCommandHelper.AddParameter (dbCommand, "id", id);
			IDataReader dataReader = dbCommand.ExecuteReader ();
			articulo.Nombre = (string)dataReader["nombre"];
			articulo.Categoria = get (dataReader ["categoria"], null);
			articulo.Precio = (decimal)get(dataReader["precio"], 0L);
			dataReader.Close ();
			return articulo;
		}

		private static object get (object value, object defaultValue){
			return value is DBNull ? defaultValue : value;
		}

		public static int Insert(Articulo articulo) {
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "insert into articulo (nombre,categoria,precio) " +
				"values (@nombre, @categoria, @precio)";

			DbCommandHelper.AddParameter (dbCommand,"nombre",articulo.Nombre);
			DbCommandHelper.AddParameter (dbCommand,"categoria",articulo.Categoria);
			DbCommandHelper.AddParameter (dbCommand,"precio",articulo.Precio);
			return dbCommand.ExecuteNonQuery ();
		}

		public static int Update(Articulo articulo) {
			IDbCommand dbCommand = App.Instance.DbConnection.CreateCommand ();
			dbCommand.CommandText = "update articulo set nombre = @nombre, " +
				"categoria = @categoria, precio = @precio where id = @id";

			DbCommandHelper.AddParameter (dbCommand,"nombre",articulo.Nombre);
			DbCommandHelper.AddParameter (dbCommand,"categoria",articulo.Categoria);
			DbCommandHelper.AddParameter (dbCommand,"precio",articulo.Precio);
			DbCommandHelper.AddParameter (dbCommand,"id",articulo.Id);
			return dbCommand.ExecuteNonQuery ();
		}

		public static int Save (Articulo articulo) {
			return articulo.Id == null ? Insert (articulo) : Update (articulo);
		}
	}
}

