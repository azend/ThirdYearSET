
#include "Dice.h"

// StatsRollerView.h : interface of the CStatsRollerView class
//

#pragma once


class CStatsRollerView : public CView
{
protected: // create from serialization only
	CStatsRollerView();
	DECLARE_DYNCREATE(CStatsRollerView)

// Attributes
public:
	CStatsRollerDoc* GetDocument() const;
	Image* background = NULL;

// Operations
public:

// Overrides
public:
	virtual void OnDraw(CDC* pDC);  // overridden to draw this view
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
protected:
	virtual BOOL OnPreparePrinting(CPrintInfo* pInfo);
	virtual void OnBeginPrinting(CDC* pDC, CPrintInfo* pInfo);
	virtual void OnEndPrinting(CDC* pDC, CPrintInfo* pInfo);

// Implementation
public:
	virtual ~CStatsRollerView();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// Generated message map functions
protected:
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnTimer(UINT_PTR nIDEvent);
	afx_msg BOOL OnEraseBkgnd(CDC* pDC);
	afx_msg void OnLButtonUp(UINT nFlags, CPoint point);
	afx_msg int OnCreate(LPCREATESTRUCT lpCreateStruct);
};

#ifndef _DEBUG  // debug version in StatsRollerView.cpp
inline CStatsRollerDoc* CStatsRollerView::GetDocument() const
   { return reinterpret_cast<CStatsRollerDoc*>(m_pDocument); }
#endif

