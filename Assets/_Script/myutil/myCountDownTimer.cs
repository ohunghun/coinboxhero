using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myCountDownTimer : Singleton<myCountDownTimer>
{
    public delegate void OnTimerZero();
    Dictionary<string, countTImer> listTimer = new Dictionary<string, countTImer>();
    List<countTImer> listTimer_erase = new List<countTImer>();
    public countTImer add(string key,float sec,OnTimerZero callback)
    {
        if(listTimer.ContainsKey(key))
            listTimer.Remove("key");
        listTimer.Add(key, new countTImer(sec, callback));
        return listTimer[key];
    }
    public countTImer CountDown(float sec,OnTimerZero callback)
    {
        countTImer t = new countTImer(sec, callback);
        listTimer_erase.Add(t);
        t.restart();
        return t;

    }
    public void RemoveCountDown(countTImer t)
    {
        if(listTimer_erase.Contains(t))
            listTimer_erase.Remove(t);
    }
    
    public countTImer getTimer(string key)
    {
        return listTimer[key];
    }
    private void FixedUpdate()
    {
        foreach(countTImer t in listTimer.Values)
            t.CountDown();
        
        foreach (countTImer t in listTimer_erase.ToArray())
        {
            float left=t.CountDown();
        }
        listTimer_erase.RemoveAll(elem => elem.left<=0);
    }
    
    public class countTImer
    {
        public float initSec = 0;
        public float left;
        OnTimerZero callback;
        bool isCallbackCall = false;
        public bool pause = true;
        float div_init;
        public  countTImer(float sec,OnTimerZero callback)
        {
            initSec = sec ;
            this.left = sec;
            this.callback = callback;
            div_init = 1f / initSec;

        }
        public countTImer(float sec, OnTimerZero callback,float startLeft)
        {
            initSec = sec;
            this.left = startLeft;
            this.callback = callback;
            div_init = 1f / initSec;

        }
        public float getLeftRatio()
        {
            return left * div_init;
        }
        public float CountDown()
        {

            if (isCallbackCall || pause)
                return left;
            left -= Time.deltaTime;
            if (left <= 0)
            {
                isCallbackCall = true;
                callback();
            }
            return left;
        }
        public void restart()
        {
            isCallbackCall = false;
            left = initSec;
            pause = false;
        }
        public void setpause(bool val)
        {
            pause = val;
        }
        

    }

}
