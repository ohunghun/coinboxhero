using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSprite2Born : MonoBehaviour
{

    public void Match()
    {
        Dictionary<string, int> order = new Dictionary<string, int>();
        order.Add("arm1_front", 15);
        order.Add("arm2_front", 16);
        order.Add("arm1_back", 4);
        order.Add("arm2_back", 5);
        order.Add("head", 11);
        order.Add("body", 10);
        order.Add("tail", 9);
        order.Add("leg1_front", 20);
        order.Add("leg2_front", 21);
        order.Add("leg1_back", 2);
        order.Add("leg2_back", 3);
        Dictionary<string, Transform> listBorns = new Dictionary<string, Transform>();
        Dictionary<string, Transform> listSprite = new Dictionary<string, Transform>();
        MakeList(transform.Find("born"), listBorns);
        MakeList(transform.Find("sp"), listSprite);
        foreach(Transform born in listBorns.Values)
        {
           
            if (listSprite.ContainsKey(born.name))
            {
                listSprite[born.name].parent = born;
            }
            born.gameObject.name = "born_" + born.gameObject.name;
        }


    }
    void MakeList(Transform t, Dictionary<string, Transform> list)
    {
        list.Add(t.gameObject.name,t);
        foreach (Transform ch in t)
        {
            MakeList(ch, list);
        }
    }
    
}
