using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : Singleton<PrefabManager>
{
    ArrayList Prefabs = new ArrayList();
    Hashtable hash = new Hashtable();
    private void Awake()
    {
        addPath("weapon");
    }
    void addPath(string folder)
    {
        GameObject[] sps = Resources.LoadAll<GameObject>("prefabs\\" + folder);
        foreach (GameObject sp in sps)
            Prefabs.Add(sp);
    }
    public GameObject getPrefab(string id)
    {
        if (hash.ContainsKey(id))
            return (GameObject)hash[id];
        else
        {
            foreach (GameObject sp in Prefabs)
            {
                if (sp.name == id)
                {
                    hash.Add(id, sp);
                    return sp;
                }
            }
        }
        return null;
    }
}
