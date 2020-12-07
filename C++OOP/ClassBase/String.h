#pragma once
#include <iostream>

using namespace std;

class String
{
private:
	char* str;
	int size;
public:
	String(const String& copyString)
	{
		size = copyString.size;
		str = new char[size+1];
		strcpy(str, copyString.str);
	}
};
