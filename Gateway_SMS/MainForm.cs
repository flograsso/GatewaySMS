/*
 * Created by SharpDevelop.
 * User: Lo Grasso Federico
 * Date: 04/01/2017
 * Time: 10:31
 * 
 * Gateway SMS
 * 
 * Software de envio masivo de SMS mediante comandos AT enviados por puerto serial
 * 
 * Tiempo de procesamiento: 10s por SMS
 * 
 * Instalacion GUI:
 * 	-Dentro del proyecyo en el SharpDevelop ir a "References" --> Add reference --> .NET Assembly --> Browser
 *  -Agregar los tres archivos .DLL que se encuentran en la carpeta ./Gatewat_SMS/GUI Metro/
 *  -Luego para tener los componentes  en la barra de componentes, desde la pestaña de design
 *  -Ir a tools --> Click derecho --> Configure sidebar --> Y alli agregar una nueva y agregarle los componentes
 *
 */



/*Set DEBUG_YES or DEBUG_NO*/
#define DEBUG_NO


#if DEBUG_YES
#define DEBUG
#elif DEBUG_NO
#undef DEBUG
#endif

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Globalization;
using System.Threading.Tasks;
using System.IO.Ports;
using System.ComponentModel;
using MetroFramework.Components;
using MetroFramework.Fonts;
using MetroFramework.Forms;
using MySql.Data.Types;



namespace Gateway_SMS
{
	
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : MetroForm
	{
		/*Defino clases*/
		SIM900 sim900;
		SerialPort serialPort1;
		DBconnection dbConnection;
		DataTable dt;
		DateTime dateTime;
		ContextMenuStrip contextMenuStrip;
		
		/*Variable de running*/
		bool processing;
		
		/*Constante de tiempo para procesar SMS (en ms)*/
		const int timerInterval = 10000;
		
		int SMSCount;
		
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
		
		/*Data recibida por puerto serial. Lo voy almacenando*/
		void SerialPort1DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
		{
			try{
				sim900.setReceivedBuffer(sim900.getReceivedBuffer()+serialPort1.ReadLine());
			}
			catch(Exception){}
		}
		
		
		
		/*LOAD del Main Form*/
		void MainFormLoad(object sender, EventArgs e)
		{
			//Creo Objetos
			dbConnection = new DBconnection();
			dt = new DataTable();
			serialPort1 = new SerialPort();
			dateTime = new DateTime();
			sim900 = new SIM900(ref serialPort1);
			contextMenuStrip = new ContextMenuStrip();
			
			/*Agrego evento manejador del dataReceived del serial*/
			this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.SerialPort1DataReceived);
			
			
			//Oculto dataGrid
			metroGrid1.Visible=false;
			
			//Muestro panel de ver SMS solo con boton de ver SMS
			button_procesar_SMS.Visible=false;
			button_detenerProcesamiento.Visible=false;
			button_verSMS.Visible=true;
			button_verSMS.Location=new Point(6,0); // Subo boton de Ver SMS
			panel_estado.Visible=false;
			
			/*Muevo logo Cespi*/
			logoCespi.Location= new Point(10,280);
			
			/*Agrego opciones al menu contextual*/
			contextMenuStrip.Items.Add("Abrir");
			contextMenuStrip.Items.Add("Cerrar");
			
			/*Asigno metodo al click del item del metodo contextual*/
			contextMenuStrip.ItemClicked += new ToolStripItemClickedEventHandler(contextMenuStrip_Clicked);
			
			/*Asocio el menu contextual al notifyIcon*/
			notifyIcon1.ContextMenuStrip=contextMenuStrip;
			
			
			/*Ignoro los errores de que un Thread modifica un objeto que no creo*/
			CheckForIllegalCrossThreadCalls = false;
			
			
			processing =false;
			
		}
		
		/*Evento si se clickeo en un item del menu contextual*/
		void contextMenuStrip_Clicked(object sender, ToolStripItemClickedEventArgs  e) {

			if (e.ClickedItem.Text=="Abrir")
			{
				Show();
				this.WindowState = FormWindowState.Normal;
			}
			else
			{
				if (e.ClickedItem.Text=="Cerrar")
				{
					Close();

				}
			}

			
		}
		

		
		/*Cierre del Main Form*/
		void MainFormFormClosed(object sender, FormClosedEventArgs e)
		{
			/*Cierro el serial*/
			try{
				if(serialPort1.IsOpen){
					serialPort1.Close();
				}
				
			}
			catch (Exception){}
		}
		
		
		
		#if DEBUG
		bool parche (){
			Thread.Sleep(6000);
			return true;
		}
		#endif
		
		delegate void contarElementosAProcesarDelegate();
		
		void contarElementosAProcesar(){
			if(this.InvokeRequired){
				contarElementosAProcesarDelegate delegado = new contarElementosAProcesarDelegate(contarElementosAProcesar);
				this.Invoke(delegado);
			}
			else
			{
				SMSCount = dbConnection.executeCount("SELECT COUNT(*) FROM `sms` WHERE state='FALSE';");
				metroProgressBar1.Maximum=SMSCount;
			}
		}
		
		delegate void traerDatosDeDBDelegate();
		
		void traerDatosDeDB(){
			if(this.InvokeRequired){
				traerDatosDeDBDelegate delegado = new traerDatosDeDBDelegate(traerDatosDeDB);
				this.Invoke(delegado);
			}
			else
			{
				/*Ejecuto query*/
				dt.Clear();
				dt=dbConnection.executeQuery("SELECT phone_id,date_in,state,number,message,date_out FROM `sms`;");
				dt=mostrarDatos(dt);
			}
		}
		
		delegate void grabarEnDBDelegate(string phone_id);
		
		void grabarEnDB(string phone_id){
			if(this.InvokeRequired){
				grabarEnDBDelegate delegado = new grabarEnDBDelegate(grabarEnDB);
				object[] param = new object[] {phone_id};
				this.Invoke(delegado,param);
				
			}
			else
			{
				/*Actualizo el estado en la BD a TRUE (ENVIADO)*/
				dbConnection.executeQuery("UPDATE `sms` SET state='TRUE' WHERE phone_id ='"+phone_id+"';");
				
				/*Actualizo el campo FECHA OUT en la BD*/
				dateTime=DateTime.Now;
				dbConnection.executeQuery("UPDATE `sms` SET date_out='"+dateTime.ToString("yyyy-MM-dd HH:mm:ss")+"' WHERE phone_id ='"+phone_id+"';");
			}
		}
		
		
		/*Funcion encargada de procesar los SMS pendientes de enviar. Es llamada por el timer*/
		void procesarSMS(){
			
			
			if(dbConnection.getConnectionState()==ConnectionState.Open){
				
				/*Consulto cuantos SMS hay por mandar*/
				/*Seteo el valor máximo del progressBar*/
				contarElementosAProcesar();
				


				traerDatosDeDB();
				
				int i = 0; //Indicador de la fila actual
				int j = 0; //Cuenta cantidad de SMS procesados

				foreach (DataRow dr in dt.Rows){
					
					if (dr["state"].ToString() == "NO ENVIADO"){
						
						/*Amento cantidad de SMS procesados*/
						j++;
						
						/*Selecciono celda activa*/
						metroGrid1.Rows[i].Selected=true;
						
						/*Actualizo estado SMS en Grid*/
						metroGrid1.Rows[i].Cells[1].Value="ENVIANDO...";
						
						/*Scrollea 2 posiciones mas arriba de la celda seleccionada*/
						if (i<2){
							metroGrid1.FirstDisplayedScrollingRowIndex=i;
						}
						else
						{
							metroGrid1.FirstDisplayedScrollingRowIndex=i-2;
						}
						
						metroGrid1.Refresh();
						
						/*Actualizo label de contador SMS y progressBar*/
						label_estadoEnvio.Text="ENVIANDO...\r\n"+(j).ToString()+"/"+SMSCount.ToString();
						metroProgressBar1.Value=metroProgressBar1.Value+1;
						
						panel_progres.Refresh();
						
						
						#if DEBUG

						if(parche())

							#else
							/*Envio SMS*/
							if (sim900.enviarSMS(dr["number"].ToString(),dr["message"].ToString()))
								
								#endif
						{
							grabarEnDB(dr["phone_id"].ToString());
							
							/*Actualizo el estado en la Grid (para evitarme ejecutar la consulta de nuevo)*/
							metroGrid1.Rows[i].Cells[1].Value="ENVIADO";
							metroGrid1.Refresh();
							
						}
						else /*Error al enviar el mensaje*/
						{
							/*Actualizo el estado en la Grid*/
							metroGrid1.Rows[i].Cells[1].Value="ERROR";
							metroGrid1.Refresh();
							
							/*Actualizo label estado envio*/
							label_estadoEnvio.Text="ERROR AL ENVIAR";
							
							/*Muestro globito de error al enviar*/
							notifyIcon1.BalloonTipIcon=System.Windows.Forms.ToolTipIcon.Error;
							notifyIcon1.BalloonTipText="Error al enviar SMS";
							notifyIcon1.ShowBalloonTip(2000);
						}
						
						/*Desselecciono celda*/
						metroGrid1.Rows[i].Selected=false;
						
						
					}
					
					i++;
				}
				
			}
			
		}
		
		
		/*Ordenar DataTable. direction = ASC o DESC*/
		public static DataTable sortDataTable(DataTable dt, string colName, string direction)
		{
			dt.DefaultView.Sort = colName + " " + direction;
			dt = dt.DefaultView.ToTable();
			return dt;
		}
		
		/*Muestra datos en el grid*/
		public DataTable mostrarDatos(DataTable dt){
			
			/*Cambio el TRUE y FALSE por ENVIADO y NO ENVIADO*/
			foreach (DataRow dr in dt.Rows){
				if (dr["state"].ToString() == "TRUE"){
					dr["state"]="ENVIADO";
				}
				else
				{
					if (dr["state"].ToString() == "FALSE"){
						dr["state"]="NO ENVIADO";
					}
				}
			}
			/*Oculto la columna de ID*/
			dt.Columns[0].ColumnMapping = MappingType.Hidden;
			
			/*Ordeno por fecha DESC*/
			dt=sortDataTable(dt,"date_in","DESC");
			
			/*Actualizo grid*/
			metroGrid1.DataSource=dt;
			metroGrid1.Refresh();
			return dt;
		}
		
		/*Tick del timer*/
		void Timer1Tick(object sender, EventArgs e)
		{
			if(processing){
				/*Deshabilito el boton de parar procesamiento*/
				button_detenerProcesamiento.Enabled=false;
				
				/*Actualizo label estado*/
				label_estadoEnvio.Text="PROCESANDO...";
				panel_verSMS.Refresh();
				
				/*Actualizo cursor*/
				Cursor.Current=Cursors.WaitCursor;
				
				/*Creo tarea que envia los SMS*/
				Task task = new Task(procesarSMS);
				
				/*Inicio la tarea*/
				task.Start();
				
				/*Espero que termine sin bloquear UI*/
				await task;
				
				/*Actualizo label*/
				button_detenerProcesamiento.Enabled=true;
				label_estadoEnvio.Text="EN ESPERA";
				
				/*Actualizo cursor*/
				Cursor.Current=Cursors.Default;
				
				/*Comienzo timer nuevamente*/
				timer1.Enabled=true;
			}
		}
		
		/*Boton conectar al SIM*/
		void buttonConectarClick(object sender, EventArgs e)
		{
			/*Variable para controlar que esten OK todos los pasos de conexion SIM*/
			bool OK= true;
			
			/*Limpio labels*/
			AT_label.Text="";
			textBox_IMEI.Text="";
			CSQ_label.Text="";
			SMSReady_label.Text="";
			BDstatus_label.Text="";
			
			
			/*Muevo logo Cespi*/
			logoCespi.Location= new Point(10,360);
			
			/*Seteo cursor en espera*/
			Cursor.Current=Cursors.WaitCursor;
			
			/*Actualizo label estado*/
			label_estadoConexion.Text="CONECTANDO...";
			panel_conexion.Refresh();
			
			/*Refresco MainForm*/
			this.Refresh();
			
			/*Intento conectarme al puerto COM del textBox*/
			try{
				serialPort1.Close();
				serialPort1.PortName=textBox_port.Text;
				serialPort1.Open();
			}
			catch(Exception){
				MessageBox.Show("Puerto COM incorrecto","ERROR",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
			}
			
			#if DEBUG

			if (true)
				

				#else
				/*Si el puerto es correcto y el SIM esta conectado*/
				if (serialPort1.IsOpen)
					

					#endif
			{
				/*Actualizo label estado conexion*/
				label_estadoConexion.Text="CONECTADO";
				panel_estado.Show();
				
				/*Agrando el form*/
				this.Size = new Size(327,307);
				
				/*Refresco el MainForm*/
				this.Refresh();
				#if !DEBUG
				
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
					SMSReady_label.Text="LISTO PARA ENVIAR";
				}
				else
				{
					SMSReady_label.Text="ERROR PARA ENVIAR";
					OK=false;
				}
				#endif
				panel_estado.Refresh();
				
				
				
				
				#if DEBUG
				OK=true;
				#endif

				/*Si los comandos SIM dieron todos OK*/
				if(OK){

					/*Desactivo boton conectar*/
					button_conectar.Enabled=false;
					
					/*Deshabilito textbox puerto*/
					textBox_port.Enabled=false;

					/*Seteo parametros*/
					dbConnection.setdatabaseName("testcsharp");
					dbConnection.setServer("localhost");
					dbConnection.setdatabaseUser("root");
					dbConnection.setdatabasePassword("");
					
					/*Me conecto a la DB*/
					if (dbConnection.DBConnect()){
						
						/*Agrando para ver el dataGrid*/
						this.Size = new Size(725, 400);
						
						/*Ejecuto query*/
						dt.Clear();
						dt=dbConnection.executeQuery("SELECT phone_id,date_in,state,number,message,date_out FROM `sms`;");
						dt=mostrarDatos(dt);
						
						
						/*Muestro resultados*/
						metroGrid1.Columns[0].HeaderText="Fecha IN";
						metroGrid1.Columns[1].HeaderText="Estado";
						metroGrid1.Columns[2].HeaderText="Número";
						metroGrid1.Columns[3].HeaderText="Mensaje";
						metroGrid1.Columns[4].HeaderText="Fecha OUT";
						
						metroGrid1.Visible=true;
						
						/*Muevo logo Cespi*/
						logoCespi.Location= new Point(650,450);
						
						/*Oculto boton de "Ver SMS"*/
						panel_verSMS.Visible=true;
						button_verSMS.Visible=false;
						button_procesar_SMS.Visible=true;
						button_detenerProcesamiento.Visible=true;
						button_detenerProcesamiento.Enabled=false;
						
						/*Actualizo label de estado*/
						BDstatus_label.Text="CONEXION A BD:\r\nOK";
					}
					
					else /*Error al conectar con DB*/
					{
						/*Actualizo label de estado*/
						BDstatus_label.Text="CONEXION A BD:\r\nERROR";
					}
					
				}
				
				else/*Si fallo algun comando SIM*/
				{
					/*Muestro panel estado y panel ver SMS*/
					panel_verSMS.Visible=true;
					button_verSMS.Visible=true;
					button_verSMS.Location=new Point(6,0);
					button_detenerProcesamiento.Visible=false;
					button_procesar_SMS.Visible=false;
					
				}


			}
			
			
			else /*Puerto serial no abierto*/
			{
				label_estadoConexion.Text="DESCONECTADO";
				panel_estado.Hide();

				
			}
			
			/*Seteo cursor por default*/
			Cursor.Current=Cursors.Default;
			logoCespi.Visible=true;
		}
		
		/*Boton de ver SMS*/
		void verSMSClick(object sender, EventArgs e)
		{
			/*Seteo parametros de DB*/
			dbConnection.setdatabaseName("testcsharp");
			dbConnection.setServer("localhost");
			dbConnection.setdatabaseUser("root");
			dbConnection.setdatabasePassword("");
			
			/*Me conecto a la DB*/
			if (dbConnection.DBConnect()){

				/*Ejecuto query*/
				dt.Clear();
				dt=dbConnection.executeQuery("SELECT phone_id,date_in,state,number,message,date_out FROM `sms`;");
				dt=mostrarDatos(dt);
				
				
				/*Formateo el datagrid con los headers*/
				metroGrid1.Columns[0].HeaderText="Fecha IN";
				metroGrid1.Columns[1].HeaderText="Estado";
				metroGrid1.Columns[2].HeaderText="Número";
				metroGrid1.Columns[3].HeaderText="Mensaje";
				metroGrid1.Columns[4].HeaderText="Fecha OUT";
				
				/*Muestro resultados*/
				metroGrid1.Visible=true;
				
				/*Actualizo label de estado*/
				BDstatus_label.Text="CONEXION A BD:\r\nOK";
				
				/*Muevo logo Cespi*/
				logoCespi.Location= new Point(650,450);
			}
			else /*Falla el DBConnect*/
			{
				/*Actualizo label de estado*/
				BDstatus_label.Text="CONEXION A BD:\r\nERROR";
			}
		}
		
		
		/*Boton detener procesamiento SMS*/
		void DetenerProcesamientoClick(object sender, EventArgs e)
		{
			/*Cambio el enabled de los botones*/
			button_detenerProcesamiento.Enabled=false;
			button_procesar_SMS.Enabled=true;
			
			processing = false;
			label_estadoEnvio.Text="";
			
			/*Oculto el progress y el label del estado envio*/
			label_estadoEnvio.Visible=false;
			metroProgressBar1.Visible=false;
			
			/*Detengo el timer*/
			timer1.Enabled=false;
			
		}
		
		private void ShowMessageBox()
		{
			MessageBox.Show("Hello world.");
		}

		/*Boton de procesar SMS*/
		async void procesar_SMS_Click(object sender, EventArgs e)
		{
			//Deshabilito los botones de procesar/no procesar
			button_detenerProcesamiento.Enabled=false;
			button_procesar_SMS.Enabled=false;
			
			/*Muestro paneles progreso*/
			panel_progres.Visible=true;
			label_estadoEnvio.Visible=true;
			metroProgressBar1.Visible=true;
			metroProgressBar1.Value=0;
			panel_progres.Refresh();
			
			/*Actualizo label*/
			label_estadoEnvio.Text="PROCESANDO...";
			panel_verSMS.Refresh();
			
			/*Cursor en modo espera*/
			Cursor.Current=Cursors.WaitCursor;
			
			/*Refresco MainForm*/
			this.Refresh();
			
			/*Proceso los SMS*/
			processing= true;
			
			/*Creo tarea que envia los SMS*/
			Task task = new Task(procesarSMS);
			
			/*Inicio la tarea*/
			task.Start();
			
			/*Espero que termine sin bloquear UI*/
			await task;
			
			
			/*FIN proceso SMS*/
			/*Actualizo label estado*/
			label_estadoEnvio.Text="EN ESPERA";
			
			/*Habilito boton STOP*/
			button_detenerProcesamiento.Enabled=true;
			
			/*Oculto barra progreso*/
			metroProgressBar1.Visible=false;
			Cursor.Current=Cursors.Default;
			
			
			/*Activo el timer*/
			timer1.Interval=timerInterval;
			timer1.Enabled=true;
			
		}
		
		/*Eventos de cambio del MainForm*/
		void MainFormResize(object sender, EventArgs e)
		{
			/*Si el mainForm esta miniminizado*/
			if (this.WindowState == FormWindowState.Minimized)
			{
				/*Escondo el MainForm*/
				Hide();
				
				/*Muestro el icono*/
				notifyIcon1.Visible = true;
				
				/*Muestro globito avisando que sigue funcionando*/
				notifyIcon1.BalloonTipIcon=System.Windows.Forms.ToolTipIcon.Info;
				notifyIcon1.BalloonTipText="Gateway SMS sigue funcionando";
				notifyIcon1.ShowBalloonTip(1300);
			}
		}
		
		/*Doble Click en el notify icon*/
		void NotifyIcon1MouseDoubleClick(object sender, MouseEventArgs e)
		{
			Show();  //Muestro el MainForm
			this.WindowState = FormWindowState.Normal;
		}
	}
	
	
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
	
	/*Para manejar el módulo SIM*/
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
		
		/*Espera hasta encontrar el string deseado dentro de el buffer*/
		public void waitForResult(String response){
			while((getReceivedBuffer().IndexOf(response)==-1)){
				System.Threading.Thread.Sleep(100);
			}
		}
		
		
		/*  Envio de comandos
		 * 		Return = null 	--> Comando falló
		 *		Return !=null 	--> Comando OK
		 */
		String sendCommand(String command, int delaySec, String response){
			
			string received = "";
			receivedBuffer="";
			
			/*Envio Comando*/
			serialPort.WriteLine((command+"\r\n"));
			
			/*Espero hasta que pase el tiempo delaySec o encuentre el string response en la respuesta*/
			var task = Task.Factory.StartNew (() => waitForResult(response));
			if (task.Wait(TimeSpan.FromSeconds(delaySec))){
				/*Encontro string*/
				received=receivedBuffer;
				
			}
			else /*No Encontro string*/
			{
				
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
				}while ((result[i] != ':') && ( i<result.Length-1));
				
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


