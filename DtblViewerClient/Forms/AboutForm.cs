using System;
using System.Windows.Forms;

namespace DtblViewerClient.Forms {
    public partial class AboutForm : Form {

        public AboutForm() {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e) {
            VersionLabel.Text = "Version " + Application.ProductVersion;
        }

        private void AboutForm_FormClosing(object sender, FormClosingEventArgs e) {
            // Prevent the form from disposing.
            e.Cancel = true;
            Hide();
        }

        private void CloseButton_Click(object sender, EventArgs e) {
            Close();
        }

    }
}
