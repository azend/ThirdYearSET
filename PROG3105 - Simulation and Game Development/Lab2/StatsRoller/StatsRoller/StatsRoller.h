
// StatsRoller.h : main header file for the StatsRoller application
//
#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"       // main symbols


// CStatsRollerApp:
// See StatsRoller.cpp for the implementation of this class
//

class CStatsRollerApp : public CWinApp
{
public:
	GdiplusStartupInput gdiplusStartupInput;
	ULONG_PTR gdiplusToken;

	CStatsRollerApp();


// Overrides
public:
	virtual BOOL InitInstance();
	virtual int ExitInstance();

// Implementation
	afx_msg void OnAppAbout();
	DECLARE_MESSAGE_MAP()
};

extern CStatsRollerApp theApp;
