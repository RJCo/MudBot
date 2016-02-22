using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Poderosa.Terminal;

namespace C_Sharp_Usage_Example
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.terminalControl.Host = this.servertextBox1.Text;
            this.terminalControl.Port = int.Parse(this.PorttextBox.Text);
            //this.terminalControl1.Method = WalburySoftware.ConnectionMethod.SSH2;
            this.terminalControl.Method = WalburySoftware.ConnectionMethod.Telnet;

            this.terminalControl.Connect();

            this.terminalControl.SetPaneColors(Color.Blue, Color.Black);
            this.terminalControl.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.terminalControl.TerminalPane.ConnectionTag == null) // it will be null if you're not connected to anything
                return;


            Poderosa.Forms.EditRenderProfile dlg = new Poderosa.Forms.EditRenderProfile(this.terminalControl.TerminalPane.ConnectionTag.RenderProfile);
            
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            this.terminalControl.TerminalPane.ConnectionTag.RenderProfile = dlg.Result;
            this.terminalControl.TerminalPane.ApplyRenderProfile(dlg.Result);
            
        }
        
    }

}