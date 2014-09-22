#include "stdafx.h"
#include "StatsFactory.h"


StatsFactory::StatsFactory()
{
}


StatsFactory::~StatsFactory()
{
}

const int StatsFactory::RollDice()
{
	return (rand() % 6) + 1;
}

CArray<int> * StatsFactory::GenerateStat()
{

	CArray<int> * diceRolls = new CArray<int>;

	// Roll dice four times and store it into an array
	for (int i = 0; i < 4; i++)
	{
		diceRolls->Add(RollDice());
	}

	// From those four rolls, find the roll with the lowest value
	int lowestRollIndex = 0;
	for (int i = 1; i < diceRolls->GetCount(); i++)
	{
		if (diceRolls->GetAt(i) < diceRolls->GetAt(lowestRollIndex))
		{
			lowestRollIndex = i;
		}
	}

	// Trim off the lowest roll
	diceRolls->RemoveAt(lowestRollIndex);

	return diceRolls;

}

Stats* StatsFactory::Generate()
{
	CArray<int> * strength = GenerateStat();
	CArray<int> * intelligence = GenerateStat();
	CArray<int> * dexterity = GenerateStat();

	Stats* stats = new Stats(
		*strength,
		*intelligence,
		*dexterity
	);
	
	delete(strength);
	delete(intelligence);
	delete(dexterity);
	
	return stats;
}