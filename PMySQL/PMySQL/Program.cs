
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace PMySQL
{
	class MainClass
	{
		public static void Main (string[] args)
		{

			MySqlConnection mySqlConnection = new MySqlConnection(
				//Database = nombre 
				"Database=dbprueba;Data Source=localhost;User Id=root;Password=sistemas"
				);

			mySqlConnection.Open ();

			MySqlCommand mySqlCommand = mySqlConnection.CreateCommand ();
			mySqlCommand.CommandText = "select * from articulo";

			MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader ();

			//while (mySqlDataReader.Read()) {
			//	Console.WriteLine ("id={0} nombre={1}", mySqlDataReader ["id"], mySqlDataReader ["nombre"]);
			//}
			showColumnNames(mySqlDataReader);
			show(mySqlDataReader);
			
			mySqlDataReader.Close ();

			mySqlConnection.Close ();

		}

		private static void showColumnNames (MySqlDataReader mySqlDataReader){
			Console.WriteLine ("ShowColumnNames");
			string[] columnNames = getColumnNames (mySqlDataReader);
			Console.WriteLine (string.Join (", ", columnNames));
		}

		private static string[] getColumnNames(MySqlDataReader mySqlDataReader){
			int count = mySqlDataReader.FieldCount;
			string[] columnNames = new string[count];
			for (int i = 0; i < count; i++)
				columnNames [i] = mySqlDataReader.GetName (i);
			return columnNames;
		}

		private static void show(MySqlDataReader mySqlDataReader) {
			Console.WriteLine ("show");
			while (mySqlDataReader.Read()) {
				showRow (mySqlDataReader);
			}
		}

		private static void showRow(MySqlDataReader mySqlDataReader){
			int count = mySqlDataReader.FieldCount;
			string line = "";
			for (int i = 0; i < count; i++)
				line = line + mySqlDataReader [i] + " ";

			Console.WriteLine (line);
		}

	}
}
