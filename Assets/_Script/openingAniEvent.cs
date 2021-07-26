using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openingAniEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        HorseManager.Instance.AddOnOwnerUpdated(onOwnerUpdated);
        beforeLevel = HorseManager.Instance.getOwnStat().level;
    }
    int beforeLevel = 0;
    void onOwnerUpdated()
    {
        int cur = beforeLevel = HorseManager.Instance.getOwnStat().level;
        if (beforeLevel < cur)
        {
            savemanager.Instance.save();
            beforeLevel = cur;
        }
    }
  
    // Update is called once per frame
    void Update()
    {
        
    }
}
