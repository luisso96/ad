package org.institutoserpies.ad;

import java.math.BigDecimal;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.ResultSetMetaData;
import java.util.Scanner;
import java.sql.SQLException;
import java.text.DecimalFormat;
import java.text.ParseException;
import java.sql.Connection;

public class PruebaArticulo {
	
	private enum Action {Salir, Nuevo, Editar, Eliminar, Consultar, Listar};
	
	private static Scanner scn = new Scanner(System.in);
	
	private static Action scanAction(){
		while (true) {
			System.out.println("0-Salir");
			System.out.println("1-Leer"); // ID
			System.out.println("2-Nuevo"); // Nombre Precio Categoria
			System.out.println("3-Editar"); // ID, Nombre, Precio, Categoria
			System.out.println("4-Eliminar"); // ID
			System.out.println("5-Listar Todo");
			String action = scn.nextLine().trim();
			if (action.matches("[012345]"))
				return Action.values()[Integer.parseInt(action)];
			System.out.println("Opcion invalida");
		}
	}
	
	private static class Articulo {
		private long id;
		private String nombre;
		private long categoria;
		private BigDecimal precio;
	}
	
	private static String scanString(String label) {
		System.out.print(label);
		return scn.nextLine().trim();
	}
	
	private static long scanLong(String label) {
		while (true) {
			System.out.print(label);
			String data = scn.nextLine().trim();
			try{
				long result = Long.parseLong(data);
				return result;
			}catch(NumberFormatException ex){
				System.out.println("Debe ser un numero");
			}	
		}
	}
	
	private static BigDecimal scanBigDecimal(String label) {
		while (true) {
			System.out.print(label);
			String data = scn.nextLine().trim();
			DecimalFormat decimalFormat = (DecimalFormat)DecimalFormat.getInstance();
			decimalFormat.setParseBigDecimal(true);
			try{
				return (BigDecimal)decimalFormat.parse(data);
			}catch(ParseException e){
				System.out.println("Debe ser un numero");
			}	
		}
	}
	
	private static Articulo scanArticulo() {
		Articulo articulo = new Articulo();
		articulo.nombre = scanString("Nombre: ");
		articulo.categoria = scanLong("Categoria: ");
		articulo.precio = scanBigDecimal("Precio: ");
		return articulo;
	}
	
	private static void showArticulo(Articulo articulo){
		System.out.println("id: "+articulo.id);
		System.out.println("nombre: "+articulo.nombre);
		System.out.println("categoria: "+articulo.categoria);
		System.out.println("precio: "+articulo.precio);
	}
	
	private static void showSQLException(Exception ex){
		System.out.println(ex.getLocalizedMessage());
	}
	
	private static Connection connection;
	
	private static PreparedStatement insertPreparedStatement;
	private static String insertSql = "insert into articulo (nombre, categoria, precio) values (?, ?, ?)";
	
	private static void nuevo(){
		Articulo articulo = scanArticulo();
		try{
			if (insertPreparedStatement == null)
				insertPreparedStatement = connection.prepareStatement(insertSql);
			insertPreparedStatement.setString(1, articulo.nombre);
			insertPreparedStatement.setLong(2, articulo.categoria);
			insertPreparedStatement.setBigDecimal(3, articulo.precio);
			System.out.println("Articulo guardado");
		}catch(SQLException ex){
			showSQLException(ex);
		}
	}
	
	private static PreparedStatement updatePreparedStatement;
	private final static String updateSql = "update articulo set nombre = ?, categoria = ?, precio = ? where id = ?";
	private static void editar() {
		long id = scanLong("id: ");
		Articulo articulo = scanArticulo(); //Resto de campos
		try {
			if (updatePreparedStatement == null) 
				updatePreparedStatement = connection.prepareStatement(updateSql);
			updatePreparedStatement.setString(1, articulo.nombre);
			updatePreparedStatement.setLong(2, articulo.categoria);
			updatePreparedStatement.setBigDecimal(3, articulo.precio);
			updatePreparedStatement.setLong(4, id);
			int count = updatePreparedStatement.executeUpdate();
			if (count == 1)
				System.out.println("articulo guardado.");
			else
				System.out.println("No existe articulo con ese id");
		} catch (SQLException ex){
			showSQLException(ex);
		}
	}
	
	private static PreparedStatement deletePreparedStatement;
	private final static String deleteSql = "delete from articulo where id = ?";
	private static void eliminar() {
		System.out.println("Eliminar articulo");
		long id = scanLong("id: ");
		try {
			if (deletePreparedStatement == null) 
				deletePreparedStatement = connection.prepareStatement(deleteSql);
			deletePreparedStatement.setLong(1, id);
			int count = updatePreparedStatement.executeUpdate();
			if (count == 1)
				System.out.println("articulo eliminado.");
			else
				System.out.println("No existe articulo con ese id");
		} catch (SQLException ex){
			showSQLException(ex);
		}
	}
	
	private static void showArticulo(ResultSet resulset) throws SQLException{
		System.out.println("id: "+resulset.getObject("id"));
		System.out.println("nombre: "+resulset.getObject("nombre"));
		System.out.println("categoria: "+resulset.getObject("categoria"));
		System.out.println("precio: "+resulset.getObject("precio"));
	}
	
	private static void ShowCurrentRow (ResultSet resulSet) throws SQLException {
		ResultSetMetaData resulSetMetaData = resulSet.getMetaData();
		for (int index = 1; index <= resulSetMetaData.getColumnCount(); index++)
			System.out.printf("%20s: %s", resulSetMetaData.getColumnName(index), resulSet.getObject(index));
	}
	
	
	private static PreparedStatement selectPreparedStatement;
	private final static String selectSql = "select * from articulo where id = ?";
	private static void consulta() {
		System.out.println("Consulta articulo: ");
		long id = scanLong("id: ");
		try {
			if (selectPreparedStatement == null) 
				selectPreparedStatement = connection.prepareStatement(selectSql);
			selectPreparedStatement.setLong(1, id);
			ResultSet resulSet = selectPreparedStatement.executeQuery();
		} catch (SQLException ex){
			showSQLException(ex);
		}
	}
	
	private static void closePreparedStatements() throws SQLException {
		if(insertPreparedStatement != null)
			insertPreparedStatement.close();
		if (updatePreparedStatement != null)
			updatePreparedStatement.close();
		if (deletePreparedStatement != null)
			deletePreparedStatement.close();
		if (selectPreparedStatement != null)
			selectPreparedStatement.close();
	}
	
	public static void main(String[] args) throws SQLException {
		boolean salida = false;
		int opcion = 0;
		
		connection =DriverManager.getConnection("jdbc:mysql://localhost/dbprueba", "root", "sistemas");

		while (!salida) {
				Action action = scanAction();
				if (action == Action.Salir) salida = true;
				if (action == Action.Nuevo) nuevo();
				if (action == Action.Editar) editar();
				if (action == Action.Eliminar) eliminar();
				if (action == Action.Consultar) consulta();

				closePreparedStatements();
				connection.close();
		}
		
	}
}
