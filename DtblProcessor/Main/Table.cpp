#include "Misc/stdafx.h"
#include "Main/Table.h"

using namespace std;
using namespace System;
using namespace System::IO;
using namespace System::Runtime::InteropServices;
using namespace DtblProcessor::Misc;

#define Table_Header_Length			0x00000060
#define Column_Header_Length		0x00000018
#define Column_Title_Alignment		0x00000010

#define Offset_Magic				0x00000000
#define Offset_Version				0x00000004
#define Offset_TableNameLength		0x00000008
#define Offset_RowSize				0x00000018
#define Offset_ColumnCount			0x00000020
#define Offset_ColumnOffset			0x00000028
#define Offset_RowCount				0x00000030
#define Offset_RowOffset			0x00000040
#define Offset_TableName			Table_Header_Length

#define Offset_Column_NameLength	0x00000000
#define Offset_Column_NameOffset	0x00000008
#define Offset_Column_Type			0x00000010

#pragma managed

namespace DtblProcessor {
	namespace Main {

		Table::Table() :
			Columns(gcnew List<Column^>()),
			Rows(gcnew List<Row^>()),
			m_fileStream(nullptr),
			m_fileLength(0),
			m_fileBuffer(nullptr),
			m_loadedStream(false),
			m_loadedValues(false),
			m_magic(0),
			m_version(0),
			m_tableName(),
			m_rowSize(0),
			m_columnCount(0),
			m_columnStart(0),
			m_columnEnd(0),
			m_columnSortIndex(-1),
			m_columnSortReverse(false),
			m_rowCount(0),
			m_rowStart(0)
		{ }

		Table::Table(String^ sFilePath) :
			Table()
		{
			OpenFile(sFilePath);
		}

		Table::~Table() {
			if (m_loadedStream)
				CloseFile();
		}

		Void Table::OpenFile(String^ sFilePath) {
			if (m_loadedStream)
				CloseFile();

			if (!File::Exists(sFilePath))
				throw gcnew Exception("Table::OpenFile(): File does not exist.");

			IntPtr pFilePathPtr = Marshal::StringToHGlobalUni(sFilePath);
			m_fileStream = new ifstream();
			m_fileStream->open(static_cast<const wchar_t*>(pFilePathPtr.ToPointer()), ios::in | ios::binary);
			Marshal::FreeHGlobal(pFilePathPtr);

			if (m_fileStream->is_open()) {
				m_fileStream->seekg(0, m_fileStream->end);
				m_fileLength = (Int32) m_fileStream->tellg();
				m_loadedStream = true;
			}
			else if (m_fileStream)
				delete m_fileStream;

			if (!m_loadedStream)
				throw gcnew Exception("Table::OpenFile(): Failed to obtain access to the file.");
		}

		Void Table::ReadFile() {
			if (!m_loadedStream)
				return;

			if (m_loadedValues)
				delete[] m_fileBuffer;

			m_fileBuffer = new char[m_fileLength];
			m_fileStream->seekg(0, m_fileStream->beg);
			m_fileStream->read(m_fileBuffer, m_fileLength);

			m_loadedValues = true;
		}

		Void Table::CloseFile() {
			if (!m_loadedStream)
				return;

			m_loadedStream = false;
			m_fileLength = 0;

			m_fileStream->close();
			delete m_fileStream;

			if (!m_loadedValues)
				delete[] m_fileBuffer;
		}

		Void Table::ReadHeader() {
			m_magic						= Memory::GetValue<UInt32>(m_fileBuffer, Offset_Magic);
			m_version					= Memory::GetValue<UInt32>(m_fileBuffer, Offset_Version);
			m_rowSize					= Memory::GetValue<UInt64>(m_fileBuffer, Offset_RowSize);
			m_columnCount				= Memory::GetValue<UInt64>(m_fileBuffer, Offset_ColumnCount);
			m_columnStart				= Memory::GetValue<UInt64>(m_fileBuffer, Offset_ColumnOffset) + Table_Header_Length;
			m_columnEnd					= m_columnStart + (Column_Header_Length * m_columnCount);
			m_columnEnd					+= m_columnEnd % Column_Title_Alignment;
			m_rowCount					= Memory::GetValue<UInt64>(m_fileBuffer, Offset_RowCount);
			m_rowStart					= Memory::GetValue<UInt64>(m_fileBuffer, Offset_RowOffset) + Table_Header_Length;

			size_t dwTableNameLength	= Memory::GetValue<size_t>(m_fileBuffer, Offset_TableNameLength) - 1;
			m_tableName					= Memory::GetStringW(m_fileBuffer, Offset_TableName, dwTableNameLength);
		}

		Void Table::ReadColumns() {
			Rows->Clear();
			Columns->Clear();

			GC::Collect();

			UInt64 dwColumnOffset = 0;
			for (int i = 0; i < m_columnCount; i++) {
				Column^ colTableColumn = gcnew Column(this, i);

				UInt64 dwCurrentOffset = m_columnStart + (Column_Header_Length * i);
				size_t dwColumnNameLength = Memory::GetValue<size_t>(m_fileBuffer, dwCurrentOffset + Offset_Column_NameLength) - 1;
				UInt64 dwColumnNameOffset = Memory::GetValue<UInt64>(m_fileBuffer, dwCurrentOffset + Offset_Column_NameOffset);
				
				colTableColumn->Offset = dwColumnOffset;
				colTableColumn->Name = Memory::GetStringW(m_fileBuffer, m_columnEnd + dwColumnNameOffset, dwColumnNameLength);
				colTableColumn->Type = Memory::GetValue<Column::DataType>(m_fileBuffer, dwCurrentOffset + Offset_Column_Type);

				switch (colTableColumn->Type) {
					case Column::DataType::Boolean:
					case Column::DataType::UInt32:
					case Column::DataType::Float:
					{
						dwColumnOffset += 4;
						colTableColumn->Size = 4;
					}
					break;

					case Column::DataType::UInt64:
					{
						dwColumnOffset += 8;
						colTableColumn->Size = 8;
					}
					break;

					case Column::DataType::String:
					{
						dwColumnOffset += 8;
						colTableColumn->Size = 0;
					}
					break;
				}

				Columns->Add(colTableColumn);
			}
		}

		Void Table::ReadRows() {
			for (int i = 0; i < m_rowCount; i++) {
				Row^ rowTableRow = gcnew Row(this, i);
				
				UInt64 dwCurrentOffset = m_rowStart + (m_rowSize * i);
				for (int j = 0; j < m_columnCount; j++) {
					Object^ oValue;

					switch (Columns[j]->Type) {
						case Column::DataType::Boolean:
						{ oValue = Memory::GetValue<Boolean>(m_fileBuffer, dwCurrentOffset + Columns[j]->Offset); }
						break;

						case Column::DataType::UInt32:
						{ oValue = Memory::GetValue<UInt32>(m_fileBuffer, dwCurrentOffset + Columns[j]->Offset); }
						break;

						case Column::DataType::Float:
						{ oValue = Memory::GetValue<Single>(m_fileBuffer, dwCurrentOffset + Columns[j]->Offset); }
						break;

						case Column::DataType::UInt64:
						{ oValue = Memory::GetValue<UInt64>(m_fileBuffer, dwCurrentOffset + Columns[j]->Offset); }
						break;

						case Column::DataType::String:
						{
							UInt64 dwStringPointer = Memory::GetValue<UInt64>(m_fileBuffer, dwCurrentOffset + Columns[j]->Offset);
							oValue = Memory::GetStringW(m_fileBuffer, m_rowStart + dwStringPointer, Columns[j]->Size);
						}
						break;

						default:
						{ oValue = gcnew String("<UNKNOWN VALUE TYPE>"); }
						break;
					}

					rowTableRow->Values->Add(j, oValue);
				}

				Rows->Add(rowTableRow);
			}
		}

		Void Table::SortByColumn(Int32 nColumnIndex, Boolean bReverse) {
			m_columnSortIndex = nColumnIndex;
			m_columnSortReverse = bReverse;
			Rows->Sort();
		}

		Column::Column(Table^ tblParent, Int32 nIndex) :
			Offset(0),
			Size(0),
			Name(),
			Type(Column::DataType::Unknown)
		{
			m_index = nIndex;
			m_parent = tblParent;
		}

		Row::Row(Table^ tblParent, Int32 nIndex) :
			Values(gcnew Dictionary<Int32, Object^>())
		{
			m_index = nIndex;
			m_parent = tblParent;
		}

		Int32 Row::CompareTo(Row^ rowComparison) {
			Int32 nResult = 0;
			Int32 nSortIndex = Parent->SortColumnIndex;

			if (nSortIndex == -1)
				return Index.CompareTo(rowComparison->Index);

			switch (Parent->Columns[nSortIndex]->Type) {
				case Column::DataType::Boolean:
				{ nResult = ((Boolean) Values[nSortIndex]).CompareTo(rowComparison->Values[nSortIndex]); }
				break;
				
				case Column::DataType::UInt32:
				{ nResult = ((UInt32) Values[nSortIndex]).CompareTo(rowComparison->Values[nSortIndex]); }
				break;
				
				case Column::DataType::Float:
				{ nResult = ((Single) Values[nSortIndex]).CompareTo(rowComparison->Values[nSortIndex]); }
				break;
				
				case Column::DataType::UInt64:
				{ nResult = ((UInt64) Values[nSortIndex]).CompareTo(rowComparison->Values[nSortIndex]); }
				break;
				
				case Column::DataType::String:
				{ nResult = ((String^) Values[nSortIndex])->CompareTo(rowComparison->Values[nSortIndex]); }
				break;
			}
			
			return Parent->IsSortReversed ? -nResult : nResult;
		}

	}
}

#pragma unmanaged