using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using CodeStage.AntiCheat.ObscuredTypes;
using mTypes;
public class cargo : Singleton<cargo>,saveObj
{

    public ObscuredLong money;
    public ObscuredLong vib;

    private void Awake()
    {
        //increaseMoney(0);
    }
   
    public void increaseMoney(long amount)
    {
        money += amount;
        
    }
    public void decreaseMoney(long amount)
    {
    
        money -=  amount;
        if (money < 0)
            money = 0;
    }
    public void increaseVib(long amount)
    {
        vib += amount;
    }
    public void decreaseVib(long amount)
    {
        vib = vib - amount;
        if (vib < 0)
            vib = 0;
    }
    public bool isEnoughVib(long amount)
    {
        return vib >= amount;
    }
    public bool isEnoughMoney(long amount)
    {
        return money >= amount;
    }
    public void SetMoneyTextColor(Text text,long value)
    {
        if (value < money)
            text.color = Color.red;
        else
            text.color = Color.black;
    }
    public void SetVibTextColor(Text text, long value)
    {
        if (value < vib)
            text.color = Color.red;
        else
            text.color = Color.black;
    }

    public void save()
    {
        ES3.Save<long>("money", money);
        ES3.Save<long>("vib", vib);
    }

    public void load()
    {
        money= ES3.Load<long>("money", 0);
        vib=ES3.Load<long>("vib", 0);
    }
    public void onload()
    {
        
    }

    public void init()
    {
        
    }

   
}
