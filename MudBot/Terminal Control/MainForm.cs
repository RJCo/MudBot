using Poderosa.Config;
using Poderosa.Connection;
using Poderosa.ConnectionParam;
using Poderosa.Forms;
using System;
using System.Windows.Forms;

namespace Poderosa
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void connectSSHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Poderosa.Forms.MultiPaneControl mc = new Poderosa.Forms.MultiPaneControl();
            //mc.Dock = DockStyle.Fill;
            //mc.BackColor = System.Drawing.Color.AliceBlue;
            //tabPage1.Controls.Add(mc);

            InitialAction a = new InitialAction();
            //Poderosa.Forms.GFrame frame = new Poderosa.Forms.GFrame(a);
            ConnectionHistory hst = GApp.ConnectionHistory;
            LoginDialog dlg = new LoginDialog();

            TCPTerminalParam param = hst.TopTCPParam;

            dlg.ApplyParam(param);
            dlg.StartPosition = FormStartPosition.CenterParent;

            //if (GCUtil.ShowModalDialog(_frame, dlg) == DialogResult.OK)
            //frame.Show();
            //GCUtil.ShowModalDialog(frame, dlg);
            //dlg.ShowDialog();
            dlg._hostBox.Text = "palm";
            dlg._methodBox.SelectedIndex = 2;
            dlg._portBox.Text = "22";
            dlg._userNameBox.Text = "bwilliam";
            dlg.OnOK(null, null);

            ConnectionTag ct = dlg.Result;
        }
    }
}