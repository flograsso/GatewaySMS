/*
 * Created by SharpDevelop.
 * User: SoporteSEM
 * Date: 04/01/2017
 * Time: 10:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Gateway_SMS
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Panel panel_conexion;
		private System.Windows.Forms.TextBox textBox_IMEI;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button button_conectar;
		private System.Windows.Forms.Label label_estadoConexion;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox_port;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel_estado;
		private System.Windows.Forms.Label SMSReady_label;
		private System.Windows.Forms.Label CSQ_label;
		private System.Windows.Forms.Label AT_label;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.Button button_verSMS;
		private System.Windows.Forms.Panel panel_verSMS;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button button_procesarSMS;
		private System.Windows.Forms.Button button_pararProcesarSMS;
		private System.Windows.Forms.Label BDstatus_label;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Label label_estadoEnvio;



		
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
			this.panel_conexion = new System.Windows.Forms.Panel();
			this.textBox_IMEI = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.button_conectar = new System.Windows.Forms.Button();
			this.label_estadoConexion = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox_port = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.panel_estado = new System.Windows.Forms.Panel();
			this.BDstatus_label = new System.Windows.Forms.Label();
			this.SMSReady_label = new System.Windows.Forms.Label();
			this.CSQ_label = new System.Windows.Forms.Label();
			this.AT_label = new System.Windows.Forms.Label();
			this.panel_verSMS = new System.Windows.Forms.Panel();
			this.label_estadoEnvio = new System.Windows.Forms.Label();
			this.button_pararProcesarSMS = new System.Windows.Forms.Button();
			this.button_procesarSMS = new System.Windows.Forms.Button();
			this.button_verSMS = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.panel_conexion.SuspendLayout();
			this.panel_estado.SuspendLayout();
			this.panel_verSMS.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// panel_conexion
			// 
			this.panel_conexion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel_conexion.Controls.Add(this.textBox_IMEI);
			this.panel_conexion.Controls.Add(this.label5);
			this.panel_conexion.Controls.Add(this.button_conectar);
			this.panel_conexion.Controls.Add(this.label_estadoConexion);
			this.panel_conexion.Controls.Add(this.label3);
			this.panel_conexion.Controls.Add(this.textBox_port);
			this.panel_conexion.Controls.Add(this.label2);
			this.panel_conexion.Controls.Add(this.label1);
			this.panel_conexion.Location = new System.Drawing.Point(13, 12);
			this.panel_conexion.Name = "panel_conexion";
			this.panel_conexion.Size = new System.Drawing.Size(306, 92);
			this.panel_conexion.TabIndex = 1;
			// 
			// textBox_IMEI
			// 
			this.textBox_IMEI.Location = new System.Drawing.Point(57, 50);
			this.textBox_IMEI.Name = "textBox_IMEI";
			this.textBox_IMEI.ReadOnly = true;
			this.textBox_IMEI.Size = new System.Drawing.Size(122, 20);
			this.textBox_IMEI.TabIndex = 7;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(12, 50);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(57, 23);
			this.label5.TabIndex = 6;
			this.label5.Text = "IMEI:";
			// 
			// button_conectar
			// 
			this.button_conectar.Location = new System.Drawing.Point(101, 24);
			this.button_conectar.Name = "button_conectar";
			this.button_conectar.Size = new System.Drawing.Size(75, 23);
			this.button_conectar.TabIndex = 5;
			this.button_conectar.Text = "Conectar";
			this.button_conectar.UseVisualStyleBackColor = true;
			this.button_conectar.Click += new System.EventHandler(this.Button_conectarClick);
			// 
			// label_estadoConexion
			// 
			this.label_estadoConexion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label_estadoConexion.Location = new System.Drawing.Point(185, 38);
			this.label_estadoConexion.Name = "label_estadoConexion";
			this.label_estadoConexion.Size = new System.Drawing.Size(110, 23);
			this.label_estadoConexion.TabIndex = 4;
			this.label_estadoConexion.Text = "DESCONECTADO";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(185, 21);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(43, 23);
			this.label3.TabIndex = 3;
			this.label3.Text = "Estado:";
			// 
			// textBox_port
			// 
			this.textBox_port.Location = new System.Drawing.Point(54, 24);
			this.textBox_port.Name = "textBox_port";
			this.textBox_port.Size = new System.Drawing.Size(41, 20);
			this.textBox_port.TabIndex = 2;
			this.textBox_port.Text = "COM5";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 27);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(57, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "Puerto:";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(4, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(94, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Conexion SIM:";
			this.label1.Click += new System.EventHandler(this.Label1Click);
			// 
			// panel_estado
			// 
			this.panel_estado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel_estado.Controls.Add(this.BDstatus_label);
			this.panel_estado.Controls.Add(this.SMSReady_label);
			this.panel_estado.Controls.Add(this.CSQ_label);
			this.panel_estado.Controls.Add(this.AT_label);
			this.panel_estado.Location = new System.Drawing.Point(13, 182);
			this.panel_estado.Name = "panel_estado";
			this.panel_estado.Size = new System.Drawing.Size(306, 93);
			this.panel_estado.TabIndex = 2;
			this.panel_estado.Visible = false;
			// 
			// BDstatus_label
			// 
			this.BDstatus_label.Location = new System.Drawing.Point(161, 19);
			this.BDstatus_label.Name = "BDstatus_label";
			this.BDstatus_label.Size = new System.Drawing.Size(165, 32);
			this.BDstatus_label.TabIndex = 4;
			// 
			// SMSReady_label
			// 
			this.SMSReady_label.Location = new System.Drawing.Point(0, 65);
			this.SMSReady_label.Name = "SMSReady_label";
			this.SMSReady_label.Size = new System.Drawing.Size(179, 23);
			this.SMSReady_label.TabIndex = 3;
			// 
			// CSQ_label
			// 
			this.CSQ_label.Location = new System.Drawing.Point(0, 42);
			this.CSQ_label.Name = "CSQ_label";
			this.CSQ_label.Size = new System.Drawing.Size(135, 23);
			this.CSQ_label.TabIndex = 2;
			// 
			// AT_label
			// 
			this.AT_label.Location = new System.Drawing.Point(0, 19);
			this.AT_label.Name = "AT_label";
			this.AT_label.Size = new System.Drawing.Size(135, 23);
			this.AT_label.TabIndex = 0;
			// 
			// panel_verSMS
			// 
			this.panel_verSMS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel_verSMS.Controls.Add(this.label_estadoEnvio);
			this.panel_verSMS.Controls.Add(this.button_pararProcesarSMS);
			this.panel_verSMS.Controls.Add(this.button_procesarSMS);
			this.panel_verSMS.Controls.Add(this.button_verSMS);
			this.panel_verSMS.Controls.Add(this.label8);
			this.panel_verSMS.Controls.Add(this.label4);
			this.panel_verSMS.Controls.Add(this.label6);
			this.panel_verSMS.Controls.Add(this.label7);
			this.panel_verSMS.Location = new System.Drawing.Point(13, 110);
			this.panel_verSMS.Name = "panel_verSMS";
			this.panel_verSMS.Size = new System.Drawing.Size(306, 92);
			this.panel_verSMS.TabIndex = 5;
			// 
			// label_estadoEnvio
			// 
			this.label_estadoEnvio.Location = new System.Drawing.Point(100, 68);
			this.label_estadoEnvio.Name = "label_estadoEnvio";
			this.label_estadoEnvio.Size = new System.Drawing.Size(100, 23);
			this.label_estadoEnvio.TabIndex = 8;
			this.label_estadoEnvio.Click += new System.EventHandler(this.Label_estadoEnvioClick);
			// 
			// button_pararProcesarSMS
			// 
			this.button_pararProcesarSMS.Location = new System.Drawing.Point(161, 28);
			this.button_pararProcesarSMS.Name = "button_pararProcesarSMS";
			this.button_pararProcesarSMS.Size = new System.Drawing.Size(120, 40);
			this.button_pararProcesarSMS.TabIndex = 7;
			this.button_pararProcesarSMS.Text = "Detener Procesamiento";
			this.button_pararProcesarSMS.UseVisualStyleBackColor = true;
			this.button_pararProcesarSMS.Visible = false;
			this.button_pararProcesarSMS.Click += new System.EventHandler(this.Button_pararProcesarSMSClick);
			// 
			// button_procesarSMS
			// 
			this.button_procesarSMS.Location = new System.Drawing.Point(15, 28);
			this.button_procesarSMS.Name = "button_procesarSMS";
			this.button_procesarSMS.Size = new System.Drawing.Size(120, 40);
			this.button_procesarSMS.TabIndex = 6;
			this.button_procesarSMS.Text = "Procesar SMS";
			this.button_procesarSMS.UseVisualStyleBackColor = true;
			this.button_procesarSMS.Visible = false;
			this.button_procesarSMS.Click += new System.EventHandler(this.button_procesarSMSClick);
			// 
			// button_verSMS
			// 
			this.button_verSMS.Location = new System.Drawing.Point(80, 19);
			this.button_verSMS.Name = "button_verSMS";
			this.button_verSMS.Size = new System.Drawing.Size(120, 40);
			this.button_verSMS.TabIndex = 5;
			this.button_verSMS.Text = "Ver SMS";
			this.button_verSMS.UseVisualStyleBackColor = true;
			this.button_verSMS.Click += new System.EventHandler(this.Button_verSMSClick);
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.Location = new System.Drawing.Point(4, 8);
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
			this.label6.Location = new System.Drawing.Point(0, 42);
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
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(325, 12);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.Size = new System.Drawing.Size(369, 338);
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
			this.ClientSize = new System.Drawing.Size(701, 361);
			this.Controls.Add(this.panel_verSMS);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.panel_estado);
			this.Controls.Add(this.panel_conexion);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "Gateway_SMS";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainFormFormClosed);
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.panel_conexion.ResumeLayout(false);
			this.panel_conexion.PerformLayout();
			this.panel_estado.ResumeLayout(false);
			this.panel_verSMS.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);

		}
	}
}
