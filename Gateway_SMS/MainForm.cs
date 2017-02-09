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
 * Tiempo de procesamiento aproximado: 10" por SMS
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

		/*Mantiene la cantidad de elementos a procesar*/
		int SMSCount;
		
		/*Mantiene la cantidad de errores al intentar enviar un mensaje*/
		int errorCount;
		
		/*Constante de maxima cantidad de errores antes de intentar reconectar*/
		const int MAX_ERROR = 4;
		

		
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
			
			
			/*Inicializo contador de errores*/
			errorCount=0;
			
			//Oculto dataGrid
			metroGrid1.Visible=false;
			
			
			
			
			
			//Muestro panel de ver SMS solo con boton de ver SMS
			button_detenerProcesamiento.Visible=false;

			panel_estado.Visible=false;

			
			/*Refresco MainForm*/
			this.Refresh();
			
			/*Agrego opciones al menu contextual*/
			contextMenuStrip.Items.Add("Abrir");
			contextMenuStrip.Items.Add("Cerrar");
			
			/*Asigno metodo al click del item del metodo contextual*/
			contextMenuStrip.ItemClicked += new ToolStripItemClickedEventHandler(contextMenuStrip_Clicked);
			
			/*Asocio el menu contextual al notifyIcon*/
			notifyIcon1.ContextMenuStrip=contextMenuStrip;
			
			/*Muestro el icono*/
			notifyIcon1.Visible = true;
			
			/*Ignoro los errores de que un Thread modifica un objeto que no creo*/
			CheckForIllegalCrossThreadCalls = false;
			
			this.button_procesar_SMS.Click += new System.EventHandler(this.verSMS_Click);
			
			
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
		
		bool SMSready(){
			bool OK = false;
			if (dbConnection.getConnectionState()==ConnectionState.Open){
				if (serialPort1.IsOpen){
					if(sim900.connectSIM900()){
						OK=true;
					}
				}
			}

			return OK;
		}
		
		/*Funcion encargada de procesar los SMS pendientes de enviar. Es llamada por el timer*/
		void procesarSMS(){
	
			
			if(SMSready())
			{
				
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
							
							/*Si manda mensaje limpio el contador de errores*/
							errorCount=0;
							
						}
						else /*Error al enviar el mensaje*/
						{
							/*Actualizo el estado en la Grid*/
							metroGrid1.Rows[i].Cells[1].Value="ERROR";
							metroGrid1.Refresh();
							
							/*Actualizo label estado envio*/
							label_estadoEnvio.Text="ERROR AL ENVIAR";
							
							/*Muestro globito de error al enviar*/
							notifyIcon_ShowMessage(6000,"Error al enviar SMS","warning");
							
							/*Sumo variable de cuenta de errores de envio*/
							errorCount++;
						}
						
						/*Desselecciono celda*/
						metroGrid1.Rows[i].Selected=false;
						
					}
					
					i++;
				}
				
			}
			else
			{
				errorCount=MAX_ERROR;
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
		
		/*Mostrar notificacion en la barra de tareas.
		 *@param
		 *	time: tiempo
		 * 	text: texto mostrado
		 * 	type: "warning" --> Alerta. Cualquier otro se muestra como info
		 */
		void notifyIcon_ShowMessage(int time,string text,string type){
			
			/*Seteo tipo de mensaje*/
			if (type=="warning"){
				notifyIcon1.BalloonTipIcon=ToolTipIcon.Warning;
			}
			else
			{
				notifyIcon1.BalloonTipIcon=ToolTipIcon.Info;
			}
			/*Seteo texto*/
			notifyIcon1.BalloonTipText=text;
			
			/*Muestro globo el tiempo indicado*/
			notifyIcon1.ShowBalloonTip(time);
		}
		
		bool reconnectSIM(){
			
			bool OK= true;
			bool finWhile=false;
			
			string mensajeError="";
			
			int reconnectCount = 0;
			
			/*Cambio el enabled de los botones*/
			button_detenerProcesamiento.Enabled=false;
			button_procesar_SMS.Enabled=false;
			
			
			label_estadoEnvio.Text="ERROR DE ENVÍO\r\nRECONECTANDO...";
			label_estadoEnvio.Visible=true;
			this.Refresh();
			
			
			processing=false;
			
			while (reconnectCount < 2 & !finWhile){
				
				reconnectCount++;
				
				label_estadoEnvio.Text=	"ERROR DE CONEXIÓN\r\n" +
					"RECONECTANDO... "+reconnectCount.ToString()+"/2";
				
				
				if (serialPort1.IsOpen){
					try{
						serialPort1.Close();
						serialPort1.PortName=Properties.Settings1.Default.puertoCOM;
						serialPort1.Open();
					}
					catch(Exception){
						mensajeError="PUERTO COM INCORRECTO";
						OK=false;
					}
				}
				else
				{
					mensajeError="PUERTO COM INCORRECTO";
					OK=false;
				}
				
				if (serialPort1.IsOpen){
					/*Intento conectarme al SIM*/
					if(sim900.connectSIM900()){
						/*Intento adquirir IMEI*/
						if(sim900.setIMEI()){
							/*Intento adquirir señal*/
							if (sim900.setSignal()){
								CSQ_label.Text="SEÑAL:"+sim900.getSignal();
								/*Intento setear modo SMS*/
								if (sim900.prepareSMS()){
									finWhile=true;
								}
								else
								{
									label_estadoEnvio.Text=	"ERROR\r\nNO LISTO PARA ENVIAR SMS\r\n" +
										"RECONECTANDO "+reconnectCount.ToString()+"/2";
									mensajeError="MÓDULO GMS NO LISTO PARA ENVIAR SMS";
									OK=false;
								}
							}
							else
							{
								label_estadoEnvio.Text=	"ERROR\r\nSIM APAGADO\r\n" +
									"RECONECTANDO "+reconnectCount.ToString()+"/2";
								mensajeError="MÓDULO GMS APAGADO";
								OK=false;
							}
						}
						else
						{
							label_estadoEnvio.Text=	"ERROR\r\nSIM APAGADO\r\n" +
								"RECONECTANDO "+reconnectCount.ToString()+"/2";
							mensajeError="MÓDULO GMS APAGADO";
							OK=false;
						}
						
					}
					else
					{
						label_estadoEnvio.Text=	"ERROR\r\nSIM APAGADO\r\n" +
							"RECONECTANDO "+reconnectCount.ToString()+"/2";
						mensajeError="MÓDULO GMS APAGADO";
						OK=false;
					}
				}

			}
			/*Si todo dio OK*/
			if (OK){
				
				processing=true;
				timer1.Enabled=true;
				label_estadoEnvio.Text="ES ESPERA...";
				label_estadoConexion.Text="CONECTADO";
				
				/*Desactivo boton conectar*/
				button_conectar.Enabled=false;

				return true;
			}
			else
			{
				label_estadoEnvio.Text=	"ERROR\r\n"+mensajeError;
				label_estadoConexion.Text="DESCONECTADO";
				button_conectar.Enabled=true;
				/*Habilito boton config*/
				config_button.Enabled=true;
				MessageBox.Show(mensajeError,"ERROR",MessageBoxButtons.OK,MessageBoxIcon.Error);
				notifyIcon_ShowMessage(4000,mensajeError,"warning");
				return false;
			}
		}
		
		/*Tick del timer*/
		async void Timer1Tick(object sender, EventArgs e)
		{
			timer1.Enabled=false;
			if(processing){

				/*Deshabilito los botones de procesar/no procesar*/
				button_detenerProcesamiento.Enabled=false;
				button_procesar_SMS.Enabled=false;
				
				
				/*Muestro paneles progreso*/
				panel_progres.Visible=true;
				label_estadoEnvio.Visible=true;
				metroProgressBar1.Visible=true;
				metroProgressBar1.Value=0;
				panel_progres.Refresh();
				
				/*Actualizo label estado*/
				label_estadoEnvio.Text="PROCESANDO...";
				panel_verSMS.Refresh();
				
				/*Refresco MainForm*/
				this.Refresh();
				
				/*Actualizo cursor*/
				Cursor.Current=Cursors.WaitCursor;
				
				/*Creo tarea que envia los SMS*/
				Task task = new Task(procesarSMS);
				
				/*Inicio la tarea*/
				task.Start();
				
				/*Espero que termine sin bloquear UI*/
				await task;
				
				/*FIN proceso SMS*/
				
				/*Habilito boton STOP*/
				button_detenerProcesamiento.Enabled=true;
				
				/*Actualizo label*/
				label_estadoEnvio.Text="EN ESPERA";
				
				/*Actualizo cursor*/
				Cursor.Current=Cursors.Default;
				
				/*Oculto barra progreso*/
				metroProgressBar1.Visible=false;
				
				
				/*Si se produjeron MAX_ERROR de errores de envio seguidos*/
				if (errorCount>= MAX_ERROR){
					
					/*Paro el timer, por lo tanto no se vuelve a ejecutar este evento*/
					timer1.Enabled=false;
					reconnectSIM();
				}
				else
				{
					/*Comienzo timer nuevamente*/
					timer1.Enabled=true;
				}
			}
		}
		
		/*Boton conectar al SIM*/
		void buttonConectarClick(object sender, EventArgs e)
		{
			/*Variable para controlar que esten OK todos los pasos de conexion SIM*/
			bool OK= true;
			
			/*Limpio labels*/
			AT_label.Text="";
			CSQ_label.Text="";
			SMSReady_label.Text="";
			BDstatus_label.Text="";
			
			/*Oculto label de estado envio*/
			label_estadoEnvio.Visible=false;
			
			
			/*Seteo cursor en espera*/
			Cursor.Current=Cursors.WaitCursor;
			
			/*Actualizo label estado*/
			label_estadoConexion.Text="CONECTANDO...";
			panel_conexion.Refresh();
			
			/*Deshailito boton de config*/
			config_button.Enabled=false;
			
			/*Refresco MainForm*/
			this.Refresh();
			
			/*Intento conectarme al puerto COM del textBox*/
			try{
				serialPort1.Close();
				serialPort1.PortName=Properties.Settings1.Default.puertoCOM;
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
				
				
				/*Refresco el MainForm*/
				this.Refresh();
				#if !DEBUG
				
				/*Intento conectarme al SIM*/
				if(sim900.connectSIM900()){
					AT_label.Text="SIM900 ONLINE";
					
					/*Intento adquirir IMEI*/
					if(sim900.setIMEI()){
						
						
						/*Intento adquirir señal*/
						if (sim900.setSignal()){
							CSQ_label.Text="SEÑAL:"+sim900.getSignal();
							
							/*Intento setear modo SMS*/
							if (sim900.prepareSMS()){
								SMSReady_label.Text="LISTO PARA ENVIAR";
							}
							else
							{
								SMSReady_label.Text="ERROR PARA ENVIAR";
								OK=false;
							}
						}
						else
						{
							CSQ_label.Text="ERROR EN SEÑAL";
							SMSReady_label.Text="ERROR PARA ENVIAR";
							OK=false;
						}
					}
					else
					{

						CSQ_label.Text="ERROR EN SEÑAL";
						SMSReady_label.Text="ERROR PARA ENVIAR";
						OK=false;
					}
					
				}
				else
				{
					AT_label.Text="SIM900 OFFLINE";
					CSQ_label.Text="ERROR EN SEÑAL";
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

					/*Seteo parametros*/
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
						
						/*Muestro resultados*/
						metroGrid1.Columns[0].HeaderText="Fecha IN";
						metroGrid1.Columns[1].HeaderText="Estado";
						metroGrid1.Columns[2].HeaderText="Número";
						metroGrid1.Columns[3].HeaderText="Mensaje";
						metroGrid1.Columns[4].HeaderText="Fecha OUT";
						
						metroGrid1.Visible=true;
						
						
						/*Oculto boton de "Ver SMS"*/
						panel_verSMS.Visible=true;
						button_detenerProcesamiento.Visible=true;
						button_detenerProcesamiento.Enabled=false;
						button_procesar_SMS.Enabled=true;
						
						button_procesar_SMS.Text="Procesar SMS";
						button_procesar_SMS.Click-=verSMS_Click;
						button_procesar_SMS.Click+=procesar_SMS_Click;
						
						
						/*Refresco MainForm*/
						this.Refresh();
						
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

					button_detenerProcesamiento.Visible=false;
					config_button.Enabled=true;
				}
			}
			else /*Puerto serial no abierto*/
			{
				label_estadoConexion.Text="DESCONECTADO";
				panel_estado.Hide();
				config_button.Enabled=true;
			}
			
			/*Seteo cursor por default*/
			Cursor.Current=Cursors.Default;
			
		}
		
		/*Boton de ver SMS*/
		void verSMS_Click(object sender, EventArgs e)
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
			/*Habilito boton config*/
			config_button.Enabled=true;
			
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

		/*Boton de procesar SMS*/
		async void procesar_SMS_Click(object sender, EventArgs e)
		{
			//Deshabilito los botones de procesar/no procesar
			button_detenerProcesamiento.Enabled=false;
			button_procesar_SMS.Enabled=false;
			
			/*Deshabilito boton config*/
			config_button.Enabled=false;
			
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
			timer1.Interval=Properties.Settings1.Default.delay*1000; /*Paso de s a ms*/
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

				/*Muestro globito avisando que sigue funcionando*/
				notifyIcon_ShowMessage(2000,"Gateway SMS sigue funcionando","info");
			}
		}
		
		/*Doble Click en el notify icon*/
		void NotifyIcon1MouseDoubleClick(object sender, MouseEventArgs e)
		{
			Show();  //Muestro el MainForm
			this.WindowState = FormWindowState.Normal;
		}
		void Button1Click(object sender, EventArgs e)
		{
			Config_Form config_form = new Config_Form();
			config_form.ShowDialog();
			
			if(config_form.DialogResult==DialogResult.OK){
				Properties.Settings1.Default.delay=config_form.delay;
				Properties.Settings1.Default.puertoCOM=config_form.puertoCOM;
				Properties.Settings1.Default.Save();
				
			}
		}
	}
}


