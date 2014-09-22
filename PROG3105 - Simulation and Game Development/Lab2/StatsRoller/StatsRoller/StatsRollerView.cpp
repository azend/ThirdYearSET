
// StatsRollerView.cpp : implementation of the CStatsRollerView class
//

#include "stdafx.h"
// SHARED_HANDLERS can be defined in an ATL project implementing preview, thumbnail
// and search filter handlers and allows sharing of document code with that project.
#ifndef SHARED_HANDLERS
#include "StatsRoller.h"
#endif

#include "StatsRollerDoc.h"
#include "StatsRollerView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CStatsRollerView

IMPLEMENT_DYNCREATE(CStatsRollerView, CView)

BEGIN_MESSAGE_MAP(CStatsRollerView, CView)
	// Standard printing commands
	ON_COMMAND(ID_FILE_PRINT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_DIRECT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_PREVIEW, &CView::OnFilePrintPreview)
	ON_WM_TIMER()
	ON_WM_ERASEBKGND()
	ON_WM_LBUTTONUP()
	ON_WM_CREATE()
END_MESSAGE_MAP()

// CStatsRollerView construction/destruction

CStatsRollerView::CStatsRollerView()
{
	// TODO: add construction code here
	

}

CStatsRollerView::~CStatsRollerView()
{
	delete background;
}

BOOL CStatsRollerView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	background = Image::FromFile(_T("dungeonBackground.png"));



	return CView::PreCreateWindow(cs);
}

// CStatsRollerView drawing

void CStatsRollerView::OnDraw(CDC* pDC)
{
	CStatsRollerDoc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	if (!pDoc)
		return;

	

	// Get graphics window bounds
	CRect windowBounds;
	GetClientRect(&windowBounds);
	Rect bounds(windowBounds.left, windowBounds.top, windowBounds.Width(), windowBounds.Height());

	// Create background buffer to draw to
	HDC hdcMem = CreateCompatibleDC(pDC->m_hDC); // Create a new device context for drawing
	const int nMemDC = SaveDC(hdcMem); // Create a handle to our new device context

	// Set our device context to be a bitmap
	HBITMAP hBitmap = CreateCompatibleBitmap(pDC->m_hDC, bounds.Width, bounds.Height);
	SelectObject(hdcMem, hBitmap);

	// Create handle to graphics using the device context
	Graphics g(hdcMem);
	//Graphics g(pDC->m_hDC);

	g.DrawImage(background, bounds);
	//g.DrawLine(&Pen(Color::Red, 5.0f), Point(bounds.GetLeft(), bounds.GetTop()), Point(bounds.GetRight(), bounds.GetBottom()));

	int sixth_width = bounds.Width / 6;
	int fifth_height = bounds.Height / 5;

	if (pDoc->stats != NULL)
	{

		// Write labels
		WCHAR statLabel[50] = L"";
		Gdiplus::Font statFont(FontFamily::GenericSansSerif(), 60, FontStyleRegular, UnitPixel);
		SolidBrush blackBrush(Color(0xFF, 0xff, 0x00, 0x00));

		wsprintf(statLabel, L"%s", L"STR");
		g.DrawString(statLabel, -1, &statFont, PointF(sixth_width, fifth_height), &blackBrush);

		wsprintf(statLabel, L"%s", L"INT");
		g.DrawString(statLabel, -1, &statFont, PointF(sixth_width, fifth_height * 2), &blackBrush);

		wsprintf(statLabel, L"%s", L"DEX");
		g.DrawString(statLabel, -1, &statFont, PointF(sixth_width, fifth_height * 3), &blackBrush);

		wsprintf(statLabel, L"%s", L"Click the screen to roll again");
		g.DrawString(statLabel, -1, &statFont, PointF(sixth_width, fifth_height * 4), &blackBrush);


		// Draw stats
		int statSum = 0;

		statSum = 0;
		for (int i = 0; i < 3; i++)
		{
			g.TranslateTransform(sixth_width * (i + 2), fifth_height * 1, MatrixOrderAppend);
			statSum += pDoc->stats->Strength()[i];
			Dice d(pDoc->stats->Strength()[i]);
			d.Draw(g, Rect(0, 0, sixth_width - 10, fifth_height - 10));

			g.ResetTransform();
		}
		wsprintf(statLabel, L"%d", statSum);
		g.DrawString(statLabel, -1, &statFont, PointF(sixth_width * 5, fifth_height), &blackBrush);
		
		statSum = 0;
		for (int i = 0; i < 3; i++)
		{
			g.TranslateTransform(sixth_width * (i + 2), fifth_height * 2, MatrixOrderAppend);
			statSum += pDoc->stats->Intelligence()[i];
			Dice d(pDoc->stats->Intelligence()[i]);
			d.Draw(g, Rect(0, 0, sixth_width - 10, fifth_height - 10));

			g.ResetTransform();
		}
		wsprintf(statLabel, L"%d", statSum);
		g.DrawString(statLabel, -1, &statFont, PointF(sixth_width * 5, fifth_height * 2), &blackBrush);

		statSum = 0;
		for (int i = 0; i < 3; i++)
		{
			g.TranslateTransform(sixth_width * (i + 2), fifth_height * 3, MatrixOrderAppend);
			statSum += pDoc->stats->Dexterity()[i];
			Dice d(pDoc->stats->Dexterity()[i]);
			d.Draw(g, Rect(0, 0, sixth_width - 10, fifth_height - 10));

			g.ResetTransform();
		}
		wsprintf(statLabel, L"%d", statSum);
		g.DrawString(statLabel, -1, &statFont, PointF(sixth_width * 5, fifth_height * 3), &blackBrush);

		
	}
	else {
		WCHAR statLabel[50] = L"";
		Gdiplus::Font statFont(FontFamily::GenericSansSerif(), 60, FontStyleRegular, UnitPixel);
		SolidBrush blackBrush(Color(0xFF, 0xff, 0x00, 0x00));

		wsprintf(statLabel, L"%s", L"Click the screen to roll");
		g.DrawString(statLabel, -1, &statFont, PointF(sixth_width, fifth_height), &blackBrush);
	}




	BitBlt(pDC->m_hDC, 0, 0, bounds.Width, bounds.Height, hdcMem, 0, 0, SRCCOPY);


	RestoreDC(hdcMem, nMemDC);
	DeleteObject(hBitmap);
	DeleteDC(hdcMem);
}


// CStatsRollerView printing

BOOL CStatsRollerView::OnPreparePrinting(CPrintInfo* pInfo)
{
	// default preparation
	return DoPreparePrinting(pInfo);
}

void CStatsRollerView::OnBeginPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add extra initialization before printing
}

void CStatsRollerView::OnEndPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add cleanup after printing
}


// CStatsRollerView diagnostics

#ifdef _DEBUG
void CStatsRollerView::AssertValid() const
{
	CView::AssertValid();
}

void CStatsRollerView::Dump(CDumpContext& dc) const
{
	CView::Dump(dc);
}

CStatsRollerDoc* CStatsRollerView::GetDocument() const // non-debug version is inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CStatsRollerDoc)));
	return (CStatsRollerDoc*)m_pDocument;
}
#endif //_DEBUG


// CStatsRollerView message handlers


void CStatsRollerView::OnTimer(UINT_PTR nIDEvent)
{
	Invalidate();

	CView::OnTimer(nIDEvent);
}


BOOL CStatsRollerView::OnEraseBkgnd(CDC* pDC)
{
	// TODO: Add your message handler code here and/or call default

	//return CView::OnEraseBkgnd(pDC);

	return false;
}


void CStatsRollerView::OnLButtonUp(UINT nFlags, CPoint point)
{
	CStatsRollerDoc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	if (!pDoc)
		return;

	pDoc->GenerateStats();

	CView::OnLButtonUp(nFlags, point);
}


int CStatsRollerView::OnCreate(LPCREATESTRUCT lpCreateStruct)
{
	if (CView::OnCreate(lpCreateStruct) == -1)
		return -1;

	SetTimer(1, 50, NULL);

	return 0;
}
