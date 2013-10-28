#pragma once

#include "Misc/stdafx.h"

using namespace std;
using namespace System;
using namespace System::Collections::Generic;
using namespace System::IO;
using namespace msclr::interop;

#define Expected_Magic				0x4454424C // "DTBL"
#define Expected_Version			0x00000000

#pragma managed

namespace DtblProcessor {
	namespace Main {

		ref struct Column;
		ref struct Row;

		public ref class Table {
		public:
			Table();
			Table(String^ sFilePath);
			~Table();

			Void OpenFile(String^ sFilePath);
			Void ReadFile();
			Void CloseFile();

			Void ReadHeader();
			Void ReadColumns();
			Void ReadRows();

			Void SortByColumn(Int32 nColumnIndex, Boolean bReverse);

			List<Column^>^ Columns;
			List<Row^>^ Rows;

			property Boolean IsFileOpen { public: Boolean get() { return m_loadedStream; } }
			property Boolean IsFileRead { public: Boolean get() { return m_loadedValues; } }
			property Boolean IsValid { public: Boolean get() { return m_loadedValues && Magic == Expected_Magic && Version == Expected_Version; } }
			property Int32 FileSize { public: Int32 get() { return m_fileLength; } }

			property UInt32 Magic {	public: UInt32 get() { return m_magic; } }
			property UInt32 Version { public: UInt32 get() { return m_version; } }

			property String^ TableName { public: String^ get() { return m_tableName; } }

			property UInt64 ColumnCount { public: UInt64 get() { return m_columnCount; } }
			property UInt64 RowCount { public: UInt64 get() { return m_rowCount; } }

			property Int32 SortColumnIndex { public: Int32 get() { return m_columnSortIndex; } }
			property Boolean IsSortReversed { public: Boolean get() { return m_columnSortReverse; }	}

		private:
			ifstream *m_fileStream;
			Int32 m_fileLength;
			char *m_fileBuffer;

			Boolean m_loadedStream;
			Boolean m_loadedValues;

			UInt32 m_magic;
			UInt32 m_version;
			String^ m_tableName;
			UInt64 m_rowSize;
			UInt64 m_columnCount;
			UInt64 m_columnStart;
			UInt64 m_columnEnd;
			Int32 m_columnSortIndex;
			Boolean m_columnSortReverse;
			UInt64 m_rowCount;
			UInt64 m_rowStart;
		};

		public ref struct Column {
		public:
			enum class DataType : UInt16
			{
				Unknown		= 0,
				UInt32		= 3,
				Float		= 4,
				Boolean		= 11,
				UInt64		= 20,
				String		= 130
			};

			Column(Table^ tblParent, Int32 nIndex);

			property Table^ Parent { public: Table^ get() { return m_parent; } }
			property Int32 Index { public: Int32 get() { return m_index; } }

			Int32 Offset;
			Int32 Size;
			String^ Name;
			DataType Type;
		
		private:
			Table^ m_parent;
			Int32 m_index;
		};

		public ref struct Row : IComparable<Row^> {
		public:
			Row(Table^ tblParent, Int32 nIndex);

			virtual Int32 CompareTo(Row^ rowComparison);

			property Table^ Parent { public: Table^ get() { return m_parent; } }
			property Int32 Index { public: Int32 get() { return m_index; } }

			Dictionary<Int32, Object^>^ Values;
		
		private:
			Table^ m_parent;
			Int32 m_index;
		};

	}
}

#pragma unmanaged