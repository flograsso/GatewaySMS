/*
 * Created by SharpDevelop.
 * User: SoporteSEM
 * Date: 04/01/2017
 * Time: 10:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Globalization;
using System.Threading.Tasks;
using System.IO.Ports;


namespace Gateway_SMS
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		/*Creo clases*/
		SIM900 sim900;
		SerialPort serialPort1;
		DBconnection dbConnection;
		
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void SerialPort1DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
		{
			sim900.setReceivedBuffer(sim900.getReceivedBuffer()+serialPort1.ReadLine());
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			dataGridView1.Visible=false;
			dbConnection = new DBconnection();
			this.Size = new Size(340, 220);
			serialPort1 = new SerialPort();
			sim900 = new SIM900(ref serialPort1);
			this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.SerialPort1DataReceived);
		}
		void MainFormFormClosed(object sender, FormClosedEventArgs e)
		{
			try{
				if(serialPort1.IsOpen){
					serialPort1.Close();
				}
				
			}
			catch (Exception ex){}
		}
		void Button_conectarClick(object sender, EventArgs e)
		{
			bool OK= true;

			label_estadoConexion.Text="CONECTANDO...";
			panel_conexion.Refresh();
			
			try{
				serialPort1.Close();
				serialPort1.PortName=textBox_port.Text;
				serialPort1.Open();
			}
			catch(Exception ex){
				MessageBox.Show("Error de Conexión con el módulo SIM","ERROR",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
			}
			
			if (serialPort1.IsOpen){
				
				label_estadoConexion.Text="CONECTADO";
				
				/*Bajo el panel de ver SMS*/
				panel_verSMS.Location=new Point(12,209);
				
				/*Subo el panel de estado*/
				panel_estado.Location=new Point(12,110);
				
				panel_estado.Show();
				this.Size = new Size(340, 250);
				
				
				
				/*Intento conectarme al SIM*/
				if(sim900.connectSIM900()){
					AT_label.Text="SIM900 ONLINE";
				}
				else
				{
					AT_label.Text="SIM900 OFFLINE";
					OK=false;
				}
				
				/*Intento adquirir IMEI*/
				if(sim900.setIMEI()){
					textBox_IMEI.Text=sim900.getIMEI();
				}
				else
				{
					textBox_IMEI.Text="IMEI ERROR";
					OK=false;
				}
				
				/*Intento adquirir señal*/
				if (sim900.setSignal()){
					CSQ_label.Text="SEÑAL:"+sim900.getSignal();
				}
				else
				{
					CSQ_label.Text="ERROR EN SEÑAL";
					OK=false;
				}
				
				/*Intento setear modo SMS*/
				if (sim900.prepareSMS()){
					SMSReady_label.Text="LISTO PARA ENVIAR SMS";
				}
				else
				{
					SMSReady_label.Text="NO LISTO PARA ENVIAR SMS";
					OK=false;
				}
				
				panel_estado.Refresh();
				
				/*Todo OK*/
				
				if(OK){

					/*Desactivo boton conectar*/
					button_conectar.Enabled=false;
					
					/*Deshabilito textbox puerto*/
					textBox_port.Enabled=false;

					/*Agrando para ver el dataGrid*/
					this.Size = new Size(690, 400);
					
					/*Seteo parametros*/
					dbConnection.setdatabaseName("testcsharp");
					dbConnection.setServer("localhost");
					dbConnection.setdatabaseUser("root");
					dbConnection.setdatabasePassword("");
					
					/*Me conecto a la DB*/
					if (dbConnection.DBConnect()){
						
						/*Ejecuto query*/
						dataGridView1.DataSource=dbConnection.executeQuery("SELECT date,number,message,state FROM `sms`");
						
						/*Muestro resultados*/
						dataGridView1.Columns[0].HeaderText="Fecha";
						dataGridView1.Columns[1].HeaderText="Numero";
						dataGridView1.Columns[2].HeaderText="Mensaje";
						dataGridView1.Columns[3].HeaderText="Estado";
						dataGridView1.AutoResizeColumns();
						dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
						dataGridView1.Visible=true;
						
						/*Oculto boton de "Ver SMS"*/
						button_verSMS.Visible=false;
						button_procesarSMS.Visible=true;
						button_pararProcesarSMS.Visible=true;
						button_pararProcesarSMS.Enabled=false;
					}
					
				}
				/*Si no estoy listo para mandar SMS*/
				else
				{
					/*Muestro panel estado y panel ver SMS*/
					this.Size = new Size(340, 323);
					
				}


			}
			
			/*Puerto serial no abierto*/
			else
			{
				label_estadoConexion.Text="DESCONECTADO";
				panel_estado.Hide();

				
			}
		}
		void Label1Click(object sender, EventArgs e)
		{
			
		}
		void Button_verSMSClick(object sender, EventArgs e)
		{
			/*Agrando para ver el dataGrid*/
			this.Size = new Size(690, 400);
			
			/*Seteo parametros*/
			dbConnection.setdatabaseName("testcsharp");
			dbConnection.setServer("localhost");
			dbConnection.setdatabaseUser("root");
			dbConnection.setdatabasePassword("");
			
			/*Me conecto a la DB*/
			if (dbConnection.DBConnect()){
				
				/*Ejecuto query*/
				dataGridView1.DataSource=dbConnection.executeQuery("SELECT date,number,message,state FROM `sms`");
				
				/*Muestro resultados*/
				dataGridView1.Columns[0].HeaderText="Fecha";
				dataGridView1.Columns[1].HeaderText="Numero";
				dataGridView1.Columns[2].HeaderText="Mensaje";
				dataGridView1.Columns[3].HeaderText="Estado";
				dataGridView1.AutoResizeColumns();
				dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
				dataGridView1.Visible=true;
			}
			
		}
		void button_procesarSMSClick(object sender, EventArgs e)
		{
			button_pararProcesarSMS.Enabled=true;
			button_procesarSMS.Enabled=false;
		}
		void Button_pararProcesarSMSClick(object sender, EventArgs e)
		{
			button_pararProcesarSMS.Enabled=false;
			button_procesarSMS.Enabled=true;
		}
	}
	
	public class DBconnection{
		
		private MySqlConnection conexion;
		private DataSet ds;
		private MySqlDataAdapter adapter;
		
		private string server;
		private string databaseName;
		private string databaseUser;
		private string databasePassword;
		
		public DBconnection(){
			ds = new DataSet();
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
		

		public bool DBConnect(){
			
			string conexionString = "server="+server+";database="+databaseName+";user="+databaseUser+";password="+databasePassword+";";
			conexion = new MySqlConnection(conexionString);
			
			try
			{
				conexion.Open();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			
			return (conexion.State==ConnectionState.Open);
			
		}
		public DataTable executeQuery(string query){
			
			adapter= new MySqlDataAdapter(query,conexion);
			
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
	}
	
	public class SIM900{
		
		private String receivedBuffer;
		private string IMEI;
		private string signal;
		private SerialPort serialPort;
		
		/*Constructor*/
		public SIM900(ref SerialPort serial){
			receivedBuffer="";
			IMEI="";
			signal = "";
			serialPort=serial;
		}
		
		public String getSignal(){
			return signal;
		}
		
		public String getReceivedBuffer(){
			return receivedBuffer;
		}
		
		public void setReceivedBuffer(String receivedBuffer){
			this.receivedBuffer=receivedBuffer;
		}
		
		public String getIMEI(){
			return IMEI;
		}
		
		public void waitForResult(String response){
			while((getReceivedBuffer().IndexOf(response)==-1)){}
		}
		
		
		/*Return = null --> Comando falló
		 *Return !=null 	--> Comando OK*/
		String sendCommand(String command, int delaySec, String response){
			
			string received = "";
			receivedBuffer="";
			
			/*Envio Comando*/
			serialPort.WriteLine((command+"\r\n"));
			
			/*Espero hasta que pase el tiempo delaySec o encuentre el string response en la respuesta*/
			var task = Task.Run(() => waitForResult(response));
			if (task.Wait(TimeSpan.FromSeconds(delaySec))){
				/*Encontro string*/
				received=receivedBuffer;
			}
			else
			{
				/*No Encontro string*/
				received = null;
			}
			
			receivedBuffer="";
			return received;
		}
		
		public bool enviarSMS(string numero, string mensaje){
			bool OK=true;
			
			
			if ((sendCommand("AT+CMGS=\""+numero+"\"",4,"ERROR")) != null){
				OK=false;
			}
			
			if ((sendCommand(mensaje,4,"ERROR")) != null){
				OK=false;
			}
			
			if ((sendCommand("\x1A",3,"OK")) == null){
				OK=false;
			}
			
			return OK;

		}
		
		public bool connectSIM900(){
			
			String result = "";
			bool OK = true;
			
			
			if (sendCommand("ATE0",2,"OK")==null){
				OK = false;
			}
			
			if ((result = sendCommand("AT+CREG?",2,"OK")) != null){
				if ((result.IndexOf("0,1")==-1)&&(result.IndexOf("1,1")==-1)){
					OK=false;
				}
			}
			else
			{
				OK=false;
			}

			
			if (sendCommand("AT+CPIN?",2,"OK")==null){
				OK=false;
			}
			
			return OK;
		}
		
		
		public bool setIMEI(){
			
			string result="";
			bool OK = true;
			
			if ((result = sendCommand("AT+CGSN",2,"OK")) != null){
				IMEI=result.Substring(0,result.Length-3);
			}
			else
			{
				OK=false;
			}
			
			return OK;
			
		}

		
		
		public bool setSignal(){
			
			string result = "";
			bool OK = true;
			
			if ((result = sendCommand("AT+CSQ",2,"OK")) != null){
				
				int i =0;
				
				
				do{
					i++;
				}while ((result[i] != ':') && ( i<result.Length));
				
				if (Char.IsNumber(result[i+2]) && Char.IsNumber(result[i+3])){
					signal=(Convert.ToString(result[i+2]))+(Convert.ToString(result[i+3]));

				}
				else
					if (Char.IsNumber(result[i+2])){
					signal="0"+result[i+2];
					
				}

			}
			else
			{
				OK=false;
				
			}
			return OK;
			
		}
		
		public bool prepareSMS(){
			
			bool OK = true;
			
			if ((sendCommand("AT+CMGF=1",2,"OK")) == null){
				OK=false;
			}
			
			if (sendCommand("AT+CSCS=\"GSM\"",2,"OK") == null){
				OK=false;
			}
			
			
			return OK;
			
		}

	}
}
