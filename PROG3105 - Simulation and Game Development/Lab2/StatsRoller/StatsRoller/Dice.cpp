#include "stdafx.h"
#include "Dice.h"


Dice::Dice(int val)
{
	if (val >= 1 && val <= 6)
	{
		this->val = val;
	}
}

void Dice::Draw(Graphics & g, const Rect & bbox)
{
	int stroke = bbox.Height / 10;

	SolidBrush whiteFill(Color(255, 255, 255, 255));
	SolidBrush blackFill(Color(255, 0, 0, 0));
	Pen blackBorder(Color(255, 0, 0, 0), stroke / 2.0f);

	// Draw the block
	g.FillRectangle(&whiteFill, bbox);
	g.DrawRectangle(&blackBorder, bbox);

	switch (val)
	{
	case 1:
		g.FillEllipse(&blackFill, REAL((bbox.Width / 2.0f) - (stroke / 2.0f)), REAL((bbox.Height / 2.0f) - (stroke / 2.0f)), REAL(stroke), REAL(stroke));
		break;
	case 2:
		g.FillEllipse(&blackFill, REAL((bbox.Width / 4.0f) - (stroke / 2.0f)), REAL((bbox.Height / 4.0) - (stroke / 2.0f)), REAL(stroke), REAL(stroke));
		g.FillEllipse(&blackFill, REAL((3 * (bbox.Width / 4.0f)) - (stroke / 2.0f)), REAL((3 * (bbox.Height / 4.0f)) - (stroke / 2.0f)), REAL(stroke), REAL(stroke));
		break;
	case 3:
		g.FillEllipse(&blackFill, REAL((bbox.Width / 4.0f) - (stroke / 2.0f)), REAL((bbox.Height / 4.0) - (stroke / 2.0f)), REAL(stroke), REAL(stroke));
		g.FillEllipse(&blackFill, REAL((2.0f * (bbox.Width / 4.0f)) - (stroke / 2.0f)), REAL((2.0f * (bbox.Height / 4.0f)) - (stroke / 2.0f)), REAL(stroke), REAL(stroke));
		g.FillEllipse(&blackFill, REAL((3.0f * (bbox.Width / 4.0f)) - (stroke / 2.0f)), REAL((3.0f * (bbox.Height / 4.0f)) - (stroke / 2.0f)), REAL(stroke), REAL(stroke));
		break;
	case 4:
		g.FillEllipse(&blackFill, REAL((bbox.Width / 4.0f) - (stroke / 2.0f)), REAL((bbox.Height / 4.0) - (stroke / 2.0f)), REAL(stroke), REAL(stroke));
		g.FillEllipse(&blackFill, REAL((bbox.Width / 4.0f) - (stroke / 2.0f)), REAL((3.0f * (bbox.Height / 4.0f)) - (stroke / 2.0f)), REAL(stroke), REAL(stroke));
		g.FillEllipse(&blackFill, REAL((3.0f * (bbox.Width / 4.0f)) - (stroke / 2.0f)), REAL((bbox.Height / 4.0f) - (stroke / 2.0f)), REAL(stroke), REAL(stroke));
		g.FillEllipse(&blackFill, REAL((3.0f * (bbox.Width / 4.0f)) - (stroke / 2.0f)), REAL((3.0f * (bbox.Height / 4.0f)) - (stroke / 2.0f)), REAL(stroke), REAL(stroke));
		break;
	case 5:
		g.FillEllipse(&blackFill, REAL((bbox.Width / 4.0f) - (stroke / 2.0f)), REAL((bbox.Height / 4.0) - (stroke / 2.0f)), REAL(stroke), REAL(stroke));
		g.FillEllipse(&blackFill, REAL((bbox.Width / 4.0f) - (stroke / 2.0f)), REAL((3.0f * (bbox.Height / 4.0f)) - (stroke / 2.0f)), REAL(stroke), REAL(stroke));
		g.FillEllipse(&blackFill, REAL((3.0f * (bbox.Width / 4.0f)) - (stroke / 2.0f)), REAL((bbox.Height / 4.0f) - (stroke / 2.0f)), REAL(stroke), REAL(stroke));
		g.FillEllipse(&blackFill, REAL((3.0f * (bbox.Width / 4.0f)) - (stroke / 2.0f)), REAL((3.0f * (bbox.Height / 4.0f)) - (stroke / 2.0f)), REAL(stroke), REAL(stroke));
		g.FillEllipse(&blackFill, REAL((2.0f * (bbox.Width / 4.0f)) - (stroke / 2.0f)), REAL((2.0f * (bbox.Height / 4.0f)) - (stroke / 2.0f)), REAL(stroke), REAL(stroke));
		break;
	case 6:
		g.FillEllipse(&blackFill, REAL((bbox.Width / 4.0f) - (stroke / 2.0f)), REAL((bbox.Height / 4.0) - (stroke / 2.0f)), REAL(stroke), REAL(stroke));
		g.FillEllipse(&blackFill, REAL((bbox.Width / 4.0f) - (stroke / 2.0f)), REAL((2.0f * (bbox.Height / 4.0f)) - (stroke / 2.0f)), REAL(stroke), REAL(stroke));
		g.FillEllipse(&blackFill, REAL((bbox.Width / 4.0f) - (stroke / 2.0f)), REAL((3.0f * (bbox.Height / 4.0f)) - (stroke / 2.0f)), REAL(stroke), REAL(stroke));
		g.FillEllipse(&blackFill, REAL((3.0f * (bbox.Width / 4.0f)) - (stroke / 2.0f)), REAL((bbox.Height / 4.0f) - (stroke / 2.0f)), REAL(stroke), REAL(stroke));
		g.FillEllipse(&blackFill, REAL((3.0f * (bbox.Width / 4.0f)) - (stroke / 2.0f)), REAL((2.0f * (bbox.Height / 4.0f)) - (stroke / 2.0f)), REAL(stroke), REAL(stroke));
		g.FillEllipse(&blackFill, REAL((3.0f * (bbox.Width / 4.0f)) - (stroke / 2.0f)), REAL((3.0f * (bbox.Height / 4.0f)) - (stroke / 2.0f)), REAL(stroke), REAL(stroke));
		break;
	}
	
}


Dice::~Dice()
{
}
