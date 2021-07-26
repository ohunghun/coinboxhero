using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopInvisible : MonoBehaviour
{
    public GameObject child;
    // Start is called before the first frame update
    void Start()
    {
        HorseManager.Instance.AddOnInit(onhorseinit);
        
    }

    void onhorseinit()
    {
        if(HorseManager.Instance.getOwnStat().level>=1)
        {
            child.SetActive(true);
        }else
            child.SetActive(false);
    }
    
}
