#pragma once
#include <iostream>
using namespace std;

template<typename T>
struct Node
{
    T data;
    Node<T>* next = NULL;
    Node<T>* prev = NULL;
};

template<typename T>
void AddNodeEnd(Node<T>*& head, Node<T>*& tail, T data)
{
    Node<T>* newNode = new Node<T>;
    newNode->data = data;

    if (head == NULL)
    {
        head = newNode;
        tail = newNode;
        return;
    }

    newNode->prev = tail;
    tail->next = newNode;
    tail = newNode;
}

template<typename T>
void ViewForvard(Node<T>* head)
{
    Node<T>* help = head;
    while (help != NULL)
    {
        cout << help->data << "\t";
        help = help->next;
    }
}

struct node
{
    int data;
    node* left = NULL;
    node* right = NULL;
};

class BinaryTree
{
private:
	node* root;

public:
	BinaryTree(node* = NULL);

	void AddRight(int);
	void AddLeft(int);
};
