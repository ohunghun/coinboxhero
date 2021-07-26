using mTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface saveObj
{
    void init();
    void save();
    void load();
    void onload();
}

public class savemanager : Singleton<savemanager>
{
    bool isNotSave = true;
    // Start is called before the first frame update
    void Awake()
    {
        init();        
        isNotSave=ES3.Load<bool>("isNotSave", true);
        load();
    }
    
    void init()
    {
        ScreenManager.Instance.init();
        stickManager.Instance.init();
        box.Instance.init();
        
        HorseManager.Instance.init();
        cargo.Instance.init();
        Shop.Instance.init();
        
       
        cargo.Instance.onload();
    }
    public void save()
    {    
        isNotSave = false;
        ES3.Save<bool>("isNotSave", false);
        box.Instance.save();
        stickManager.Instance.save();
        HorseManager.Instance.save();
        cargo.Instance.save();
        coinGun.Instance.save();

    }
    
    public void load()
    {
        box.Instance.load();
        HorseManager.Instance.load();
        cargo.Instance.load();
       coinGun.Instance.load();
        stickManager.Instance.load();

        HorseManager.Instance.onload();
        cargo.Instance.onload();
        coinGun.Instance.onload();
    }
    
}
