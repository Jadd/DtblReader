using System;
using System.Windows.Forms;

namespace DtblViewerClient.Forms {
    public partial class SetRowsPerPageForm : Form {

        protected const int MinimumRows = 5;
        protected const int MaximumRows = 1000;

        public new MainForm ParentForm;

        public SetRowsPerPageForm() {
            InitializeComponent();
            
            RowsPerPageNumberBox.Minimum = MinimumRows;
            RowsPerPageNumberBox.Maximum = MaximumRows;
            RowsPerPageNumberBox.Value = MinimumRows;
        }

        private void RowsPerPageNumberBox_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar != (char) Keys.Enter)
                return;
            
            ParentForm.SetRowsPerPage((int) RowsPerPageNumberBox.Value);
            Hide();
        }

        private void OKButton_Click(object sender, EventArgs e) {
            ParentForm.SetRowsPerPage((int) RowsPerPageNumberBox.Value);
            Hide();
        }

    }
}
