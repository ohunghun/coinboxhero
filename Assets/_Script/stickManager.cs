using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mTypes;
public class stickManager : Singleton<stickManager>
{
    List<callbackVoid> list_callbackOnLoaded = new List<callbackVoid>();
    List<callbackVoid> list_callbackOnStickUpdated = new List<callbackVoid>();
    Dictionary<string, stickInfo> dic_stick;
    public GameObject[] stick1s;
    public GameObject[] stick2s;
    public GameObject[] stick3s;
    public GameObject[] stick4s;
    const int max = 8;
    public void init()
    {
        dic_stick = new Dictionary<string, stickInfo>();
        dic_stick.Add("stick1", new stickInfo());
        dic_stick.Add("stick2", new stickInfo());
        dic_stick.Add("stick3", new stickInfo());
        dic_stick.Add("stick4", new stickInfo());
    }
    public float getPower(string id)
    {
        int curlevel = curStickInfo(id).level_power;
        if (id=="stick1")
        {
            return Mathf.Clamp(curlevel, 1, 100);

        }
        else if (id == "stick2")
        {
            return Mathf.Clamp(curlevel*2, 2, 100);
        }
        else if (id == "stick3")
        {
            return Mathf.Clamp(curlevel * 3, 3, 100);
        }
        else if (id == "stick4")
        {
            return Mathf.Clamp(curlevel * 4, 4, 100);
        }
        return 1;

    }
    
    public void addOnLoaded(callbackVoid callback)
    {
        if (isLoaded)
            callback();
        else
            list_callbackOnLoaded.Add(callback);
    }
    public void addOnStickUpdated(callbackVoid callback)
    {
        list_callbackOnStickUpdated.Add(callback);
    }
    public  void save()
    {
        ES3.Save<Dictionary<string, stickInfo>>("stickManagerDic", dic_stick);
    }
    bool isLoaded = false;
    public void load()
    {
        dic_stick=ES3.Load<Dictionary<string, stickInfo>>("stickManagerDic", dic_stick);
        isLoaded = true;
        foreach (callbackVoid callback in list_callbackOnLoaded)
            callback();
        updateStick();
    }
    public void powerUpStick(string id)
    {
        stickInfo info = curStickInfo(id);
        info.level_power++;
        foreach (callbackVoid callback in list_callbackOnStickUpdated)
            callback();
    }
    public bool addStick(string id)
    {
        stickInfo info = curStickInfo(id);
        if (info.count >= max)
            return false;
        else
        {
            info.count++;
            updateStick();
            foreach (callbackVoid callback in list_callbackOnStickUpdated)
                callback();
            return true;
        }
    }
    void updateStick()
    {
        for (int i = 0; i < stick1s.Length; i++)
            stick1s[i].SetActive(false);
        for (int i = 0; i < stick2s.Length; i++)
            stick2s[i].SetActive(false);
        for (int i = 0; i < stick3s.Length; i++)
            stick3s[i].SetActive(false);
        for (int i = 0; i < stick4s.Length; i++)
            stick4s[i].SetActive(false);

        int stick1 = curStickInfo("stick1").count;
        float stick1power=getPower("stick1");
        for (int i = 0; i < stick1; i++)
        {
            stick1s[i].SetActive(true);
            stick1s[i].GetComponent<stick>().setPower(stick1power);
        }

        int stick2 = curStickInfo("stick2").count;
        float stick2power = getPower("stick2");
        for (int i = 0; i < stick2; i++)
        {
            stick2s[i].SetActive(true);
            stick2s[i].GetComponent<stick>().setPower(stick2power);
        }

        int stick3 = curStickInfo("stick3").count;
        float stick3power = getPower("stick3");
        for (int i = 0; i < stick3; i++)
        {
            stick3s[i].SetActive(true);
            stick3s[i].GetComponent<stick>().setPower(stick3power);
        }

        int stick4 = curStickInfo("stick4").count;
        float stick4power = getPower("stick4");
        for (int i = 0; i < stick4; i++)
        {
            stick4s[i].SetActive(true);
            stick4s[i].GetComponent<stick>().setPower(stick4power);
        }
    }
    public stickInfo curStickInfo(string id)
    {
        return dic_stick[id];
    }


}
