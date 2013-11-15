using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DtblViewerClient.Main {
    public static class Log {
        
        private static FileStream s_logStream;

        /// <summary>
        /// Initializes the log file for writing. If no file name is specified,
        /// the file will be placed in the startup directory with a datetime file name.
        /// </summary>
        /// <param name="sFileName"></param>
        public static void Initialize(string sFileName = "") {
            if (s_logStream != null)
                Close();

            if (sFileName == "") {
                DateTime dtNow = DateTime.Now;
                sFileName = String.Format("{0}\\{1:D2}-{2:D2}-{3:D4} {4:D2}.{5:D2}.{6:D2}",
                    Application.StartupPath, dtNow.Day, dtNow.Month, dtNow.Year, dtNow.Hour, dtNow.Minute, dtNow.Second);
            }

            if (!sFileName.EndsWith(".log", StringComparison.CurrentCultureIgnoreCase))
                sFileName += ".log";

            s_logStream = new FileStream(sFileName, FileMode.OpenOrCreate, FileAccess.Write);
        }

        /// <summary>
        /// Flushes and closes the log file.
        /// </summary>
        public static void Close() {
            if (s_logStream == null)
                return;

            s_logStream.Flush();
            s_logStream.Close();
            s_logStream = null;
        }

        /// <summary>
        /// Adds an empty line to the log file.
        /// </summary>
        public static void AddLine() {
            s_logStream.WriteByte((byte) '\n');
            s_logStream.Flush();
        }

        /// <summary>
        /// Adds a line of text to the log file.
        /// </summary>
        /// <param name="sFormat">The composite format string.</param>
        /// <param name="oArgs">The array of arguments to format.</param>
        public static void AddLine(string sFormat, params object[] oArgs) {
            if (s_logStream == null)
                Initialize();

            string sText;
            if (oArgs.Length == 0)
                sText = sFormat;
            else
                sText = String.Format(sFormat, oArgs);
            sText += '\n';

            s_logStream.Write(Encoding.UTF8.GetBytes(sText), 0, sText.Length);
            s_logStream.Flush();
        }

    }
}
