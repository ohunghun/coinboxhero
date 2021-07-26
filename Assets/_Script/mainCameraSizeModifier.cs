using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainCameraSizeModifier : Singleton<mainCameraSizeModifier>
{ 
    private void Awake()
    {
        HorseManager.Instance.AddOnInit(onOwnerUpdate);
        HorseManager.Instance.AddOnOwnerUpdated(onOwnerUpdate);
    }
    void onOwnerUpdate()
    {
      
        int level = HorseManager.Instance.getOwnStat().level;
        int size = 5;
        if (level >= 10)
            size = 11;
        else if (level >= 8)
            size = 10;
        else if (level >= 5)
            size = 9;
        else if (level == 4)
            size = 8;
        else if (level == 3)
            size = 7;
        else if (level == 2)
            size = 6;
        
      

        ScreenManager.Instance.setCameraSize(size);
    }

    
}
