using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;

using mTypes;
public class HorseManager : Singleton<HorseManager> ,saveObj
{
    public Transform initPosition;
    ObscuredInt maxClone = 5;
    Stat Data_horseOwn = new Stat(0);
    Queue<Stat> Data_horseClones = new Queue<Stat>();
    [HideInInspector]
    public GameObject Own;
    List<GameObject> Clones = new List<GameObject>();
    ObscuredInt lastCloneId = 0;
    List<callbackVoid> callback_OwnerUpdated = new List<callbackVoid>();
    List<callbackVoid> callback_oninit = new List<callbackVoid>();

    bool isinit = false;
    public void AddOnInit(callbackVoid callback)
    {
        callback_oninit.Add(callback);
        if (isinit)
            callback();
    }
    public void AddOnOwnerUpdated(callbackVoid callback)
    {
        callback_OwnerUpdated.Add(callback);
        
    }

    public void PauseHorse()
    {
        foreach(GameObject clone in Clones)
        {
            Horse h = clone.GetComponent<Horse>();
            h.isAutoStop = true;
        }
    }
    public void ResumeHorse()
    {
        foreach (GameObject clone in Clones)
        {
            Horse h = clone.GetComponent<Horse>();
            h.isAutoStop = false;
        }
    }
    public List<GameObject> getAllHorse()
    {
        List<GameObject> t = new List<GameObject>(Clones);
        t.Add(Own);
        return t;
    }
    public Stat getOwnStat()
    {
        return Data_horseOwn;
    }
    public void increaseMaxClone(int amount)
    {
        maxClone += amount;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            levelUp();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            MakeClone();
        }
       

    }
    public void save()
    {
        ES3.Save<bool>("isFirst", false);
        ES3.Save<Queue<Stat>>("Data_horseClones", Data_horseClones);
        ES3.Save<Stat>("Data_horseOwn", Data_horseOwn);
        ES3.Save<int>("lastCloneId", lastCloneId);
        ES3.Save<int>("maxClone", maxClone);
    }
    public void load()
    {
        Data_horseClones = ES3.Load<Queue<Stat>>("Data_horseClones", Data_horseClones);
        Data_horseOwn = ES3.Load<Stat>("Data_horseOwn", Data_horseOwn);
        lastCloneId = ES3.Load<int>("lastCloneId", lastCloneId);
        maxClone = ES3.Load<int>("maxClone", maxClone);
    }
    public void onload()
    {

        Vector3 v = Camera.main.ScreenToWorldPoint(new Vector3(ScreenManager.Instance.getScreenLeft(), 0, 0));
        initPosition.position = new Vector3(v.x * 0.7f, initPosition.position.y, initPosition.position.z);
        resetOwner();
        resetClones();
        isinit = true;
    }
    public int getCurmax()
    {
        return maxClone;
    }
    public int getCurHorse()
    {
        return Clones.Count;
    }

    void resetClones()
    {
        foreach(GameObject obj in Clones)
        {
            Destroy(obj);
        }
        Clones.Clear();
        foreach (Stat s in Data_horseClones)
        {
            Debug.Log(s.level);
            GameObject obj = Instantiate(getPrefab(s.level), initPosition);
            obj.transform.position = initPosition.position;
            obj.GetComponent<Horse>().init(s,true);
            obj.GetComponent<coinGather>().enabled = false;

            Clones.Add(obj);
        }
    }
    public Stat getStat(int id)
    {
        foreach(Stat s in Data_horseClones)
        {
            if (s.id == id)
                return s;
        }
        return null;
    }
    GameObject getCloneObj(int id)
    {
        foreach(GameObject obj in Clones)
        {
            if (obj.GetComponent<Horse>().stat.id == id)
                return obj;
        }
        return null;
    }
    public void MakeClone()
    {
        if(Clones.Count>=maxClone)
        {
            Stat removeHorse = Data_horseClones.Dequeue();
            GameObject t=getCloneObj(removeHorse.id);
            Clones.Remove(t);
            Destroy(t);

        }
        GameObject obj = Instantiate(getPrefab(Data_horseOwn.level), initPosition);
        obj.transform.position = initPosition.position;
        Stat stat=Data_horseOwn.getCopy();
        stat.id = lastCloneId++;
        Data_horseClones.Enqueue(stat);
        obj.GetComponent<Horse>().init(stat, true);
        obj.GetComponent<coinGather>().enabled = false;
        obj.name = lastCloneId + "_clone";
        Clones.Add(obj);
       
    }
    public void levelUp()
    {
        evolutionEffect.Instance.Effect(0.5f, Own.transform.position);
        Data_horseOwn.level += 1;
        SoundManager.getInstance().play("evolution");
        resetOwner();


    }
    public void bonusPowerUp()
    {
        Data_horseOwn.BonusPower += 1;
        resetOwner();
    }
  
    public void speedUp()
    {
        Data_horseOwn.speed += 1;
        resetOwner();
    }
    
    
    public void WeaponUp()
    {
        SoundManager.getInstance().play("equip");
        Data_horseOwn.weaponLevel += 1;
        resetOwner();
    }
    
    void resetOwner()
    {
        Vector3 position = initPosition.position;
        if (Own != null)
            position = Own.transform.position;
        GameObject obj= Instantiate(getPrefab(Data_horseOwn.level),initPosition);
        obj.GetComponent<Horse>().init(Data_horseOwn, false);
        
        Destroy(Own);
        Own = obj;
        obj.transform.position = new Vector3(position.x, initPosition.position.y, position.z);
        foreach (callbackVoid callback in callback_OwnerUpdated)
        {
            callback();
        }
        
    }
    GameObject getPrefab(int level)
    {
        return Resources.Load<GameObject>("prefab_horse/horse" + level);
    }

    public void init()
    {
    }
}

public class Stat
{
    public int id;
    public int level = 0;
    public ObscuredInt BonusPower =0;
    public ObscuredInt speed =0;
    public int weaponLevel = 0;
    public Stat()
    {
        
    }
    public Stat(int id)
    {
        this.id = id;
    }
    public Stat getCopy()
    {
        Stat t = new Stat(id);
        t.level = this.level;
        t.BonusPower = this.BonusPower;
        t.speed = this.speed;
      
        t.weaponLevel = this.weaponLevel;
        return t;
    }
    public bool isEqual(Stat t)
    {
        if (t.level == level && t.BonusPower == BonusPower && t.speed == speed &&  t.weaponLevel == weaponLevel)
            return true;
        return false;
    }
}