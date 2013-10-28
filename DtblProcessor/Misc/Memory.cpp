#include "Misc/stdafx.h"
#include "Misc/Memory.h"

using namespace System;
using namespace msclr::interop;

namespace DtblProcessor {
	namespace Misc {
		namespace Memory {

			String^ GetStringA(const char *bBuffer, DWORD64 dwOffset, size_t dwLength) {
				if (dwLength == 0)
					dwLength = strlen(&bBuffer[dwOffset]);

				char *pszString = new char[dwLength + 1];
				memcpy(pszString, &bBuffer[dwOffset], dwLength);
				pszString[dwLength] = 0;
				String^ sResult = marshal_as<String^>(pszString);

				delete[] pszString;
				return sResult;
			}

			String^ GetStringW(const char *bBuffer, DWORD64 dwOffset, size_t dwLength) {
				if (dwLength == 0)
					dwLength = wcslen(reinterpret_cast<const wchar_t*>(&bBuffer[dwOffset]));

				wchar_t *pwszString = new wchar_t[dwLength + 1];
				memcpy(pwszString, &bBuffer[dwOffset], dwLength * 2);
				pwszString[dwLength] = 0;
				String^ sResult = marshal_as<String^>(pwszString);

				delete[] pwszString;
				return sResult;
			}

		}
	}
}