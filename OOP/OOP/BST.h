#pragma once
#include <iostream>

using namespace std;

struct node
{
    int key;
    node* left  = NULL;
    node* right = NULL;
};


class BinarySearchTree
{
private:
    node* root = NULL;

    void _PreOrder(node*);
    void _InOrder(node*);
    void _Add(node*&, int);
    void _Clear(node*&);

public:
    BinarySearchTree(node* = NULL);

    void Add(int data) { this->_Add(this->root, data); }

    void InOrder()  { this->_InOrder(this->root); }
    void PreOrder() { this->_PreOrder(this->root); }
    //void PostOrder(node* root);

    
    ~BinarySearchTree() { this->_Clear(this->root); }
};
