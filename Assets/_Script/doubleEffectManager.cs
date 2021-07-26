using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doubleEffectManager : Singleton<doubleEffectManager>
{
    public Transform popups;
    List<popupx2> ps = new List<popupx2>();
    int cur = 0;
    Vector3 boxpos;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform ch in popups)
        {
            ps.Add(ch.GetComponent<popupx2>());
        }
        boxpos = box.Instance.transform.position;
    }
    public void ShootEffect()
    {
        ps[cur].shoot(boxpos + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(0, 1f)));
        cur = (cur + 1) % ps.Count;
    }
    
}
