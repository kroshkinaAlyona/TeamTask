#include <iostream>
#include "ArrayTemp.h"
#include "ArrayTemp.cpp"

using namespace my_namespace;
using namespace test;
//using namespace std;

namespace test
{
    void Print() {};
}


int main()
{
    //using my_namespace::ArrayTemp;
    
    try
    {
        test::Print();
        std::cout << 1 << endl;
        //my_namespace::ArrayTemp<int> myMass(1);
        ////cout << myMass;
        //myMass.Insert(1, 0);       
        //myMass.Insert(2, 1);
        //myMass.Insert(3, 2);
        //myMass.Insert(4, 3);
        //cout << myMass;
        //
        //cout << myMass.Remove(2) << endl;
        //cout << myMass;
    }
    catch (exception& exp)
    {
        cout << exp.what() << endl;
    }

}

