#include "BST.h"

void BinarySearchTree::_PreOrder(node* current)
{
	if (current == NULL)
		return;
	cout << current->key << "\t";
	this->_PreOrder(current->left);
	this->_PreOrder(current->right);
}

void BinarySearchTree::_InOrder(node* current)
{
	if (current == NULL)
		return;	
	this->_InOrder(current->left);
	cout << current->key << "\t";
	this->_InOrder(current->right);
}

void BinarySearchTree::_Add(node*& current, int data)
{
	node* newNode = new node{ data, NULL, NULL };
	
	if (current == NULL)
	{
		current = newNode;
		return;
	}
	if (data < current->key)
		_Add(current->left, data);
	else
		_Add(current->right, data);
}

BinarySearchTree::BinarySearchTree(node* root)
{
	this->root = root;
}

void BinarySearchTree::_Clear(node*& current)
{	
	if (current != NULL)
	{
		_Clear(current->left);
		_Clear(current->right);
		delete current;
	}				
}
