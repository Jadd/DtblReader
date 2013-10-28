namespace DtblViewerClient.Forms {
    partial class MainForm {
        public class DataGridViewAltScroll : System.Windows.Forms.DataGridView {
            public bool IsScrolling;

            public DataGridViewAltScroll()
                : base()
            { 
                IsScrolling = true;
            }
            
            protected override void OnMouseWheel(System.Windows.Forms.MouseEventArgs e) {
                if (IsScrolling)
                    base.OnMouseWheel(e);
            }
        }

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
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.MenuStrip_File = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip_File_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip_File_Export = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip_File_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip_File_Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuStrip_File_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip_View = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip_View_DisplayRowIndex = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip_View_Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuStrip_View_NextPage = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip_View_PreviousPage = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip_View_ViewPage = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip_View_Separator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuStrip_View_RowsPerPage = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip_View_RowsPerPage_50 = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip_View_RowsPerPage_100 = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip_View_RowsPerPage_250 = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip_View_RowsPerPage_500 = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip_View_RowsPerPage_1000 = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip_View_RowsPerPage_Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuStrip_View_RowsPerPage_Custom = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip_About = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip_Tools = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip_Tools_UseExternalDescriptors = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip_Tools_Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuStrip_Tools_SelectExternalDescriptorsFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip_Tools_DownloadDescriptors = new System.Windows.Forms.ToolStripMenuItem();
            this.TableGridView = new DtblViewerClient.Forms.MainForm.DataGridViewAltScroll();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusStrip_StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusStrip_PageNumberLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.ExportFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.OpenDescriptorsFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveDescriptorsFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.MenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TableGridView)).BeginInit();
            this.StatusStrip.SuspendLayout();
            this.BottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            this.MenuStrip.BackColor = System.Drawing.SystemColors.Control;
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStrip_File,
            this.MenuStrip_View,
            this.MenuStrip_About,
            this.MenuStrip_Tools});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Padding = new System.Windows.Forms.Padding(2);
            this.MenuStrip.Size = new System.Drawing.Size(984, 24);
            this.MenuStrip.TabIndex = 3;
            // 
            // MenuStrip_File
            // 
            this.MenuStrip_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStrip_File_Open,
            this.MenuStrip_File_Export,
            this.MenuStrip_File_Close,
            this.MenuStrip_File_Separator1,
            this.MenuStrip_File_Exit});
            this.MenuStrip_File.Name = "MenuStrip_File";
            this.MenuStrip_File.Size = new System.Drawing.Size(37, 20);
            this.MenuStrip_File.Text = "File";
            // 
            // MenuStrip_File_Open
            // 
            this.MenuStrip_File_Open.Name = "MenuStrip_File_Open";
            this.MenuStrip_File_Open.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.MenuStrip_File_Open.Size = new System.Drawing.Size(156, 22);
            this.MenuStrip_File_Open.Text = "Open...";
            this.MenuStrip_File_Open.Click += new System.EventHandler(this.MenuStrip_File_Open_Click);
            // 
            // MenuStrip_File_Export
            // 
            this.MenuStrip_File_Export.Enabled = false;
            this.MenuStrip_File_Export.Name = "MenuStrip_File_Export";
            this.MenuStrip_File_Export.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.MenuStrip_File_Export.Size = new System.Drawing.Size(156, 22);
            this.MenuStrip_File_Export.Text = "Export...";
            this.MenuStrip_File_Export.Click += new System.EventHandler(this.MenuStrip_File_Export_Click);
            // 
            // MenuStrip_File_Close
            // 
            this.MenuStrip_File_Close.Enabled = false;
            this.MenuStrip_File_Close.Name = "MenuStrip_File_Close";
            this.MenuStrip_File_Close.Size = new System.Drawing.Size(156, 22);
            this.MenuStrip_File_Close.Text = "Close";
            this.MenuStrip_File_Close.Click += new System.EventHandler(this.MenuStrip_File_Close_Click);
            // 
            // MenuStrip_File_Separator1
            // 
            this.MenuStrip_File_Separator1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MenuStrip_File_Separator1.Name = "MenuStrip_File_Separator1";
            this.MenuStrip_File_Separator1.Size = new System.Drawing.Size(153, 6);
            // 
            // MenuStrip_File_Exit
            // 
            this.MenuStrip_File_Exit.Name = "MenuStrip_File_Exit";
            this.MenuStrip_File_Exit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.MenuStrip_File_Exit.Size = new System.Drawing.Size(156, 22);
            this.MenuStrip_File_Exit.Text = "Exit";
            this.MenuStrip_File_Exit.Click += new System.EventHandler(this.MenuStrip_File_Exit_Click);
            // 
            // MenuStrip_View
            // 
            this.MenuStrip_View.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStrip_View_DisplayRowIndex,
            this.MenuStrip_View_Separator1,
            this.MenuStrip_View_NextPage,
            this.MenuStrip_View_PreviousPage,
            this.MenuStrip_View_ViewPage,
            this.MenuStrip_View_Separator2,
            this.MenuStrip_View_RowsPerPage});
            this.MenuStrip_View.Name = "MenuStrip_View";
            this.MenuStrip_View.Size = new System.Drawing.Size(44, 20);
            this.MenuStrip_View.Text = "View";
            // 
            // MenuStrip_View_DisplayRowIndex
            // 
            this.MenuStrip_View_DisplayRowIndex.CheckOnClick = true;
            this.MenuStrip_View_DisplayRowIndex.Name = "MenuStrip_View_DisplayRowIndex";
            this.MenuStrip_View_DisplayRowIndex.Size = new System.Drawing.Size(169, 22);
            this.MenuStrip_View_DisplayRowIndex.Text = "Display Row Index";
            this.MenuStrip_View_DisplayRowIndex.CheckedChanged += new System.EventHandler(this.MenuStrip_View_DisplayRowIndex_CheckedChanged);
            // 
            // MenuStrip_View_Separator1
            // 
            this.MenuStrip_View_Separator1.Name = "MenuStrip_View_Separator1";
            this.MenuStrip_View_Separator1.Size = new System.Drawing.Size(166, 6);
            // 
            // MenuStrip_View_NextPage
            // 
            this.MenuStrip_View_NextPage.Enabled = false;
            this.MenuStrip_View_NextPage.Name = "MenuStrip_View_NextPage";
            this.MenuStrip_View_NextPage.Size = new System.Drawing.Size(169, 22);
            this.MenuStrip_View_NextPage.Text = "Next Page";
            this.MenuStrip_View_NextPage.Click += new System.EventHandler(this.MenuStrip_View_NextPage_Click);
            // 
            // MenuStrip_View_PreviousPage
            // 
            this.MenuStrip_View_PreviousPage.Enabled = false;
            this.MenuStrip_View_PreviousPage.Name = "MenuStrip_View_PreviousPage";
            this.MenuStrip_View_PreviousPage.Size = new System.Drawing.Size(169, 22);
            this.MenuStrip_View_PreviousPage.Text = "Previous Page";
            this.MenuStrip_View_PreviousPage.Click += new System.EventHandler(this.MenuStrip_View_PreviousPage_Click);
            // 
            // MenuStrip_View_ViewPage
            // 
            this.MenuStrip_View_ViewPage.Enabled = false;
            this.MenuStrip_View_ViewPage.Name = "MenuStrip_View_ViewPage";
            this.MenuStrip_View_ViewPage.Size = new System.Drawing.Size(169, 22);
            this.MenuStrip_View_ViewPage.Text = "View Page...";
            this.MenuStrip_View_ViewPage.Click += new System.EventHandler(this.MenuStrip_View_ViewPage_Click);
            // 
            // MenuStrip_View_Separator2
            // 
            this.MenuStrip_View_Separator2.Name = "MenuStrip_View_Separator2";
            this.MenuStrip_View_Separator2.Size = new System.Drawing.Size(166, 6);
            // 
            // MenuStrip_View_RowsPerPage
            // 
            this.MenuStrip_View_RowsPerPage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStrip_View_RowsPerPage_50,
            this.MenuStrip_View_RowsPerPage_100,
            this.MenuStrip_View_RowsPerPage_250,
            this.MenuStrip_View_RowsPerPage_500,
            this.MenuStrip_View_RowsPerPage_1000,
            this.MenuStrip_View_RowsPerPage_Separator1,
            this.MenuStrip_View_RowsPerPage_Custom});
            this.MenuStrip_View_RowsPerPage.Name = "MenuStrip_View_RowsPerPage";
            this.MenuStrip_View_RowsPerPage.Size = new System.Drawing.Size(169, 22);
            this.MenuStrip_View_RowsPerPage.Text = "Rows Per Page";
            // 
            // MenuStrip_View_RowsPerPage_50
            // 
            this.MenuStrip_View_RowsPerPage_50.Name = "MenuStrip_View_RowsPerPage_50";
            this.MenuStrip_View_RowsPerPage_50.Size = new System.Drawing.Size(125, 22);
            this.MenuStrip_View_RowsPerPage_50.Text = "50";
            this.MenuStrip_View_RowsPerPage_50.Click += new System.EventHandler(this.MenuStrip_View_RowsPerPage_50_Click);
            // 
            // MenuStrip_View_RowsPerPage_100
            // 
            this.MenuStrip_View_RowsPerPage_100.Name = "MenuStrip_View_RowsPerPage_100";
            this.MenuStrip_View_RowsPerPage_100.Size = new System.Drawing.Size(125, 22);
            this.MenuStrip_View_RowsPerPage_100.Text = "100";
            this.MenuStrip_View_RowsPerPage_100.Click += new System.EventHandler(this.MenuStrip_View_RowsPerPage_100_Click);
            // 
            // MenuStrip_View_RowsPerPage_250
            // 
            this.MenuStrip_View_RowsPerPage_250.Name = "MenuStrip_View_RowsPerPage_250";
            this.MenuStrip_View_RowsPerPage_250.Size = new System.Drawing.Size(125, 22);
            this.MenuStrip_View_RowsPerPage_250.Text = "250";
            this.MenuStrip_View_RowsPerPage_250.Click += new System.EventHandler(this.MenuStrip_View_RowsPerPage_250_Click);
            // 
            // MenuStrip_View_RowsPerPage_500
            // 
            this.MenuStrip_View_RowsPerPage_500.Name = "MenuStrip_View_RowsPerPage_500";
            this.MenuStrip_View_RowsPerPage_500.Size = new System.Drawing.Size(125, 22);
            this.MenuStrip_View_RowsPerPage_500.Text = "500";
            this.MenuStrip_View_RowsPerPage_500.Click += new System.EventHandler(this.MenuStrip_View_RowsPerPage_500_Click);
            // 
            // MenuStrip_View_RowsPerPage_1000
            // 
            this.MenuStrip_View_RowsPerPage_1000.Name = "MenuStrip_View_RowsPerPage_1000";
            this.MenuStrip_View_RowsPerPage_1000.Size = new System.Drawing.Size(125, 22);
            this.MenuStrip_View_RowsPerPage_1000.Text = "1,000";
            this.MenuStrip_View_RowsPerPage_1000.Click += new System.EventHandler(this.MenuStrip_View_RowsPerPage_1000_Click);
            // 
            // MenuStrip_View_RowsPerPage_Separator1
            // 
            this.MenuStrip_View_RowsPerPage_Separator1.Name = "MenuStrip_View_RowsPerPage_Separator1";
            this.MenuStrip_View_RowsPerPage_Separator1.Size = new System.Drawing.Size(122, 6);
            // 
            // MenuStrip_View_RowsPerPage_Custom
            // 
            this.MenuStrip_View_RowsPerPage_Custom.Name = "MenuStrip_View_RowsPerPage_Custom";
            this.MenuStrip_View_RowsPerPage_Custom.Size = new System.Drawing.Size(125, 22);
            this.MenuStrip_View_RowsPerPage_Custom.Text = "Custom...";
            this.MenuStrip_View_RowsPerPage_Custom.Click += new System.EventHandler(this.MenuStrip_View_RowsPerPage_Custom_Click);
            // 
            // MenuStrip_About
            // 
            this.MenuStrip_About.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.MenuStrip_About.Name = "MenuStrip_About";
            this.MenuStrip_About.Size = new System.Drawing.Size(52, 20);
            this.MenuStrip_About.Text = "About";
            this.MenuStrip_About.Click += new System.EventHandler(this.MenuStrip_About_Click);
            // 
            // MenuStrip_Tools
            // 
            this.MenuStrip_Tools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStrip_Tools_UseExternalDescriptors,
            this.MenuStrip_Tools_Separator1,
            this.MenuStrip_Tools_SelectExternalDescriptorsFile,
            this.MenuStrip_Tools_DownloadDescriptors});
            this.MenuStrip_Tools.Name = "MenuStrip_Tools";
            this.MenuStrip_Tools.Size = new System.Drawing.Size(48, 20);
            this.MenuStrip_Tools.Text = "Tools";
            // 
            // MenuStrip_Tools_UseExternalDescriptors
            // 
            this.MenuStrip_Tools_UseExternalDescriptors.CheckOnClick = true;
            this.MenuStrip_Tools_UseExternalDescriptors.Name = "MenuStrip_Tools_UseExternalDescriptors";
            this.MenuStrip_Tools_UseExternalDescriptors.Size = new System.Drawing.Size(295, 22);
            this.MenuStrip_Tools_UseExternalDescriptors.Text = "Use External Table Definitions";
            // 
            // MenuStrip_Tools_Separator1
            // 
            this.MenuStrip_Tools_Separator1.Name = "MenuStrip_Tools_Separator1";
            this.MenuStrip_Tools_Separator1.Size = new System.Drawing.Size(292, 6);
            // 
            // MenuStrip_Tools_SelectExternalDescriptorsFile
            // 
            this.MenuStrip_Tools_SelectExternalDescriptorsFile.Name = "MenuStrip_Tools_SelectExternalDescriptorsFile";
            this.MenuStrip_Tools_SelectExternalDescriptorsFile.Size = new System.Drawing.Size(295, 22);
            this.MenuStrip_Tools_SelectExternalDescriptorsFile.Text = "Select Table Definitions File...";
            this.MenuStrip_Tools_SelectExternalDescriptorsFile.Click += new System.EventHandler(this.MenuStrip_Tools_SelectExternalDescriptorsFile_Click);
            // 
            // MenuStrip_Tools_DownloadDescriptors
            // 
            this.MenuStrip_Tools_DownloadDescriptors.Name = "MenuStrip_Tools_DownloadDescriptors";
            this.MenuStrip_Tools_DownloadDescriptors.Size = new System.Drawing.Size(295, 22);
            this.MenuStrip_Tools_DownloadDescriptors.Text = "Download Up-To-Date Table Definitions...";
            this.MenuStrip_Tools_DownloadDescriptors.Click += new System.EventHandler(this.MenuStrip_Tools_DownloadDescriptors_Click);
            // 
            // TableGridView
            // 
            this.TableGridView.AllowUserToAddRows = false;
            this.TableGridView.AllowUserToDeleteRows = false;
            this.TableGridView.AllowUserToResizeRows = false;
            this.TableGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TableGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.TableGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableGridView.Location = new System.Drawing.Point(0, 24);
            this.TableGridView.MultiSelect = false;
            this.TableGridView.Name = "TableGridView";
            this.TableGridView.ReadOnly = true;
            this.TableGridView.RowHeadersVisible = false;
            this.TableGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TableGridView.Size = new System.Drawing.Size(984, 415);
            this.TableGridView.TabIndex = 4;
            this.TableGridView.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.TableGridView_ColumnHeaderMouseClick);
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.Filter = "WildStar Data Table files (*.tbl)|*.tbl|All files (*.*)|*.*";
            this.OpenFileDialog.SupportMultiDottedExtensions = true;
            this.OpenFileDialog.Title = "Open Data Table file...";
            // 
            // StatusStrip
            // 
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusStrip_StatusLabel,
            this.StatusStrip_PageNumberLabel});
            this.StatusStrip.Location = new System.Drawing.Point(0, 0);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Padding = new System.Windows.Forms.Padding(0);
            this.StatusStrip.Size = new System.Drawing.Size(984, 22);
            this.StatusStrip.SizingGrip = false;
            this.StatusStrip.TabIndex = 5;
            // 
            // StatusStrip_StatusLabel
            // 
            this.StatusStrip_StatusLabel.Name = "StatusStrip_StatusLabel";
            this.StatusStrip_StatusLabel.Size = new System.Drawing.Size(857, 17);
            this.StatusStrip_StatusLabel.Spring = true;
            this.StatusStrip_StatusLabel.Text = "Ready";
            this.StatusStrip_StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatusStrip_PageNumberLabel
            // 
            this.StatusStrip_PageNumberLabel.Name = "StatusStrip_PageNumberLabel";
            this.StatusStrip_PageNumberLabel.Size = new System.Drawing.Size(96, 17);
            this.StatusStrip_PageNumberLabel.Text = "Page: 0 / 0 (0 - 0)";
            this.StatusStrip_PageNumberLabel.Visible = false;
            this.StatusStrip_PageNumberLabel.Click += new System.EventHandler(this.StatusStrip_PageNumberLabel_Click);
            this.StatusStrip_PageNumberLabel.MouseEnter += new System.EventHandler(this.StatusStrip_PageNumberLabel_MouseEnter);
            this.StatusStrip_PageNumberLabel.MouseLeave += new System.EventHandler(this.StatusStrip_PageNumberLabel_MouseLeave);
            // 
            // BottomPanel
            // 
            this.BottomPanel.BackColor = System.Drawing.Color.Transparent;
            this.BottomPanel.Controls.Add(this.StatusStrip);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 439);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(984, 22);
            this.BottomPanel.TabIndex = 6;
            // 
            // ExportFileDialog
            // 
            this.ExportFileDialog.Filter = "Comma-separated values file (*.csv)|*.csv|MySQL executable file (*.sql)|*.sql|All" +
    " files (*.*)|*.*";
            this.ExportFileDialog.SupportMultiDottedExtensions = true;
            this.ExportFileDialog.Title = "Export Data Table file...";
            // 
            // OpenDescriptorsFileDialog
            // 
            this.OpenDescriptorsFileDialog.FileName = "Descriptors.xml";
            this.OpenDescriptorsFileDialog.Filter = "WildStar Table Definition files (*.xml)|*.xml|All files (*.*)|*.*";
            this.OpenDescriptorsFileDialog.SupportMultiDottedExtensions = true;
            this.OpenDescriptorsFileDialog.Title = "Open Table Definitions file...";
            // 
            // SaveDescriptorsFileDialog
            // 
            this.SaveDescriptorsFileDialog.FileName = "Descriptors.xml";
            this.SaveDescriptorsFileDialog.Filter = "WildStar Table Definitions file (*.xml)|*.xml";
            this.SaveDescriptorsFileDialog.SupportMultiDottedExtensions = true;
            this.SaveDescriptorsFileDialog.Title = "Save Table Definitions file...";
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 461);
            this.Controls.Add(this.TableGridView);
            this.Controls.Add(this.BottomPanel);
            this.Controls.Add(this.MenuStrip);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = Properties.Resources.Icon;
            this.MainMenuStrip = this.MenuStrip;
            this.MinimumSize = new System.Drawing.Size(1000, 500);
            this.Name = "MainForm";
            this.Text = "WildStar Data Table Viewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseWheel);
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TableGridView)).EndInit();
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.BottomPanel.ResumeLayout(false);
            this.BottomPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem MenuStrip_File;
        private System.Windows.Forms.ToolStripMenuItem MenuStrip_File_Open;
        private System.Windows.Forms.ToolStripMenuItem MenuStrip_File_Export;
        private System.Windows.Forms.ToolStripSeparator MenuStrip_File_Separator1;
        private System.Windows.Forms.ToolStripMenuItem MenuStrip_File_Exit;
        private DataGridViewAltScroll TableGridView;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.ToolStripMenuItem MenuStrip_File_Close;
        private System.Windows.Forms.ToolStripMenuItem MenuStrip_View;
        private System.Windows.Forms.ToolStripMenuItem MenuStrip_View_NextPage;
        private System.Windows.Forms.ToolStripMenuItem MenuStrip_View_PreviousPage;
        private System.Windows.Forms.ToolStripSeparator MenuStrip_View_Separator2;
        private System.Windows.Forms.ToolStripMenuItem MenuStrip_View_RowsPerPage;
        private System.Windows.Forms.ToolStripMenuItem MenuStrip_View_RowsPerPage_50;
        private System.Windows.Forms.ToolStripMenuItem MenuStrip_View_RowsPerPage_100;
        private System.Windows.Forms.ToolStripMenuItem MenuStrip_View_RowsPerPage_250;
        private System.Windows.Forms.ToolStripMenuItem MenuStrip_View_RowsPerPage_500;
        private System.Windows.Forms.ToolStripMenuItem MenuStrip_View_RowsPerPage_1000;
        private System.Windows.Forms.ToolStripSeparator MenuStrip_View_RowsPerPage_Separator1;
        private System.Windows.Forms.ToolStripMenuItem MenuStrip_View_RowsPerPage_Custom;
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel StatusStrip_StatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel StatusStrip_PageNumberLabel;
        private System.Windows.Forms.ToolStripMenuItem MenuStrip_View_ViewPage;
        private System.Windows.Forms.Panel BottomPanel;
        private System.Windows.Forms.ToolStripMenuItem MenuStrip_About;
        private System.Windows.Forms.ToolStripSeparator MenuStrip_View_Separator1;
        private System.Windows.Forms.ToolStripMenuItem MenuStrip_View_DisplayRowIndex;
        private System.Windows.Forms.SaveFileDialog ExportFileDialog;
        private System.Windows.Forms.ToolStripMenuItem MenuStrip_Tools;
        private System.Windows.Forms.ToolStripMenuItem MenuStrip_Tools_UseExternalDescriptors;
        private System.Windows.Forms.ToolStripSeparator MenuStrip_Tools_Separator1;
        private System.Windows.Forms.ToolStripMenuItem MenuStrip_Tools_SelectExternalDescriptorsFile;
        private System.Windows.Forms.ToolStripMenuItem MenuStrip_Tools_DownloadDescriptors;
        private System.Windows.Forms.OpenFileDialog OpenDescriptorsFileDialog;
        private System.Windows.Forms.SaveFileDialog SaveDescriptorsFileDialog;
    }
}