using System;
using System.Windows.Forms;

namespace DtblViewerClient.Forms {
    public partial class SetPageNumberForm : Form {

        public new MainForm ParentForm;

        public SetPageNumberForm() {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the maximum page number which can be viewed.
        /// </summary>
        public int MaxPageNumber {
            get { return (int) PageNumberNumberBox.Maximum; }
            set { PageNumberNumberBox.Maximum = value; }
        }

        private void PageNumberNumberBox_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar != (char) Keys.Enter)
                return;
            
            ParentForm.SetPageNumber((int) PageNumberNumberBox.Value);
            Hide();
        }

        private void OKButton_Click(object sender, EventArgs e) {
            ParentForm.SetPageNumber((int) PageNumberNumberBox.Value);
            Hide();
        }

    }
}
