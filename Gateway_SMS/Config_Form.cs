/*
 * Created by SharpDevelop.
 * User: SoporteSEM
 * Date: 02/02/2017
 * Time: 16:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace Gateway_SMS
{
	/// <summary>
	/// Description of Config_Form.
	/// </summary>
	public partial class Config_Form : MetroForm
	{
		public string puertoCOM{set;get;}
		public int delay{get;set;}
		
		public Config_Form()
		{
			
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

		void Config_FormLoad(object sender, EventArgs e)
		{
			metroToolTip1.SetToolTip(this.textBox2, "Tiempo de espera hasta un nuevo\n\rprocesamiento automático de SMS");
			metroToolTip1.SetToolTip(this.metroLabel2, "Tiempo de espera hasta un nuevo\n\rprocesamiento automático de SMS");
			
			metroToolTip1.SetToolTip(this.textBox1, "Puerto COM donde está conectado el GSM.\n\r Se puede ver desde el admin de dispositivos de Windows");
			metroToolTip1.SetToolTip(this.metroLabel1, "Puerto COM donde está conectado el GSM.\n\r Se puede ver desde el admin de dispositivos de Windows");
				
				
			textBox1.Text=Properties.Settings1.Default.puertoCOM;
			textBox2.Text=Properties.Settings1.Default.delay.ToString();
		}
		void Button_cancelarClick(object sender, EventArgs e)
		{
			this.Close();
		}
		void Guardar_buttonClick(object sender, EventArgs e)
		{
			try{
				puertoCOM=textBox1.Text;
				delay=int.Parse(textBox2.Text);
			}
			catch(Exception){
				MessageBox.Show("Corrobore los datos ingresados");
			}
			
			
		}
	}
}
