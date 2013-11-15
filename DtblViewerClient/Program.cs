using System;
using System.Threading;
using System.Windows.Forms;

using DtblViewerClient.Forms;
using DtblViewerClient.Main;
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
            Application.ThreadException += new ThreadExceptionEventHandler(OnUnhandledException);

            Settings.Initialize();
            Descriptors.Initialize();

            if (Settings.UseExternalDescriptors)
                Descriptors.Load(Settings.ExternalDescriptorsFile);

            Application.Run(new MainForm());
        }

        private static void OnUnhandledException(object sender, ThreadExceptionEventArgs e) {
            Exception ex = e.Exception;

            if (MessageBox.Show("DtblViewerClient encountered an unhandled exception! Would you like to log the output?",
                "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error) != DialogResult.Yes)
                return;

            try {
                Log.Initialize();

                Log.AddLine("An exception occurred!");
                Log.AddLine();
                Log.AddLine("Module:\t\t{0}", ex.TargetSite.Module.Name);
                Log.AddLine("Namespace:\t{0}", ex.TargetSite.DeclaringType.Namespace);
                Log.AddLine("Class:\t\t{0}", ex.TargetSite.DeclaringType.Name);
                Log.AddLine("Function:\t{0}", ex.TargetSite.Name);
                Log.AddLine();
                Log.AddLine("Call Stack:");
                Log.AddLine(ex.StackTrace);
                
                if (Settings.IsInitialized) {
                    Log.AddLine();
                    Log.AddLine("Settings:");
                    Log.AddLine("   UseExternalDescriptors:\t{0}", Settings.UseExternalDescriptors);
                    Log.AddLine("   ExternalDescriptorsFile:\t{0}", Settings.ExternalDescriptorsFile);
                }
                
                Log.Close();
            }
            catch {
                MessageBox.Show("Could not write exception information to the log file!",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Log.Close();
            }

            Application.Exit();
        }

    }
}
