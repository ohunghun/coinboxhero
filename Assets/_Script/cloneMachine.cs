using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cloneMachine : MonoBehaviour
{
    Text cost_clone, cost_max,curmax,curclone;
    Animator ani;
    private void Start()
    {
        ani = GetComponent<Animator>();
    }
    public void makeClone()
    {
        long cost = (long)getCostMakeClone(HorseManager.Instance.getCurHorse(), HorseManager.Instance.getCurmax());

        if (cargo.Instance.isEnoughMoney(cost))
        {
            ani.SetTrigger("shoot");
            HorseManager.Instance.MakeClone();
            cargo.Instance.decreaseMoney(cost);
            //myCountDownTimer.Instance.CountDown(4, () => openUFoPanel.Instance.closePanel());

        }
        else
        {
            SoundManager.getInstance().play("false");
        }
        
        
    }
    void uiUpdate()
    {

    }
    public void increaseMax()
    {
        long cost = (long)getCostMax(HorseManager.Instance.getCurmax() + 5);
        if(cargo.Instance.isEnoughMoney(cost))
        {
            HorseManager.Instance.increaseMaxClone(5);
            cargo.Instance.decreaseMoney(cost);
            SoundManager.getInstance().play("suc");
           
        }
        else
        {
            SoundManager.getInstance().play("false");
        }
    }
    int getCostMakeClone(int level,int max)
    {
        int t1 = 1000 + 100 * (1 - level);
        int t2 = 10 * (1 - max);
        return 0;// t1 + t2;
    }
    int getCostMax(int max)
    {
        return 100000 + 500 * (5 - max);
    }
    private void Update()
    {
        
    }
}
