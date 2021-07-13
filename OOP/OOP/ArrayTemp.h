#pragma once
#include <iostream>

using namespace std;

namespace my_namespace
{
	template<typename T>
	class ArrayTemp
	{
	public:
		T* mass = NULL;
		int size = 0;
	public:
		ArrayTemp(int = 0);

		friend ostream& operator<<(ostream& out, ArrayTemp& arr)
		{
			try
			{
				if (arr.mass == NULL)
					throw invalid_argument("null mass argument");

				out << "size = " << arr.size << endl;
				for (int i = 0; i < arr.size; i++)
				{
					out << arr.mass[i] << "\t";
				}
				out << endl;

				return out;
			}
			catch (exception& exp)
			{
				cout << "Input error :" << exp.what() << endl;
			}
		}

		void Insert(T value, int index = 0);
		T Remove(int index);
	};


	void Print();
}

namespace




