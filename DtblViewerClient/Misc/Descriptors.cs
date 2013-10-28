using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

using DtblProcessor.Main;

namespace DtblViewerClient.Misc {
    public static class Descriptors {

        private struct ColumnDescriptor {
            public int Index;
            public int Offset;
            public int Size;
        }

        /// <summary>
        /// Determines whether the descriptors are loaded from the file.
        /// </summary>
        public static bool IsFileLoaded { get; private set; }

        private static Dictionary<string, Dictionary<int, ColumnDescriptor>> s_columnDescriptors;
        private static bool s_initialized;

        /// <summary>
        /// Initializes descriptor management and allows descriptors to be loaded into memory.
        /// </summary>
        public static void Initialize() {
            if (s_initialized)
                return;

            s_columnDescriptors = new Dictionary<string, Dictionary<int, ColumnDescriptor>>();
            
            s_initialized = true;
        }

        /// <summary>
        /// Loads descriptors from an XML file into memory.
        /// </summary>
        /// <param name="sFilePath">The path to the XML file.</param>
        public static void Load(string sFilePath) {
            if (!s_initialized)
                return;

            if (!File.Exists(sFilePath)) {
                Settings.UseExternalDescriptors = false;
                return;
            }

            s_columnDescriptors.Clear();
            GC.Collect();

            XmlReader fsDescriptors = XmlReader.Create(sFilePath);
            
            try {
                while (fsDescriptors.Read()) {
                    if(fsDescriptors.NodeType != XmlNodeType.Element || fsDescriptors.Name != "Table")
                        continue;

                    string sTableName = fsDescriptors.GetAttribute("Name");
                    int nColumnCount = Int32.Parse(fsDescriptors.GetAttribute("Columns"));

                    s_columnDescriptors.Add(sTableName, new Dictionary<int, ColumnDescriptor>(nColumnCount));

                    for (int i = 0; i < nColumnCount; i++) {
                        while (fsDescriptors.NodeType != XmlNodeType.Element || fsDescriptors.Name != "Column") {
                            if (!fsDescriptors.Read())
                                throw new Exception("Descriptors::Load(): Element ended unexpectedly.");
                        }

                        int nIndex = Int32.Parse(fsDescriptors.GetAttribute("Index"));
                        int nOffset = Int32.Parse(fsDescriptors.GetAttribute("Offset"));
                        int nSize = Int32.Parse(fsDescriptors.GetAttribute("Size"));

                        if (nIndex < 0 || nIndex >= nColumnCount || s_columnDescriptors[sTableName].ContainsKey(nIndex))
                            throw new Exception("Descriptors::Load(): Encountered unexpected column index.");

                        ColumnDescriptor colColumnDescriptor = new ColumnDescriptor();
                        colColumnDescriptor.Index = nIndex;
                        colColumnDescriptor.Offset = nOffset;
                        colColumnDescriptor.Size = nSize;

                        s_columnDescriptors[sTableName].Add(nIndex, colColumnDescriptor);

                        if (!fsDescriptors.Read())
                            throw new Exception("Descriptors::Load(): Element ended unexpectedly.");
                    }
                }
            } catch { }

            fsDescriptors.Close();
            IsFileLoaded = true;
        }

        /// <summary>
        /// Unloads descriptors from memory.
        /// </summary>
        public static void Unload() {
            s_columnDescriptors.Clear();
            GC.Collect();

            IsFileLoaded = false;
        }

        /// <summary>
        /// Applies descriptors to a table. Rows are required to be re-read after external descriptors are applied.
        /// </summary>
        /// <param name="tblDataTable">The table to apply descriptor information to.</param>
        public static void Apply(ref Table tblDataTable) {
            if (!s_initialized || !IsFileLoaded || !s_columnDescriptors.ContainsKey(tblDataTable.TableName))
                throw new Exception("Descriptors::Apply(): Descriptors do not exist for this table.");

            foreach (ColumnDescriptor colColumnDescriptor in s_columnDescriptors[tblDataTable.TableName].Values) {
                tblDataTable.Columns[colColumnDescriptor.Index].Offset = colColumnDescriptor.Offset;
                tblDataTable.Columns[colColumnDescriptor.Index].Size = colColumnDescriptor.Size;
            }
        }

    }
}
