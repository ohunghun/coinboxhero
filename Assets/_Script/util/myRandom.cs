using UnityEngine;
using System.Collections;

public class myRandom 
{
    
    int index = 0;
    int count = 2000;
    public myRandom(float min, float max)
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
    public float get()
    {
        float random = current.value;
        current = current.next;
        return random;
    }
    class node
    {
        public float value;
        public node next;
        public node(float value)
        {
            this.value = value;
        }
    }
}
