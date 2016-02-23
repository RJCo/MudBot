using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MudBot {
    public partial class DebugForm : Form {
        public DebugForm() {
            InitializeComponent();
        }

        private MudBot _parent = null;
        public DebugForm(Form parent) {
            _parent = parent as MudBot;
            InitializeComponent();
        }

        private delegate void UpdateDebugCallback(string text);
        public void UpdateDebug(string text) {
            if (this.DebugTextBox.InvokeRequired) {
                UpdateDebugCallback debugCallback = new UpdateDebugCallback(UpdateDebug);
                this.Invoke(debugCallback, new object[] { text });
            } 
            else {
                this.DebugTextBox.Text += text;
            }

            this.DebugTextBox.Update();
            this.Update();
        }

    }
}
