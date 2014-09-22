#pragma once
class Stats
{
private:
	CArray<int>* strength;
	CArray<int>* dexterity;
	CArray<int>* intelligence;
public:
	Stats(CArray<int> & strength, CArray<int> & dexterity, CArray<int> & intelligence);
	const CArray<int> & Strength();
	const CArray<int> & Dexterity();
	const CArray<int> & Intelligence();
	virtual ~Stats();
};

