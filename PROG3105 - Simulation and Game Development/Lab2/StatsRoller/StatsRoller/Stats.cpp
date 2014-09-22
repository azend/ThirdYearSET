#include "stdafx.h"
#include "Stats.h"


Stats::Stats(CArray<int> & strength, CArray<int> & dexterity, CArray<int> & intelligence)
{
	this->strength = new CArray<int>;
	this->intelligence = new CArray<int>;
	this->dexterity = new CArray<int>;

	this->strength->Copy(strength);
	this->intelligence->Copy(intelligence);
	this->dexterity->Copy(dexterity);
}

const CArray<int> & Stats::Strength()
{
	return *strength;
}

const CArray<int> & Stats::Dexterity()
{
	return *dexterity;
}

const CArray<int> & Stats::Intelligence()
{
	return *intelligence;
}

Stats::~Stats()
{
	delete(strength);
	delete(dexterity);
	delete(intelligence);
}
