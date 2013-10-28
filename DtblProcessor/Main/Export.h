#pragma once

#include "Misc/stdafx.h"

using namespace std;
using namespace System;

#pragma managed

namespace DtblProcessor {
	namespace Main {

		public ref class Export abstract sealed {
		public:
			enum class FormatType : Byte
			{
				None,
				CSV,
				SQL
			};

			static Void ToFile(Table^% tblDataTable, String^ sFilePath, FormatType eFormat);
			static FormatType GetFormatFromExtension(String^ sFilePath);
		
		private:
			static Void ToCSV(wofstream *pfsOutputFile, Table^% tblDataTable);
			static Void ToSQL(wofstream *pfsOutputFile, Table^% tblDataTable);
			static Void ReplaceLiterals(String^% sText);
		};

	}
}

#pragma unmanaged