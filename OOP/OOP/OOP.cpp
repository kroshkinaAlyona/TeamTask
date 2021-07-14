#include <iostream>
//#include "ArrayTemp.h"
//#include "ArrayTemp.cpp"

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

int main()
{    
    Node<int>* head = NULL;
    Node<int>* tail = NULL;
    for (int i = 0; i < 3; i++)
        AddNodeEnd(head, tail, i);

    ViewForvard(head);
}

