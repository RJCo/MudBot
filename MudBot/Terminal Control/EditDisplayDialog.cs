using System.Windows.Forms;

namespace Poderosa
{
    public partial class EditDisplayDialog : Form
    {
        public EditDisplayDialog()
        {
            InitializeComponent();
            Forms.DisplayOptionPanel panel = new Forms.DisplayOptionPanel
            {
                Dock = DockStyle.Fill
            };

            Controls.Add(panel);
        }
    }
}