using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;


public class SetspriteOrder : MonoBehaviour
{
    
    private void Start()
    {
      
    }
    public void setOrder()
    {
        Dictionary<string, int> order = new Dictionary<string, int>();
        order.Add("arm1_front", 20);
        order.Add("arm2_front", 21);
        order.Add("arm1_back", 4);
        order.Add("arm2_back", 5);
        order.Add("head", 11);
        order.Add("body", 10);
        order.Add("tail", 9);
        order.Add("leg1_front", 15);
        order.Add("leg2_front", 16);
        order.Add("leg1_back", 2);
        order.Add("leg2_back", 3);
        _setOrder(transform,order);
    }
    void _setOrder(Transform t,Dictionary<string,int> order)
    {
        if (order.ContainsKey(t.gameObject.name))
        {
            if (t.gameObject.GetComponent<SpriteRenderer>() != null)
                t.gameObject.GetComponent<SpriteRenderer>().sortingOrder = order[t.gameObject.name];
        }
        foreach (Transform ch in t)
        {   
            _setOrder(ch,order);
        }
    }


}

