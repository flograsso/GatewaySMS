/*
 * Created by SharpDevelop.
 * User: SoporteSEM
 * Date: 04/01/2017
 * Time: 10:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using MetroFramework.Controls;
using MetroFramework.Components;
using MetroFramework.Fonts;
namespace Gateway_SMS
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private MetroFramework.Controls.MetroPanel panel_conexion;
		private System.Windows.Forms.TextBox textBox_IMEI;
		private MetroFramework.Controls.MetroLabel label5;
		private MetroFramework.Controls.MetroLabel label_estadoConexion;
		private MetroFramework.Controls.MetroLabel label3;
		private System.Windows.Forms.TextBox textBox_port;
		private MetroFramework.Controls.MetroLabel label2;
		private MetroFramework.Controls.MetroPanel panel_estado;
		private MetroFramework.Controls.MetroLabel SMSReady_label;
		private MetroFramework.Controls.MetroLabel CSQ_label;
		private MetroFramework.Controls.MetroLabel AT_label;
		private System.Windows.Forms.DataGridView dataGridView1;
		private MetroFramework.Controls.MetroPanel panel_verSMS;
		private MetroFramework.Controls.MetroLabel label8;
		private MetroFramework.Controls.MetroLabel label4;
		private MetroFramework.Controls.MetroLabel label6;
		private MetroFramework.Controls.MetroLabel label7;
		private MetroFramework.Controls.MetroLabel BDstatus_label;
		private System.Windows.Forms.Timer timer1;
		private MetroFramework.Controls.MetroLabel label_estadoEnvio;
		private MetroFramework.Controls.MetroLabel Label1;
		private MetroFramework.Controls.MetroTile button_conectar;
		private MetroFramework.Controls.MetroTile button_verSMS;
		private MetroFramework.Controls.MetroTile button_detenerProcesamiento;
		private MetroFramework.Controls.MetroTile button_procesar_SMS;
	



		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		private void InitializeComponent() 
		{
			this.components = new System.ComponentModel.Container();
			this.panel_conexion = new MetroFramework.Controls.MetroPanel();
			this.button_conectar = new MetroFramework.Controls.MetroTile();
			this.Label1 = new MetroFramework.Controls.MetroLabel();
			this.textBox_IMEI = new System.Windows.Forms.TextBox();
			this.label5 = new MetroFramework.Controls.MetroLabel();
			this.label_estadoConexion = new MetroFramework.Controls.MetroLabel();
			this.label3 = new MetroFramework.Controls.MetroLabel();
			this.textBox_port = new System.Windows.Forms.TextBox();
			this.label2 = new MetroFramework.Controls.MetroLabel();
			this.panel_verSMS = new MetroFramework.Controls.MetroPanel();
			this.button_procesar_SMS = new MetroFramework.Controls.MetroTile();
			this.button_verSMS = new MetroFramework.Controls.MetroTile();
			this.button_detenerProcesamiento = new MetroFramework.Controls.MetroTile();
			this.label_estadoEnvio = new MetroFramework.Controls.MetroLabel();
			this.label8 = new MetroFramework.Controls.MetroLabel();
			this.label4 = new MetroFramework.Controls.MetroLabel();
			this.label6 = new MetroFramework.Controls.MetroLabel();
			this.label7 = new MetroFramework.Controls.MetroLabel();
			this.panel_estado = new MetroFramework.Controls.MetroPanel();
			this.BDstatus_label = new MetroFramework.Controls.MetroLabel();
			this.SMSReady_label = new MetroFramework.Controls.MetroLabel();
			this.CSQ_label = new MetroFramework.Controls.MetroLabel();
			this.AT_label = new MetroFramework.Controls.MetroLabel();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.panel_conexion.SuspendLayout();
			this.panel_verSMS.SuspendLayout();
			this.panel_estado.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// panel_conexion
			// 
			this.panel_conexion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel_conexion.Controls.Add(this.button_conectar);
			this.panel_conexion.Controls.Add(this.Label1);
			this.panel_conexion.Controls.Add(this.textBox_IMEI);
			this.panel_conexion.Controls.Add(this.label5);
			this.panel_conexion.Controls.Add(this.label_estadoConexion);
			this.panel_conexion.Controls.Add(this.label3);
			this.panel_conexion.Controls.Add(this.textBox_port);
			this.panel_conexion.Controls.Add(this.label2);
			this.panel_conexion.HorizontalScrollbarBarColor = true;
			this.panel_conexion.HorizontalScrollbarHighlightOnWheel = false;
			this.panel_conexion.HorizontalScrollbarSize = 10;
			this.panel_conexion.Location = new System.Drawing.Point(10, 70);
			this.panel_conexion.Name = "panel_conexion";
			this.panel_conexion.Size = new System.Drawing.Size(306, 133);
			this.panel_conexion.TabIndex = 1;
			this.panel_conexion.VerticalScrollbarBarColor = true;
			this.panel_conexion.VerticalScrollbarHighlightOnWheel = false;
			this.panel_conexion.VerticalScrollbarSize = 10;
			// 
			// button_conectar
			// 
			this.button_conectar.ActiveControl = null;
			this.button_conectar.Location = new System.Drawing.Point(170, 71);
			this.button_conectar.Name = "button_conectar";
			this.button_conectar.Size = new System.Drawing.Size(100, 50);
			this.button_conectar.TabIndex = 8;
			this.button_conectar.Text = "Conectar";
			this.button_conectar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.button_conectar.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.button_conectar.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
			this.button_conectar.UseSelectable = true;
			this.button_conectar.Click += new System.EventHandler(this.buttonConectarClick);
			// 
			// Label1
			// 
			this.Label1.FontWeight = MetroFramework.MetroLabelWeight.Bold;
			this.Label1.Location = new System.Drawing.Point(8, 6);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(123, 26);
			this.Label1.TabIndex = 6;
			this.Label1.Text = "Conexión SIM:";
			this.Label1.Theme = MetroFramework.MetroThemeStyle.Light;
			// 
			// textBox_IMEI
			// 
			this.textBox_IMEI.Location = new System.Drawing.Point(148, 45);
			this.textBox_IMEI.Name = "textBox_IMEI";
			this.textBox_IMEI.ReadOnly = true;
			this.textBox_IMEI.Size = new System.Drawing.Size(122, 20);
			this.textBox_IMEI.TabIndex = 7;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(107, 45);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(57, 26);
			this.label5.TabIndex = 6;
			this.label5.Text = "IMEI:";
			// 
			// label_estadoConexion
			// 
			this.label_estadoConexion.FontWeight = MetroFramework.MetroLabelWeight.Regular;
			this.label_estadoConexion.Location = new System.Drawing.Point(57, 84);
			this.label_estadoConexion.Name = "label_estadoConexion";
			this.label_estadoConexion.Size = new System.Drawing.Size(122, 26);
			this.label_estadoConexion.TabIndex = 4;
			this.label_estadoConexion.Text = "DESCONECTADO";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 84);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(58, 23);
			this.label3.TabIndex = 3;
			this.label3.Text = "Estado:";
			// 
			// textBox_port
			// 
			this.textBox_port.Location = new System.Drawing.Point(60, 45);
			this.textBox_port.Name = "textBox_port";
			this.textBox_port.Size = new System.Drawing.Size(41, 20);
			this.textBox_port.TabIndex = 2;
			this.textBox_port.Text = "COM5";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 46);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(57, 26);
			this.label2.TabIndex = 1;
			this.label2.Text = "Puerto:";
			// 
			// panel_verSMS
			// 
			this.panel_verSMS.AutoSize = true;
			this.panel_verSMS.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel_verSMS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel_verSMS.Controls.Add(this.button_procesar_SMS);
			this.panel_verSMS.Controls.Add(this.button_verSMS);
			this.panel_verSMS.Controls.Add(this.button_detenerProcesamiento);
			this.panel_verSMS.Controls.Add(this.label_estadoEnvio);
			this.panel_verSMS.Controls.Add(this.label8);
			this.panel_verSMS.Controls.Add(this.label4);
			this.panel_verSMS.Controls.Add(this.label6);
			this.panel_verSMS.Controls.Add(this.label7);
			this.panel_verSMS.HorizontalScrollbarBarColor = true;
			this.panel_verSMS.HorizontalScrollbarHighlightOnWheel = false;
			this.panel_verSMS.HorizontalScrollbarSize = 10;
			this.panel_verSMS.Location = new System.Drawing.Point(10, 308);
			this.panel_verSMS.MinimumSize = new System.Drawing.Size(306, 2);
			this.panel_verSMS.Name = "panel_verSMS";
			this.panel_verSMS.Size = new System.Drawing.Size(306, 162);
			this.panel_verSMS.TabIndex = 5;
			this.panel_verSMS.VerticalScrollbarBarColor = true;
			this.panel_verSMS.VerticalScrollbarHighlightOnWheel = false;
			this.panel_verSMS.VerticalScrollbarSize = 10;
			// 
			// button_procesar_SMS
			// 
			this.button_procesar_SMS.ActiveControl = null;
			this.button_procesar_SMS.Location = new System.Drawing.Point(3, 31);
			this.button_procesar_SMS.Name = "button_procesar_SMS";
			this.button_procesar_SMS.Size = new System.Drawing.Size(145, 60);
			this.button_procesar_SMS.TabIndex = 11;
			this.button_procesar_SMS.Text = "Procesar SMS";
			this.button_procesar_SMS.UseSelectable = true;
			this.button_procesar_SMS.Click += new System.EventHandler(this.procesar_SMS_Click);
			// 
			// button_verSMS
			// 
			this.button_verSMS.ActiveControl = null;
			this.button_verSMS.Location = new System.Drawing.Point(78, 97);
			this.button_verSMS.Name = "button_verSMS";
			this.button_verSMS.Size = new System.Drawing.Size(145, 60);
			this.button_verSMS.TabIndex = 0;
			this.button_verSMS.Text = "Ver SMS";
			this.button_verSMS.UseSelectable = true;
			this.button_verSMS.Click += new System.EventHandler(this.verSMSClick);
			// 
			// button_detenerProcesamiento
			// 
			this.button_detenerProcesamiento.ActiveControl = null;
			this.button_detenerProcesamiento.Location = new System.Drawing.Point(154, 31);
			this.button_detenerProcesamiento.Name = "button_detenerProcesamiento";
			this.button_detenerProcesamiento.Size = new System.Drawing.Size(145, 60);
			this.button_detenerProcesamiento.TabIndex = 10;
			this.button_detenerProcesamiento.Text = "Detener \r\nprocesamiento";
			this.button_detenerProcesamiento.UseSelectable = true;
			this.button_detenerProcesamiento.Click += new System.EventHandler(this.DetenerProcesamientoClick);
			// 
			// label_estadoEnvio
			// 
			this.label_estadoEnvio.Location = new System.Drawing.Point(100, 68);
			this.label_estadoEnvio.Name = "label_estadoEnvio";
			this.label_estadoEnvio.Size = new System.Drawing.Size(100, 23);
			this.label_estadoEnvio.TabIndex = 8;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(0, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(86, 23);
			this.label8.TabIndex = 4;
			this.label8.Text = "SMS:";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(0, 65);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(179, 23);
			this.label4.TabIndex = 3;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(28, 65);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(135, 23);
			this.label6.TabIndex = 2;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(0, 19);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(135, 23);
			this.label7.TabIndex = 0;
			// 
			// panel_estado
			// 
			this.panel_estado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel_estado.Controls.Add(this.BDstatus_label);
			this.panel_estado.Controls.Add(this.SMSReady_label);
			this.panel_estado.Controls.Add(this.CSQ_label);
			this.panel_estado.Controls.Add(this.AT_label);
			this.panel_estado.HorizontalScrollbarBarColor = true;
			this.panel_estado.HorizontalScrollbarHighlightOnWheel = false;
			this.panel_estado.HorizontalScrollbarSize = 10;
			this.panel_estado.Location = new System.Drawing.Point(10, 209);
			this.panel_estado.Name = "panel_estado";
			this.panel_estado.Size = new System.Drawing.Size(306, 93);
			this.panel_estado.TabIndex = 2;
			this.panel_estado.VerticalScrollbarBarColor = true;
			this.panel_estado.VerticalScrollbarHighlightOnWheel = false;
			this.panel_estado.VerticalScrollbarSize = 10;
			this.panel_estado.Visible = false;
			// 
			// BDstatus_label
			// 
			this.BDstatus_label.AutoSize = true;
			this.BDstatus_label.Location = new System.Drawing.Point(189, 9);
			this.BDstatus_label.Name = "BDstatus_label";
			this.BDstatus_label.Size = new System.Drawing.Size(102, 38);
			this.BDstatus_label.TabIndex = 4;
			this.BDstatus_label.Text = "CONEXION BD:\r\nOK";
			// 
			// SMSReady_label
			// 
			this.SMSReady_label.AutoSize = true;
			this.SMSReady_label.Location = new System.Drawing.Point(0, 55);
			this.SMSReady_label.Name = "SMSReady_label";
			this.SMSReady_label.Size = new System.Drawing.Size(35, 19);
			this.SMSReady_label.TabIndex = 3;
			this.SMSReady_label.Text = "SMS";
			// 
			// CSQ_label
			// 
			this.CSQ_label.AutoSize = true;
			this.CSQ_label.Location = new System.Drawing.Point(0, 32);
			this.CSQ_label.Name = "CSQ_label";
			this.CSQ_label.Size = new System.Drawing.Size(36, 19);
			this.CSQ_label.TabIndex = 2;
			this.CSQ_label.Text = "CSQ";
			// 
			// AT_label
			// 
			this.AT_label.AutoSize = true;
			this.AT_label.Location = new System.Drawing.Point(0, 9);
			this.AT_label.Name = "AT_label";
			this.AT_label.Size = new System.Drawing.Size(25, 19);
			this.AT_label.TabIndex = 0;
			this.AT_label.Text = "AT";
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(331, 70);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.Size = new System.Drawing.Size(369, 275);
			this.dataGridView1.TabIndex = 3;
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.Timer1Tick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
			this.ClientSize = new System.Drawing.Size(761, 582);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.panel_estado);
			this.Controls.Add(this.panel_conexion);
			this.Controls.Add(this.panel_verSMS);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "Gateway SMS";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainFormFormClosed);
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.panel_conexion.ResumeLayout(false);
			this.panel_conexion.PerformLayout();
			this.panel_verSMS.ResumeLayout(false);
			this.panel_estado.ResumeLayout(false);
			this.panel_estado.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
