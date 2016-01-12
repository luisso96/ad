package org.institutoserpies.ad;

import java.sql.DriverManager;
import java.util.Scanner;
import java.sql.ResultSet;
import java.sql.ResultSetMetaData;
import java.sql.SQLException;
import com.mysql.jdbc.Connection;

public class PruebaArticulo {
	public static void main(String[] args) throws SQLException {
		boolean salida = false;
		int opcion = 0;
		Scanner scn = new Scanner(System.in);

		Connection connection = (Connection) DriverManager.getConnection("jdbc:mysql://localhost/dbprueba", "root",
				"sistemas");

		String query = "SELECT * FROM articulo";
		ResultSet resultset = connection.createStatement().executeQuery(query);
		ResultSetMetaData resultsetmd = resultset.getMetaData();

		while (!salida) {
			System.out.println("0-Salir");
			System.out.println("1-Leer"); // ID
			System.out.println("2-Nuevo"); // Nombre Precio Categoria
			System.out.println("3-Editar"); // ID, Nombre, Precio, Categoria
			System.out.println("4-Eliminar"); // ID
			System.out.println("5-Listar Todo");
			try {
				opcion = Integer.parseInt(scn.nextLine());

				if (opcion == 0) {
					salida = true;
					System.out.println("Adios");
				} else if (opcion == 1) {
					System.out.println("Introduce ID");
					int id = Integer.parseInt(scn.nextLine());
					for (int i = 1; i <= resultsetmd.getColumnCount(); i++) {
						System.out.println(resultsetmd.getColumnName(i));
					}
					for (int i = 1; i <= resultsetmd.getColumnCount(); i++) {
						System.out.printf("%-15s", resultset);
						// System.out.print(resultset.getString(i)+"\t"+"\t");
					}
				} else if (opcion == 2) {
					System.out.println("2");
				} else if (opcion == 3) {
					System.out.println("3");
				} else if (opcion == 4) {
					System.out.println("4");
				} else if (opcion == 5) {
					for (int i = 1; i <= resultsetmd.getColumnCount(); i++) {
						System.out.printf("%-15s", resultsetmd.getColumnName(i));
						// System.out.print(resultsetmd.getColumnName(i)+"\t"+"\t");
					}

					while (resultset.next()) {
						System.out.println("");
						for (int i = 1; i <= resultsetmd.getColumnCount(); i++) {
							System.out.printf("%-15s", resultset.getString(i));
							// System.out.print(resultset.getString(i)+"\t"+"\t");
						}
					}
				} else {
					System.out.println("Escribe uno de los numeros de las opciones");
				}
			} catch (Exception e) {
				System.out.println("Introduce un numero");
			}
		}

		connection.close();
	}
}
