#include "Stats.h"

#pragma once
class StatsFactory
{
private:
	const int RollDice();
	CArray<int> * GenerateStat();
public:
	StatsFactory();
	~StatsFactory();
	Stats* Generate();
};

