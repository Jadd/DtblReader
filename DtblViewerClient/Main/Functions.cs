using System.Windows.Forms;

namespace DtblViewerClient.Main {
    public static class Functions {

        /// <summary>
        /// Gets a path relative to the application's startup path.
        /// If the path is a lower directory than the startup path, it returns the full path.
        /// </summary>
        /// <param name="sPath">The path to find the relative path of.</param>
        public static string GetRelativePath(string sPath) {
            string sCurrentPath = Application.StartupPath + "\\";
            if (!sPath.StartsWith(sCurrentPath))
                return sPath;

            return sPath.Substring(sCurrentPath.Length);
        }

    }
}
