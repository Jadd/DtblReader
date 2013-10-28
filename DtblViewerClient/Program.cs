using System;
using System.Windows.Forms;

using DtblViewerClient.Forms;
using DtblViewerClient.Misc;

namespace DtblViewerClient {
    public static class Program {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Settings.Initialize();
            Descriptors.Initialize();

            if (Settings.UseExternalDescriptors)
                Descriptors.Load(Settings.ExternalDescriptorsFile);

            Application.Run(new MainForm());
        }

    }
}
