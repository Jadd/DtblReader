using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

using DtblProcessor.Main;
using DtblViewerClient.Main;
using DtblViewerClient.Misc;

namespace DtblViewerClient.Forms {
    public partial class MainForm : Form {
        
        private SetRowsPerPageForm m_formRowsPerPage;
        private SetPageNumberForm m_formPageNumber;
        private AboutForm m_formAbout;

        private WebClient m_webClient;

        private bool m_pageLabelHovered;
        private int m_sortColumnIndex;
        private bool m_sortReversed;
        
        private Table m_loadedTable;
        private string m_lastLoadedTableFilePath;
        private int m_currentPageNumber;
        private int m_pageCount;
        private int m_rowsPerPage;

        public MainForm() {
            InitializeComponent();
            
            m_formRowsPerPage = new SetRowsPerPageForm();
            m_formRowsPerPage.ParentForm = this;
            
            m_formPageNumber = new SetPageNumberForm();
            m_formPageNumber.ParentForm = this;
            
            m_formAbout = new AboutForm();

            m_webClient = new WebClient();

            m_pageLabelHovered = false;
            m_sortColumnIndex = -1;
            m_sortReversed = false;

            m_loadedTable = null;
            m_lastLoadedTableFilePath = "";
            m_currentPageNumber = 0;
            m_pageCount = 0;
            m_rowsPerPage = 50;

            MenuStrip_Tools_UseExternalDescriptors.Checked = Settings.UseExternalDescriptors;
            MenuStrip_Tools_UseExternalDescriptors.CheckedChanged +=
                new EventHandler(MenuStrip_Tools_UseExternalDescriptors_CheckedChanged);
        }

        #region Form Events
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (m_loadedTable != null)
                UnloadTable();
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e) {
            string[] sFilePaths = (string[]) e.Data.GetData(DataFormats.FileDrop);

            if (sFilePaths.Length > 1) {
                MessageBox.Show("WildStar Data Table Viewer may only open one item at a time.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoadTable(sFilePaths[0]);
        }

        private void MainForm_MouseWheel(object sender, MouseEventArgs e) {
            if (!m_pageLabelHovered)
                return;
            
            int nScrollClicks = e.Delta / SystemInformation.MouseWheelScrollDelta;
            if (nScrollClicks < 0 && m_currentPageNumber == 1)
                return;
            if (nScrollClicks > 0 && m_currentPageNumber == m_pageCount)
                return;
            
            SetPageNumber(m_currentPageNumber + nScrollClicks);
        }

        private void TableGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                int nSelectedColumnIndex = e.ColumnIndex - 1;
                if (m_sortColumnIndex != nSelectedColumnIndex) {
                    m_sortColumnIndex = nSelectedColumnIndex;
                    m_sortReversed = false;
                }
                else
                    m_sortReversed = !m_sortReversed;

                m_loadedTable.SortByColumn(m_sortColumnIndex, m_sortReversed);
                SetPageNumber(m_currentPageNumber);
            }
        }

        #region MenuStrip Events
        private void MenuStrip_File_Open_Click(object sender, EventArgs e) {
            if (OpenFileDialog.ShowDialog(this) != DialogResult.OK)
                return;

            LoadTable(OpenFileDialog.FileName);
        }

        private void MenuStrip_File_Export_Click(object sender, EventArgs e) {
            if (ExportFileDialog.ShowDialog(this) != DialogResult.OK)
                return;

            SetStatusText("Exporting Table...");

            Export.ToFile(ref m_loadedTable, ExportFileDialog.FileName,
                Export.GetFormatFromExtension(ExportFileDialog.FileName));

            SetStatusText("Viewing Table: {0}.tbl", m_loadedTable.TableName);
        }

        private void MenuStrip_File_Close_Click(object sender, EventArgs e) {
            UnloadTable();
        }

        private void MenuStrip_File_Exit_Click(object sender, EventArgs e) {
            Close();
        }

        private void MenuStrip_View_DisplayRowIndex_CheckedChanged(object sender, EventArgs e) {
            if (m_loadedTable != null)
                TableGridView.Columns[0].Visible = MenuStrip_View_DisplayRowIndex.Checked;
        }

        private void MenuStrip_View_NextPage_Click(object sender, EventArgs e) {
            if (m_currentPageNumber < m_pageCount)
                SetPageNumber(m_currentPageNumber + 1);
        }

        private void MenuStrip_View_PreviousPage_Click(object sender, EventArgs e) {
            if (m_currentPageNumber > 1)
                SetPageNumber(m_currentPageNumber - 1);
        }

        private void MenuStrip_View_ViewPage_Click(object sender, EventArgs e) {
            m_formPageNumber.MaxPageNumber = m_pageCount;
            m_formPageNumber.ShowDialog(this);
        }

        private void MenuStrip_View_RowsPerPage_50_Click(object sender, EventArgs e) {
            SetRowsPerPage(50);
        }

        private void MenuStrip_View_RowsPerPage_100_Click(object sender, EventArgs e) {
            SetRowsPerPage(100);
        }

        private void MenuStrip_View_RowsPerPage_250_Click(object sender, EventArgs e) {
            SetRowsPerPage(250);
        }

        private void MenuStrip_View_RowsPerPage_500_Click(object sender, EventArgs e) {
            SetRowsPerPage(500);
        }

        private void MenuStrip_View_RowsPerPage_1000_Click(object sender, EventArgs e) {
            SetRowsPerPage(1000);
        }

        private void MenuStrip_View_RowsPerPage_Custom_Click(object sender, EventArgs e) {
            m_formRowsPerPage.ShowDialog(this);
        }

        private void MenuStrip_Tools_UseExternalDescriptors_CheckedChanged(object sender, EventArgs e) {
            if (MenuStrip_Tools_UseExternalDescriptors.Checked && !File.Exists(Settings.ExternalDescriptorsFile)) {
                MessageBox.Show("Please browse for a valid table definitions file.",
                    "WildStar Data Table Viewer", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (OpenDescriptorsFileDialog.ShowDialog(this) != DialogResult.OK)
                    goto UndoCheckChanged;

                Settings.ExternalDescriptorsFile = Functions.GetRelativePath(OpenDescriptorsFileDialog.FileName);
            }

            if (m_loadedTable != null &&
                MessageBox.Show("Changing this option requires the current table to be reloaded. Would you like to continue?",
                    "WildStar Data Table Viewer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                goto UndoCheckChanged;

            Settings.UseExternalDescriptors = MenuStrip_Tools_UseExternalDescriptors.Checked;

            if (m_loadedTable != null) {
                UnloadTable();
                LoadTable(m_lastLoadedTableFilePath);
            }

            return;

        UndoCheckChanged:
            MenuStrip_Tools_UseExternalDescriptors.CheckedChanged -=
                MenuStrip_Tools_UseExternalDescriptors_CheckedChanged;
                    
            MenuStrip_Tools_UseExternalDescriptors.Checked = Settings.UseExternalDescriptors;
                    
            MenuStrip_Tools_UseExternalDescriptors.CheckedChanged +=
                MenuStrip_Tools_UseExternalDescriptors_CheckedChanged;
        }

        private void MenuStrip_Tools_SelectExternalDescriptorsFile_Click(object sender, EventArgs e) {
            if (OpenDescriptorsFileDialog.ShowDialog(this) != DialogResult.OK)
                return;

            if (Settings.UseExternalDescriptors && m_loadedTable != null &&
                MessageBox.Show("Changing this option requires the current table to be reloaded. Would you like to continue?",
                    "WildStar Data Table Viewer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            Settings.ExternalDescriptorsFile = Functions.GetRelativePath(OpenDescriptorsFileDialog.FileName);
            Descriptors.Unload();
            
            if (Settings.UseExternalDescriptors && m_loadedTable != null) {
                UnloadTable();
                LoadTable(m_lastLoadedTableFilePath);
            }
        }

        private void MenuStrip_Tools_DownloadDescriptors_Click(object sender, EventArgs e) {
            if (SaveDescriptorsFileDialog.ShowDialog(this) != DialogResult.OK)
                return;

            SetStatusText("Downloading Table Definitions...");

            try {
                string sVersion = m_webClient.DownloadString("https://dl.dropboxusercontent.com/u/2650876/DtblViewerClient/Descriptors/version.txt");
                m_webClient.DownloadFile("https://dl.dropboxusercontent.com/u/2650876/DtblViewerClient/Descriptors/" + sVersion + ".xml",
                    SaveDescriptorsFileDialog.FileName);
            }
            catch {
                MessageBox.Show("An error occurred while downloading the latest table definitions. Please check your internet connection and try again later.",
                    "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (m_loadedTable == null)
                SetStatusText("Ready");
            else
                SetStatusText("Viewing Table: {0}.tbl", m_loadedTable.TableName);
        }

        private void MenuStrip_About_Click(object sender, EventArgs e) {
            m_formAbout.ShowDialog(this);
        }
        #endregion

        #region StatusStrip Events
        private void StatusStrip_PageNumberLabel_Click(object sender, EventArgs e) {
            m_formPageNumber.MaxPageNumber = m_pageCount;
            m_formPageNumber.ShowDialog(this);
        }

        private void StatusStrip_PageNumberLabel_MouseEnter(object sender, EventArgs e) {
            m_pageLabelHovered = true;
            TableGridView.IsScrolling = false;
        }

        private void StatusStrip_PageNumberLabel_MouseLeave(object sender, EventArgs e) {
            m_pageLabelHovered = false;
            TableGridView.IsScrolling = true;
        }
        #endregion
        #endregion

        /// <summary>
        /// Sets the status text which appears in the bottom-left of the form.
        /// </summary>
        /// <param name="sFormat">The composite format string.</param>
        /// <param name="sArgs">The array of objects which will be formatted.</param>
        protected void SetStatusText(string sFormat, params string[] sArgs) {
            string sText = String.Format(sFormat, sArgs);
            StatusStrip_StatusLabel.Text = sText;
            Refresh();
        }

        /// <summary>
        /// Loads and displays a data table in the grid.
        /// </summary>
        /// <param name="sFilePath">The path to the data table file.</param>
        public void LoadTable(string sFilePath) {
            if (m_loadedTable != null)
                UnloadTable();

            m_loadedTable = new Table(sFilePath);
            m_lastLoadedTableFilePath = sFilePath;

            if (m_loadedTable.FileSize >= (50 * 1024 * 1024))
                if (MessageBox.Show("The selected file is exceptionally large and could take a long time to load. Would you like to continue?",
                    "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;

            if (Settings.UseExternalDescriptors && !Descriptors.IsFileLoaded) {
                SetStatusText("Loading Definitions...");
                Descriptors.Load(Settings.ExternalDescriptorsFile);
            }

            SetStatusText("Loading Table...");

            m_loadedTable.ReadFile();
            m_loadedTable.ReadHeader();
            m_loadedTable.ReadColumns();

            if (Settings.UseExternalDescriptors)
                Descriptors.Apply(ref m_loadedTable);

            m_loadedTable.ReadRows();
            m_loadedTable.CloseFile();

            if (!m_loadedTable.IsValid) {
                MessageBox.Show("The loaded table did not match the expected file format!",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (m_loadedTable.RowCount == 0) {
                MessageBox.Show("The loaded table did not contain any rows!",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            MenuStrip_File_Export.Enabled = true;
            MenuStrip_File_Close.Enabled = true;
            MenuStrip_View_NextPage.Enabled = true;
            MenuStrip_View_PreviousPage.Enabled = true;
            MenuStrip_View_ViewPage.Enabled = true;
            StatusStrip_PageNumberLabel.Visible = true;

            TableGridView.Columns.Add("RowIndexColumn", "<Index>");
            TableGridView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            TableGridView.Columns[0].Visible = MenuStrip_View_DisplayRowIndex.Checked;

            for (int i = 0; i < (int) m_loadedTable.ColumnCount; i++) {
                string sColumnName = m_loadedTable.Columns[i].Name;
                TableGridView.Columns.Add(sColumnName + "Column", sColumnName);
                TableGridView.Columns[i + 1].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            SetRowsPerPage(m_rowsPerPage);
            SetPageNumber(1);
            
            SetStatusText("Viewing Table: {0}.tbl", m_loadedTable.TableName);
        }

        /// <summary>
        /// Unloads the current data table and clears the grid.
        /// </summary>
        public void UnloadTable() {
            if (m_loadedTable == null)
                return;

            SetStatusText("Unloading Table...");

            MenuStrip_File_Export.Enabled = false;
            MenuStrip_File_Close.Enabled = false;
            MenuStrip_View_NextPage.Enabled = false;
            MenuStrip_View_PreviousPage.Enabled = false;
            MenuStrip_View_ViewPage.Enabled = false;
            StatusStrip_PageNumberLabel.Visible = false;

            TableGridView.Rows.Clear();
            TableGridView.Columns.Clear();

            if (m_loadedTable.IsFileOpen)
                m_loadedTable.CloseFile();

            m_loadedTable.Dispose();
            m_loadedTable = null;

            // Clean up lost variables.
            GC.Collect();

            SetStatusText("Ready");
        }

        /// <summary>
        /// Sets the amount of rows which will be displayed per page in the grid.
        /// </summary>
        /// <param name="nRowsPerPage">The amount of rows which will be displayed per page.</param>
        public void SetRowsPerPage(int nRowsPerPage) {
            m_rowsPerPage = nRowsPerPage;

            if (m_loadedTable != null) {
                m_pageCount = (m_loadedTable.Rows.Count - 1) / m_rowsPerPage + 1;
                SetPageNumber(m_currentPageNumber);
            }
        }

        /// <summary>
        /// Sets the current page number of the grid.
        /// </summary>
        /// <param name="nPageNumber"></param>
        public void SetPageNumber(int nPageNumber) {
            if (m_loadedTable == null)
                return;

            if (nPageNumber > m_pageCount)
                nPageNumber = m_pageCount;
            else if (nPageNumber < 1)
                nPageNumber = 1;

            m_currentPageNumber = nPageNumber;
            
            int nCurrentPageStartIndex = (m_currentPageNumber - 1) * m_rowsPerPage + 1;
            int nCurrentPageEndIndex = m_currentPageNumber * m_rowsPerPage;

            if (nCurrentPageEndIndex > m_loadedTable.Rows.Count)
                nCurrentPageEndIndex = m_loadedTable.Rows.Count;

            StatusStrip_PageNumberLabel.Text = "Page: " +
                m_currentPageNumber + " / " + m_pageCount +
                " (" + nCurrentPageStartIndex + " - " + nCurrentPageEndIndex + ")";

            TableGridView.Rows.Clear();

            // When auto-size is enabled, column width is recalculated every
            // time a row is added. Disable it while we are adding them.
            TableGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            long dwColumnCount = (long) m_loadedTable.ColumnCount;
            for (int i = nCurrentPageStartIndex - 1; i < nCurrentPageEndIndex; i++) {
                object[] objValues = new object[dwColumnCount + 1];

                objValues[0] = m_loadedTable.Rows[i].Index + 1;
                for (int j = 0; j < dwColumnCount; j++)
                    objValues[j + 1] = m_loadedTable.Rows[i].Values[j];

                TableGridView.Rows.Add(objValues);
            }

            // Enable auto-size again.
            TableGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

    }
}
