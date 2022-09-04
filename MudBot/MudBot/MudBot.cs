using MajorMud.Database;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MudBot
{
    public partial class MudBot : Form
    {
        private DebugForm dform;
        private static Database _database = new Database();


        public MudBot()
        {
            InitializeComponent();
            _database.Bootstrap();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            terminalControl.Host = servertextBox1.Text;
            terminalControl.Port = int.Parse(PorttextBox.Text);

            terminalControl.Connect();

            terminalControl.SetPaneColors(Color.Blue, Color.Black);
            terminalControl.Focus();

            //terminalControl.SetLog(WalburySoftware.LogType.Default, @"C:\logfile.txt", true);

            terminalControl.TerminalPane.TextChanged += terminalControl_TextChanged;
            //this.terminalControl.TerminalPane.
        }

        private void terminalControl_TextChanged(object sender, EventArgs e)
        {
            var tb = sender as WalburySoftware.TerminalControl;

            if (dform != null && tb != null)
            {
                dform.UpdateDebug(tb.GetLastLine());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (terminalControl.TerminalPane.ConnectionTag == null) // it will be null if you're not connected to anything
            {
                return;
            }

            Poderosa.Forms.EditRenderProfile dlg = new Poderosa.Forms.EditRenderProfile(terminalControl.TerminalPane.ConnectionTag.RenderProfile);

            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            terminalControl.TerminalPane.ConnectionTag.RenderProfile = dlg.Result;
            terminalControl.TerminalPane.ApplyRenderProfile(dlg.Result);
        }

        private void DebugButton_Click(object sender, EventArgs e)
        {
            if (dform == null)
            {
                dform = new DebugForm();
                dform.Closed += (sendr, args) => dform = null;
                dform.Show();
            }
        }

    }

}