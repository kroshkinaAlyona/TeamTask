#include <iostream>

#include "BST.h"
using namespace std;


int main()
{   
	BinarySearchTree tree;
	tree.Add(8);
	tree.Add(3);
	tree.Add(10);
	tree.Add(6);
	tree.InOrder();

	//tree.~BinarySearchTree();
	//tree.InOrder();
}

