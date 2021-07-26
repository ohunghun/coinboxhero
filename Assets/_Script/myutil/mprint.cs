using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mprint
{

    public static  void printhash(Hashtable hash)
    {
        foreach(string key in hash.Keys)
        {
            Debug.Log(key + " -> " + hash[key]);
        }
    }
    
}
