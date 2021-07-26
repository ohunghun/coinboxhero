using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rainbowMachine : MonoBehaviour
{
    const float interval = 10;
    const float shootTime = 10;
    myCountDownTimer.countTImer leftTimer;
    Animator ani;
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
        leftTimer = myCountDownTimer.Instance.add("RainbowLazer", interval, OnCharged);
        leftTimer.restart();
        ani = gameObject.GetComponentInChildren<Animator>();
    }
    void OnShopUpdated()
    {
        if(Shop.Instance.isBuy(Shop.itemNames.RainbowLazer))
        {
            body.SetActive(true);
            init();
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

            ani.SetInteger("state", 2);
            box.Instance.isRainbow = true;
            myCountDownTimer.Instance.CountDown(shootTime, () =>
            {
                box.Instance.isRainbow = false;
                ani.SetInteger("state", 0);
                leftTimer.restart();


            });
        }
    }

    private void OnMouseUpAsButton()
    {
        shoot();
    }

}
