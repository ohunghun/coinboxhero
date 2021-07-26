using UnityEngine;
using UnityEditor;

public class myRandomInt 
{
    int index = 0;
    int count = 2000;
    public myRandomInt(int min, int max)
    {
        node firstnode = new node(Random.Range(min, max));
        node before = firstnode;
        for (int i = 0; i < count; i++)
        {
            node t = new node(Random.Range(min, max));
            before.next = t;
            before = t;
        }
        before.next = firstnode;
        current = firstnode;
    }
    node current;
    public int get()
    {
        int random = current.value;
        current = current.next;
        return random;
    }
    class node
    {
        public int value;
        public node next;
        public node(int value)
        {
            this.value = value;
        }
    }
}