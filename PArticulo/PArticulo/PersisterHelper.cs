using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
namespace PArticulo
{
	public class PersisterHelper
	{
		public PersisterHelper ()
		{
		}

		public static QueryResult Get(string selectText) {

			IDbConnection dbConnection = App.Instance.DbConnection;
			IDbCommand dbCommand = dbConnection.CreateCommand();
			dbCommand.CommandText = selectText;
			IDataReader dataReader = dbCommand.ExecuteReader ();
			QueryResult queryResult = new QueryResult ();
			queryResult.ColumnNames = getColumnName (dataReader);
			List<IList> rows = new List<IList> ();
			while (dataReader.Read()) {
				IList row = getRow (dataReader);
				rows.Add (row);
			}

			queryResult.Rows = rows;
			dataReader.Close ();
			return queryResult;
		}

		private static string [] getColumnName (IDataReader dataReader){
			List<string> columnaNames = new List<string> ();
			int count = dataReader.FieldCount;
			for (int i = 0; i < count; i++)
				columnaNames.Add (dataReader.GetName (i));
			return columnaNames.ToArray ();
		}

		private static IList getRow(IDataReader dataReader) {
			List<object> values = new List<object> ();
			int count = dataReader.FieldCount;
			for (int index = 0; index < count; index++)
					values.Add (dataReader [index]);
			return values;
		}
	}
}

