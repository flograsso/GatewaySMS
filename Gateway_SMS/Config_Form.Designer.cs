/*
 * Created by SharpDevelop.
 * User: SoporteSEM
 * Date: 02/02/2017
 * Time: 16:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Gateway_SMS
{
	partial class Config_Form
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private MetroFramework.Controls.MetroTile button_cancelar;
		private MetroFramework.Controls.MetroTile guardar_button;
		private MetroFramework.Controls.MetroLabel metroLabel1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private MetroFramework.Controls.MetroLabel metroLabel2;
		private MetroFramework.Components.MetroToolTip metroToolTip1;
		private MetroFramework.Components.MetroToolTip metroToolTip2;
		
		
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
		/// </summary>
		private void InitializeComponent()
		{
			this.button_cancelar = new MetroFramework.Controls.MetroTile();
			this.guardar_button = new MetroFramework.Controls.MetroTile();
			this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
			this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
			this.metroToolTip2 = new MetroFramework.Components.MetroToolTip();
			this.SuspendLayout();
			// 
			// button_cancelar
			// 
			this.button_cancelar.ActiveControl = null;
			this.button_cancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button_cancelar.Location = new System.Drawing.Point(23, 157);
			this.button_cancelar.Name = "button_cancelar";
			this.button_cancelar.Size = new System.Drawing.Size(120, 54);
			this.button_cancelar.TabIndex = 12;
			this.button_cancelar.Text = "Cancelar";
			this.button_cancelar.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
			this.button_cancelar.UseSelectable = true;
			this.button_cancelar.Click += new System.EventHandler(this.Button_cancelarClick);
			// 
			// guardar_button
			// 
			this.guardar_button.ActiveControl = null;
			this.guardar_button.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.guardar_button.Location = new System.Drawing.Point(168, 157);
			this.guardar_button.Name = "guardar_button";
			this.guardar_button.Size = new System.Drawing.Size(120, 54);
			this.guardar_button.TabIndex = 13;
			this.guardar_button.Text = "Guardar";
			this.guardar_button.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
			this.guardar_button.UseSelectable = true;
			this.guardar_button.Click += new System.EventHandler(this.Guardar_buttonClick);
			// 
			// metroLabel1
			// 
			this.metroLabel1.Location = new System.Drawing.Point(23, 79);
			this.metroLabel1.Name = "metroLabel1";
			this.metroLabel1.Size = new System.Drawing.Size(100, 23);
			this.metroLabel1.TabIndex = 15;
			this.metroLabel1.Text = "Puerto COM:";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(24, 106);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(100, 20);
			this.textBox1.TabIndex = 16;
			this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(168, 106);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(100, 20);
			this.textBox2.TabIndex = 18;
			this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// metroLabel2
			// 
			this.metroLabel2.Location = new System.Drawing.Point(148, 79);
			this.metroLabel2.Name = "metroLabel2";
			this.metroLabel2.Size = new System.Drawing.Size(156, 23);
			this.metroLabel2.TabIndex = 17;
			this.metroLabel2.Text = "Tiempo de espera (seg):";
			// 
			// metroToolTip1
			// 
			this.metroToolTip1.AutomaticDelay = 200;
			this.metroToolTip1.AutoPopDelay = 8000;
			this.metroToolTip1.InitialDelay = 100;
			this.metroToolTip1.ReshowDelay = 40;
			this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Green;
			this.metroToolTip1.StyleManager = null;
			this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
			// 
			// metroToolTip2
			// 
			this.metroToolTip2.AutomaticDelay = 200;
			this.metroToolTip2.AutoPopDelay = 8000;
			this.metroToolTip2.InitialDelay = 100;
			this.metroToolTip2.ReshowDelay = 40;
			this.metroToolTip2.Style = MetroFramework.MetroColorStyle.Green;
			this.metroToolTip2.StyleManager = null;
			this.metroToolTip2.Theme = MetroFramework.MetroThemeStyle.Light;
			// 
			// Config_Form
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(317, 243);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.metroLabel2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.metroLabel1);
			this.Controls.Add(this.guardar_button);
			this.Controls.Add(this.button_cancelar);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Config_Form";
			this.Text = "Configuración";
			this.Load += new System.EventHandler(this.Config_FormLoad);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
