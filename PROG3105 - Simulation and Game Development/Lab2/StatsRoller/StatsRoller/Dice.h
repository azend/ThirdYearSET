#pragma once
class Dice
{
private:
	int val = 1;
public:
	Dice(int val);
	void Draw(Graphics & g, const Rect & bbox);
	~Dice();
};

