#include "BinaryTree.h"

BinaryTree::BinaryTree(node* _root)
{
	this->root = _root;
}

void BinaryTree::AddRight(int value)
{
	node* newNode = new node{ value, NULL, NULL };
	
	if (this->root == NULL)
	{
		this->root = newNode;
		return;
	}

	node* help = this->root;
	while (help->right != NULL)
		help = help->right;

	help->right = newNode;
}

void BinaryTree::AddLeft(int value)
{
}
