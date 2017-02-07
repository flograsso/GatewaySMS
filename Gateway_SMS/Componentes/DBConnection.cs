/*
 * Created by SharpDevelop.
 * User: SoporteSEM
 * Date: 06/02/2017
 * Time: 16:29
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;

namespace Gateway_SMS
{
	/// <summary>
	/// Description of DBConnection.
	/// </summary>
	/*Clase para manejar la conexion a la DB*/
	public class DBconnection{
		
		private MySqlConnection conexion;
		private DataSet ds;
		private MySqlDataAdapter adapter;
		
		private string server;
		private string databaseName;
		private string databaseUser;
		private string databasePassword;
		
		public DBconnection(){
			
		}
		

		public void setServer(string server){
			this.server=server;
		}
		
		public void setdatabaseName(string databaseName){
			this.databaseName=databaseName;
		}
		
		public void setdatabaseUser(string databaseUser){
			this.databaseUser=databaseUser;
		}
		
		public void setdatabasePassword(string databasePassword){
			this.databasePassword=databasePassword;
		}
		
		/*Devuelve estado conexion*/
		public ConnectionState getConnectionState(){
			return conexion.State;
		}
		
		/*Conecta a la base*/
		public bool DBConnect(){
			
			string conexionString = "server="+server+";database="+databaseName+";user="+databaseUser+";password="+databasePassword+";Allow Zero Datetime=True;";
			conexion = new MySqlConnection(conexionString);
			
			try
			{
				conexion.Open();
			}
			catch (Exception)
			{
				MessageBox.Show("No se pudo conectar a la base de datos");
			}
			
			return (conexion.State==ConnectionState.Open);
			
		}
		
		/*Ejecuta query y devuelve un DataTable*/
		public DataTable executeQuery(string query){
			
			adapter= new MySqlDataAdapter(query,conexion);
			ds = new DataSet();
			
			
			try
			{
				adapter.Fill(ds,"Phone_Table");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			adapter = null;
			
			return ds.Tables["Phone_Table"];
			
			
		}
		
		/*Ejecuta una query que devuelva un valor (integer)*/
		public int executeCount(string sql){

			MySqlCommand cmd = new MySqlCommand(sql, conexion);
			Int32 count = Convert.ToInt32(cmd.ExecuteScalar());
			cmd = null;
			return count;
		}
	}
}
