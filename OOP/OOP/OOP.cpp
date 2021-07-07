#include <iostream>

using namespace std;

int Div(int a, int b)
{
    try
    {
        if (b == 0)
        {
            throw exception("Divided by zero!");
        }
        return a / b;
    }
    catch (...)
    {
        cout << "Divided by zero!" << endl;
        throw;
    }
}



int main()
{
    
    try
    {
        int c = Div(1, 0);
        cout << c << endl;
    }
    catch (exception exp)
    {
        cout << "Exeption in fun Div :" << exp.what() << endl;
    }
}

