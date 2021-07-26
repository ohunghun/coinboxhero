using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;
using mTypes;
using System;

public class Shop : Singleton<Shop>, saveObj
{
    public enum itemNames{TimeMachine,RainbowLazer,PowerAmp,Weapon};
    Dictionary<itemNames, bool> buyStates = new Dictionary<itemNames, bool>();
    Dictionary<itemNames, ObscuredInt> itemLevel = new Dictionary<itemNames, ObscuredInt>();
    List<callbackVoid> list_callback = new List<callbackVoid>();
    
    public bool levelUp(itemNames itemName)
    {
        int cost = getCostLevelUp(itemName);
        if (cargo.Instance.isEnoughVib((long)cost))
        {
            cargo.Instance.decreaseVib((long)cost);
            itemLevel[itemName] = itemLevel[itemName] + 1;
            savemanager.Instance.save();
            callback();
            return true;
        }
        return false;

    }
    public int getLevel(itemNames itemName)
    {
        return itemLevel[itemName];
    }
    public int getCostLevelUp(itemNames itemName)
    {
        switch(itemName)
        {
            case itemNames.TimeMachine:
                return 1000 + 200 * (getLevel(itemName) - 1);
            case itemNames.RainbowLazer:
                return 1000 + 200 * (getLevel(itemName) - 1);
            case itemNames.PowerAmp:
                return 1000 + 200 * (getLevel(itemName) - 1);
            case itemNames.Weapon:
                return 0;
        }
        return 0;
    }
    public int getBuyCost(itemNames itemName)
    {
        switch (itemName)
        {
            case itemNames.TimeMachine:
                return 1000;
            case itemNames.RainbowLazer:
                return 1000;
            case itemNames.PowerAmp:
                return 2000;
            case itemNames.Weapon:
                return 0;
        }
        return 0;
    }
    public bool buy(itemNames itemName)
    {
        int cost = getBuyCost(itemName);
        if(cargo.Instance.isEnoughVib((long)cost))
        {
            cargo.Instance.decreaseVib((long)cost);
            buyStates[itemName] = true;
            savemanager.Instance.save();
            callback();
            return true;
        }
        return false;
    }
    public bool isBuy(itemNames itemName)
    {
        return buyStates[itemName];
    }
   
    public void init()
    {
        for (int i = 0; i < Enum.GetNames(typeof(itemNames)).Length; i++)
        {
            buyStates[(itemNames)i] = false;
            itemLevel[(itemNames)i] = 1;
            
        }
        
        callback();
    }
    public void addCallback(callbackVoid callback)
    {
        list_callback.Add(callback);
    }
    void callback()
    {
        foreach (callbackVoid callback in list_callback)
        {
            callback();
        }
    }
    public void load()
    {
     
    }

    public void onload()
    {
     
    }

    public void save()
    {
     
    }

   
}
