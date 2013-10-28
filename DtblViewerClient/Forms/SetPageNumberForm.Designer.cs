namespace DtblViewerClient.Forms {
    partial class SetPageNumberForm {
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
            this.OKButton = new System.Windows.Forms.Button();
            this.PageNumberNumberBox = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.PageNumberNumberBox)).BeginInit();
            this.SuspendLayout();
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(128, 3);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 3;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // PageNumberNumberBox
            // 
            this.PageNumberNumberBox.Location = new System.Drawing.Point(4, 4);
            this.PageNumberNumberBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.PageNumberNumberBox.Name = "PageNumberNumberBox";
            this.PageNumberNumberBox.Size = new System.Drawing.Size(124, 21);
            this.PageNumberNumberBox.TabIndex = 2;
            this.PageNumberNumberBox.ThousandsSeparator = true;
            this.PageNumberNumberBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.PageNumberNumberBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PageNumberNumberBox_KeyPress);
            // 
            // SetPageNumberForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(206, 29);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.PageNumberNumberBox);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = Properties.Resources.Icon;
            this.Name = "SetPageNumberForm";
            this.Text = "Set Page Number...";
            ((System.ComponentModel.ISupportInitialize)(this.PageNumberNumberBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.NumericUpDown PageNumberNumberBox;

    }
}