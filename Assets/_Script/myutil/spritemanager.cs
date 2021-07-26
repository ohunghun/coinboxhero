using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spritemanager : Singleton<spritemanager>
{

    ArrayList sprites=new ArrayList();
    Hashtable hash = new Hashtable();
    private void Awake()
    {
        addPath("andsoon");
        addPath("character");
        addPath("coins");
        addPath("weapon");
        /*addPath("Miner");
        addPath("Minerals");
        */

    }
    void addPath(string folder)
    {
        Sprite[] sps = Resources.LoadAll<Sprite>("sprites\\"+folder);
        foreach (Sprite sp in sps)
            sprites.Add(sp);
    }


    public Sprite getSprite(string id)
    {
        if(hash.ContainsKey(id))
            return (Sprite)hash[id];
        else
        {
            foreach(Sprite sp in sprites)
            {
                if(sp.name==id)
                {
                    hash.Add(id, sp);
                    return sp;
                }
            }
        }
        return null;
    }
    
}
