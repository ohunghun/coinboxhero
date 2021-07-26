using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerAmp : MonoBehaviour
{
    const float interval = 10;
    const float effectTime = 10;
    myCountDownTimer.countTImer leftTimer;
    Animator ani;
    public GameObject body;
    int level = 1;
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
        leftTimer = myCountDownTimer.Instance.add("powerAmp", interval, OnCharged);
        leftTimer.restart();
        ani = gameObject.GetComponentInChildren<Animator>();
       
    }
    void OnShopUpdated()
    {
        if(Shop.Instance.isBuy(Shop.itemNames.PowerAmp))
        {
            body.SetActive(true);
            init();
            level = Shop.Instance.getLevel(Shop.itemNames.PowerAmp);
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

            myCountDownTimer.Instance.CountDown(1f, () => {
                List<GameObject> t = HorseManager.Instance.getAllHorse();
                foreach (GameObject horse in t)
                {
                    horse.GetComponent<Horse>().powerAmpUp(getPower(), effectTime);
                }
                leftTimer.restart();
                ani.SetInteger("state", 0);
                }
            );
            
        }
    }
    float getPower()
    {
        return 2 + 0.5f * (level - 1);
    }
    private void OnMouseUpAsButton()
    {
        shoot();
    }
    
}
