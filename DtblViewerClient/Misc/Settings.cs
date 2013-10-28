using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace DtblViewerClient.Misc {
    public static class Settings {

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string lpAppName, string lpKeyName,
            string lpDefault, [In, Out] char[] lpReturnedString, uint nSize, string lpFileName);
        
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName,
            string lpString, string lpFileName);

        private static string s_settingsFile;

        private static bool s_useExternalDescriptors;
        private static string s_externalDescriptorsFile;

        /// <summary>
        /// Initializes access to the settings file and reads settings into memory.
        /// </summary>
        public static void Initialize() {
            s_settingsFile = Application.StartupPath + "\\Settings.ini";

            if (File.Exists(s_settingsFile)) {
                s_useExternalDescriptors = Read("UseExternalDescriptors") == "1";
                s_externalDescriptorsFile = Read("ExternalDescriptorsFile");
            }
            else {
                File.Create(s_settingsFile).Dispose();
                UseExternalDescriptors = true;
                ExternalDescriptorsFile = "Resources\\Descriptors.xml";
            }
        }

        /// <summary>
        /// Reads a value from the settings file.
        /// </summary>
        /// <param name="sKey">The variable's name who's value will be obtained.</param>
        public static string Read(string sKey) {
            char[] sResultBuffer = new char[256];
            int nLength = GetPrivateProfileString("Settings", sKey, "", sResultBuffer,
                256, s_settingsFile);

            return new String(sResultBuffer, 0, nLength);
        }

        /// <summary>
        /// Writes a value into the settings file.
        /// </summary>
        /// <param name="sKey">The variable's name who's value will be set.</param>
        /// <param name="sValue">The value of the variable to write.</param>
        public static void Write(string sKey, string sValue) {
            WritePrivateProfileString("Settings", sKey, sValue, s_settingsFile);
        }

        /// <summary>
        /// Gets or sets the usage of external descriptors.
        /// </summary>
        public static bool UseExternalDescriptors {
            get { return s_useExternalDescriptors; }
            set {
                s_useExternalDescriptors = value;
                Write("UseExternalDescriptors", value ? "1" : "0");
            }
        }

        /// <summary>
        /// Gets or sets the path to the external descriptors file.
        /// </summary>
        public static string ExternalDescriptorsFile {
            get { return s_externalDescriptorsFile; }
            set {
                s_externalDescriptorsFile = value;
                Write("ExternalDescriptorsFile", value);
            }
        }

    }
}
