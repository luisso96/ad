package org.institutoserpies.ad;

import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.ResultSetMetaData;
import java.sql.SQLException;
import com.mysql.jdbc.Connection;
import com.mysql.jdbc.Statement;

public class PruebaArticulo {
	public static void main(String[] args) throws SQLException {
		Connection connection = (Connection) DriverManager.getConnection("jdbc:mysql://localhost/dbprueba","root","sistemas");
		
		String query = "SELECT * FROM articulo";
		ResultSet resultset = connection.createStatement().executeQuery(query);
		ResultSetMetaData resultsetmd = resultset.getMetaData();
		
			for (int i = 1; i <= resultsetmd.getColumnCount(); i++) {
				System.out.printf("%-12s",resultsetmd.getColumnName(i));
				//System.out.print(resultsetmd.getColumnName(i)+"\t"+"\t");
			}
		
	       while (resultset.next()) {
	    	   System.out.println("");
	    	   for (int i = 1; i <= resultsetmd.getColumnCount(); i++) {
	    		   System.out.printf("%-12s",resultset.getString(i));
	    		   //System.out.print(resultset.getString(i)+"\t"+"\t");
			}
	       }
		
		
		
		connection.close();
	}
}
