using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Poderosa.Terminal;

namespace MudBot
{
    public partial class MudBot : Form
    {
        DebugForm dform = null;
        

        public MudBot()
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

            //terminalControl.SetLog(WalburySoftware.LogType.Default, @"C:\logfile.txt", true);

            this.terminalControl.TerminalPane.TextChanged += terminalControl_TextChanged;
            //this.terminalControl.TerminalPane.
        }

        private void terminalControl_TextChanged(object sender, EventArgs e) {
            var tb = sender as WalburySoftware.TerminalControl;

            if (dform != null && tb != null) {
                dform.UpdateDebug(tb.GetLastLine());
            }
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

        private void DebugButton_Click(object sender, EventArgs e) {
            if (dform == null) {
                dform = new DebugForm();
                dform.Closed += (sendr, args) => dform = null;
                dform.Show();
            }
        }
        
    }

}