#pragma once

#include "Misc/stdafx.h"

using namespace System;

namespace DtblProcessor {
	namespace Misc {
		namespace Memory {
			
			template <typename T>
			inline T GetValue(const char *bBuffer, DWORD64 dwOffset) {
				return *reinterpret_cast<const T*>(&bBuffer[dwOffset]);
			}

			String^ GetStringA(const char *bBuffer, DWORD64 dwOffset, size_t dwLength);
			String^ GetStringW(const char *bBuffer, DWORD64 dwOffset, size_t dwLength);

		}
	}
}