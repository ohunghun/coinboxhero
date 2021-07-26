using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;
using System;
using JacobGames.SuperInvoke;
public class treeManager : Singleton<treeManager>
{
    public ObscuredInt powerAmpLevel;
    public ObscuredInt timeAmpLevel;
    public ObscuredInt rainbowTimeLevel;
    DateTime lastShootTime_powerAmp;
    DateTime lastShootTime_timeAmp;
    DateTime lastShootTime_rainbow;
    public Transform fruit_poweramp,fruit_timeAmp,fruit_rainbow;

    private void Start()
    {
        StartCoroutine(fruitGrowup_poweramp());
    }
    public float getCoolTimeRatio_powerAmp()
    {
        float cooltime = 60;
        float remain = Mathf.Clamp(cooltime - (DateTime.Now - lastShootTime_powerAmp).Seconds, 0, cooltime);
        return remain / cooltime;
    }
    public float getCoolTimeRatio_timeAmp()
    {
        float cooltime = 60;
        float remain = Mathf.Clamp(cooltime - (DateTime.Now - lastShootTime_timeAmp).Seconds, 0, cooltime);
        return remain / cooltime;
    }
    public float getCoolTimeRatio_rainbow()
    {
        float cooltime = 60;
        float remain = Mathf.Clamp(cooltime - (DateTime.Now - lastShootTime_rainbow).Seconds, 0, cooltime);
        return remain / cooltime;
    }
    public bool isActive_powerAmp;
    public bool isActive_timeAmp;
    public bool isActive_rainbow;
    IEnumerator fruitGrowup_poweramp()
    {
        while (true)
        {
            
            fruit_poweramp.localScale = Vector3.one*getCoolTimeRatio_powerAmp();
            float ratio = getCoolTimeRatio_powerAmp();
            if(ratio==1&&!isActive_powerAmp)
            {
                isActive_powerAmp = true;
                fruit_poweramp.GetChild(0).GetComponent<Animator>().SetBool("activate", true);
            }
            yield return new WaitForSeconds(1);
        }
    }
    public void shootPoweramp()
    {
        if (isActive_powerAmp)
        {
            box.Instance.powerAmp = getPowerAmp();
            isActive_powerAmp = false;
            SuperInvoke.Run(() =>
            {
                box.Instance.powerAmp = 1;

            }, 30);
        }
    }
    public void shootTimeAmp()
    {
        Time.timeScale = getTimeAmp();
        SuperInvoke.Run(() =>
        {
            Time.timeScale = 1;
        }, 30);
    }
    public void shootRainbow()
    {
        box.Instance.isRainbow = true;
        SuperInvoke.Run(() =>
        {
            box.Instance.isRainbow = false;
        }, getRainbowTime());
    }
    public void save()
    {
        ES3.Save<int>("tree_powerAmpLevel", powerAmpLevel);
        ES3.Save<int>("tree_timeAmpLevel", timeAmpLevel);
        ES3.Save<int>("tree_rainbowTimeLevel", rainbowTimeLevel);
        ES3.Save<DateTime>("tree_lastShootTime_powerAmp", lastShootTime_powerAmp);
        ES3.Save<DateTime>("tree_lastShootTime_timeAmp", lastShootTime_timeAmp);
        ES3.Save<DateTime>("tree_lastShootTime_rainbow", lastShootTime_rainbow);

    }
    
    public void load()
    {
        powerAmpLevel= ES3.Load<int>("tree_powerAmpLevel", 0);
        timeAmpLevel= ES3.Load<int>("tree_timeAmpLevel", 0);
        rainbowTimeLevel=ES3.Load<int>("tree_rainbowTimeLevel", 0);
        lastShootTime_powerAmp=ES3.Load<DateTime>("tree_lastShootTime_powerAmp", DateTime.Now);
        lastShootTime_timeAmp=ES3.Load<DateTime>("tree_lastShootTime_timeAmp", DateTime.Now);
        lastShootTime_rainbow= ES3.Load<DateTime>("tree_lastShootTime_rainbow", DateTime.Now);
    }

    public void levelup_powerAmpLevel()
    {
        powerAmpLevel++;
    }
    public void levelup_timeAmpLevel()
    {
        timeAmpLevel++;
    }
    public void levelup_rainbowTimeLevel()
    {
        rainbowTimeLevel++;
    }
    public float getPowerAmp()
    {
        return 1 + powerAmpLevel * 2;
    }
    public float getTimeAmp()
    {
        return 1 + powerAmpLevel * 2;
    }
    public float getRainbowTime()
    {
        return 30 + powerAmpLevel*5 ;
    }
   
}
