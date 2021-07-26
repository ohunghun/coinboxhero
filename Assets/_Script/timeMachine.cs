using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeMachine : MonoBehaviour
{
    const float interval = 10;
    const float shootTime = 10;
    myCountDownTimer.countTImer leftTimer;
    Animator ani;
    int level=1;
    public GameObject body;
    private void Start()
    {
        Shop.Instance.addCallback(OnShopUpdated);
        
        OnShopUpdated();
    }
    bool isinit = false;
    private void init()
    {
        if (isinit)
            return;
        isinit = true;
        leftTimer = myCountDownTimer.Instance.add("timeMachine", interval, OnCharged);
        leftTimer.restart();
        ani = gameObject.GetComponentInChildren<Animator>();
     
     
    }
    void OnShopUpdated()
    {
        if (Shop.Instance.isBuy(Shop.itemNames.TimeMachine))
        {
            body.SetActive(true);
            init();
            level = Shop.Instance.getLevel(Shop.itemNames.TimeMachine);
        }
        else
        {
            body.SetActive(false);
        }
    }
    void OnCharged()
    {
        ani.SetInteger("state", 1);
    }
    void shoot()
    {
        if (leftTimer.left <= 0)
        {
            attackOn();
            myCountDownTimer.Instance.CountDown(shootTime, () =>
            {
                attackOff();
            });
        }
    }

    void attackOn()
    {
        ani.SetInteger("state", 2);
        Time.timeScale = getPower();
    }
    void attackOff()
    {
        ani.SetInteger("state", 0);
        leftTimer.restart();
        Time.timeScale = 1;
    }
    float getPower()
    {
        return 1+0.5f*(level-1);
    }
    private void OnMouseUpAsButton()
    {
        shoot();
    }
}
