#include "Misc/stdafx.h"
#include "Main/Table.h"
#include "Main/Export.h"

using namespace std;
using namespace System;
using namespace System::Runtime::InteropServices;

#pragma managed

namespace DtblProcessor {
	namespace Main {

		Void Export::ToFile(Table^% tblDataTable, String^ sFilePath, FormatType eFormat) {
			if (eFormat == FormatType::None)
				return;

			IntPtr pFilePathPtr = Marshal::StringToHGlobalUni(sFilePath);
			wofstream fsOutput;
			fsOutput.open(static_cast<const wchar_t*>(pFilePathPtr.ToPointer()), ios::out);
			Marshal::FreeHGlobal(pFilePathPtr);
			fsOutput.imbue(locale(locale::classic(), new codecvt_utf8<wchar_t>));

			if (!fsOutput.is_open())
				throw gcnew Exception("Export::ToFile(): Could not write to output file.");

			if (eFormat == FormatType::CSV)
				ToCSV(&fsOutput, tblDataTable);
			else if (eFormat == FormatType::SQL)
				ToSQL(&fsOutput, tblDataTable);

			fsOutput.flush();
			fsOutput.close();

			GC::Collect();
		}

		Export::FormatType Export::GetFormatFromExtension(String^ sFilePath) {
			String ^sExtension = sFilePath->Substring(sFilePath->LastIndexOf(L'.'))->ToLower();

			if (sExtension == ".csv")
				return FormatType::CSV;
			
			if (sExtension == ".sql")
				return FormatType::SQL;

			return FormatType::None;
		}

		Void Export::ToCSV(wofstream *pfsOutputFile, Table^% tblDataTable) {
			UInt64 dwColumnCount = tblDataTable->ColumnCount;
			UInt64 dwRowCount = tblDataTable->RowCount;
			List<Column^>^ liColumns = tblDataTable->Columns;
			List<Row^>^ liRows = tblDataTable->Rows;

			// Write columns.
			for (int i = 0; i < dwColumnCount; i++) {
				IntPtr pColumnName = Marshal::StringToHGlobalUni(liColumns[i]->Name);
				*pfsOutputFile << static_cast<const wchar_t*>(pColumnName.ToPointer());
				Marshal::FreeHGlobal(pColumnName);
				
				if (i < dwColumnCount - 1)
					*pfsOutputFile << ',';
			}
			*pfsOutputFile << endl;

			// Write rows.
			for (int i = 0; i < dwRowCount; i++) {
				for (int j = 0; j < dwColumnCount; j++) {
					String^ sOutputText;

					if (liColumns[j]->Type == Column::DataType::String) {
						sOutputText = "\"" + ((String^) liRows[i]->Values[j])->Replace("\"", "\"\"") + "\"";
						ReplaceLiterals(sOutputText);
					}
					else
						sOutputText = liRows[i]->Values[j]->ToString();

					IntPtr pRowValue = Marshal::StringToHGlobalUni(sOutputText);
					*pfsOutputFile << static_cast<const wchar_t*>(pRowValue.ToPointer());
					Marshal::FreeHGlobal(pRowValue);
					
					if (j < dwColumnCount - 1)
						*pfsOutputFile << ',';
				}

				if (i < dwRowCount - 1)
					*pfsOutputFile << endl;
			}
		}

		Void Export::ToSQL(wofstream *pfsOutputFile, Table^% tblDataTable) {
			IntPtr pTableName = Marshal::StringToHGlobalUni(tblDataTable->TableName);
			const wchar_t *pwszTableName = static_cast<const wchar_t*>(pTableName.ToPointer());
			
			UInt64 dwColumnCount = tblDataTable->ColumnCount;
			UInt64 dwRowCount = tblDataTable->RowCount;
			List<Column^>^ liColumns = tblDataTable->Columns;
			List<Row^>^ liRows = tblDataTable->Rows;

			// Write comment and table initializer start.
			*pfsOutputFile << L"-- WildStar Table SQL Dump" << endl <<
							  L"-- Extracted using WildStar Data Table Viewer." << endl <<
							  L"-- http://www.ownedcore.com/" << endl <<
							  endl <<
							  L"DROP TABLE IF EXISTS " << pwszTableName << ';' << endl <<
							  L"CREATE TABLE `" << pwszTableName << L"` (" << endl <<
							  L"  `rowId` bigint(20) unsigned NOT NULL," << endl;

			// Write columns.
			for (int i = 0; i < dwColumnCount; i++) {
				IntPtr pColumnName = Marshal::StringToHGlobalUni(liColumns[i]->Name);
				*pfsOutputFile << "  `" << static_cast<const wchar_t*>(pColumnName.ToPointer()) << "` ";
				Marshal::FreeHGlobal(pColumnName);

				switch (liColumns[i]->Type) {
					case Column::DataType::Boolean:
					{ *pfsOutputFile << L"bit(1)"; }
					break;

					case Column::DataType::UInt32:
					{ *pfsOutputFile << L"int(10) unsigned"; }
					break;

					case Column::DataType::Float:
					{ *pfsOutputFile << L"float"; }
					break;

					case Column::DataType::UInt64:
					{ *pfsOutputFile << L"bigint(20) unsigned"; }
					break;

					case Column::DataType::String:
					{ *pfsOutputFile << L"varchar(" << liColumns[i]->Size << ");"; }
					break;
				}
				*pfsOutputFile << " NOT NULL," << endl;
			}

			// Write table initializer end.
			*pfsOutputFile << L"  PRIMARY KEY (`rowId`)" << endl <<
							  L") ENGINE=MyISAM DEFAULT CHARSET=utf8;" << endl <<
							  endl;

			// Write rows.
			for (int i = 0; i < dwRowCount; i++) {
				*pfsOutputFile << L"INSERT INTO " << pwszTableName << L" VALUES('" << liRows[i]->Index << L"', ";
				for (int j = 0; j < dwColumnCount; j++) {
					String^ sOutputText;

					if (liColumns[j]->Type == Column::DataType::String) {
						sOutputText = ((String^) liRows[i]->Values[j])->Replace("\\", "\\\\")->Replace("\'", "\\\'");
						ReplaceLiterals(sOutputText);
					}
					else
						sOutputText = liRows[i]->Values[j]->ToString();

					IntPtr pRowValue = Marshal::StringToHGlobalUni(sOutputText);
					*pfsOutputFile << '\'' << static_cast<const wchar_t*>(pRowValue.ToPointer()) << '\'';
					Marshal::FreeHGlobal(pRowValue);

					if (j < dwColumnCount - 1)
						*pfsOutputFile << L", ";
				}
				
				*pfsOutputFile << ");";
				if (i < dwRowCount - 1)
					*pfsOutputFile << endl;
			}

			Marshal::FreeHGlobal(pTableName);
		}

		Void Export::ReplaceLiterals(String^% sText) {
			sText = sText->Replace("\n", "\\n");
			sText = sText->Replace("\r", "\\r");
			sText = sText->Replace("\t", "\\t");
		}

	}
}

#pragma unmanaged