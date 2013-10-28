namespace DtblViewerClient.Forms {
    partial class SetRowsPerPageForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.RowsPerPageNumberBox = new System.Windows.Forms.NumericUpDown();
            this.OKButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.RowsPerPageNumberBox)).BeginInit();
            this.SuspendLayout();
            // 
            // RowsPerPageNumberBox
            // 
            this.RowsPerPageNumberBox.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.RowsPerPageNumberBox.Location = new System.Drawing.Point(4, 4);
            this.RowsPerPageNumberBox.Name = "RowsPerPageNumberBox";
            this.RowsPerPageNumberBox.Size = new System.Drawing.Size(124, 21);
            this.RowsPerPageNumberBox.TabIndex = 0;
            this.RowsPerPageNumberBox.ThousandsSeparator = true;
            this.RowsPerPageNumberBox.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.RowsPerPageNumberBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RowsPerPageNumberBox_KeyPress);
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(128, 3);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 1;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // SetRowsPerPageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(206, 29);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.RowsPerPageNumberBox);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = Properties.Resources.Icon;
            this.Name = "SetRowsPerPageForm";
            this.Text = "Set Rows Per Page...";
            ((System.ComponentModel.ISupportInitialize)(this.RowsPerPageNumberBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown RowsPerPageNumberBox;
        private System.Windows.Forms.Button OKButton;
    }
}