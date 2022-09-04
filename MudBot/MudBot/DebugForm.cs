using System.Windows.Forms;

namespace MudBot
{
    public partial class DebugForm : Form
    {
        public DebugForm()
        {
            InitializeComponent();
        }

        private delegate void UpdateDebugCallback(string text);
        public void UpdateDebug(string text)
        {
            if (DebugTextBox.InvokeRequired)
            {
                UpdateDebugCallback debugCallback = UpdateDebug;
                Invoke(debugCallback, text);
            }
            else
            {
                DebugTextBox.Text += text;
            }

            DebugTextBox.Update();
            Update();
        }

    }
}
