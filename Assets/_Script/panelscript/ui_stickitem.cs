using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ui_stickitem : MonoBehaviour
{
    public string id;
    public Image img;
    public Text power, count,powerCost,countCost;
    public GameObject plock, punlock;
    stickManager sm;
    private void Start()
    {
        
    }
    private void OnEnable()
    {
        if(sm==null)
        {
            sm = stickManager.Instance;
            stickManager.Instance.addOnStickUpdated(updateUI);
            img.sprite = spritemanager.Instance.getSprite(id);
        }
        updateUI();
    }
    void updateUI()
    {
        stickInfo info=sm.curStickInfo(id);
        
        if (info.count == 0)
        {
            plock.SetActive(true);
            punlock.SetActive(false);
        }
        else
        {
            plock.SetActive(false);
            punlock.SetActive(true);
            power.text = sm.getPower(id) + "";
            count.text = info.count + "";
            powerCost.text = getPowerupCost() + "";
            countCost.text = getCountUpCost() + "";
        }

        
    }
    public void upgradePower()
    {
        int cost = getPowerupCost();
        if(!cargo.Instance.isEnoughMoney(cost))
        {
            panelManager.Instance.openPopup(null,"!!", Lang.Instance.getString("돈부족"));
            SoundManager.getInstance().play("fail");
            return;
        }
        else
        {
            cargo.Instance.decreaseMoney(cost);
            sm.powerUpStick(id);
            SoundManager.getInstance().play("suc");
            savemanager.Instance.save();
        }
    }
    public void upgradeCount()
    {
        int cost = getCountUpCost();
        if (!cargo.Instance.isEnoughMoney(cost))
        {
            panelManager.Instance.openPopup(null, "!!", Lang.Instance.getString("돈부족"));
            SoundManager.getInstance().play("fail");
            return;
        }
        else
        {
            cargo.Instance.decreaseMoney(cost);
            sm.addStick(id);
            SoundManager.getInstance().play("suc");
            savemanager.Instance.save();
        }
    }
    int getPowerupCost()
    {
        stickInfo info = sm.curStickInfo(id);

        if (id=="stick1")
        {
            return 1000 + (info.level_power - 1) * 100;
        }else if(id=="stick2")
        {
            return 2000 + (info.level_power - 1) * 200;
        }
        else if (id == "stick3")
        {
            return 3000 + (info.level_power - 1) * 300;
        }
        else if (id == "stick4")
        {
            return 4000 + (info.level_power - 1) * 400;
        }
        return 10000;
    }
    int getCountUpCost()
    {
        stickInfo info = sm.curStickInfo(id);

        if (id == "stick1")
        {
            return 10000 + (info.level_power - 1) * 5000;
        }
        else if (id == "stick2")
        {
            return 20000 + (info.level_power - 1) * 5000;
        }
        else if (id == "stick3")
        {
            return 30000 + (info.level_power - 1) * 5000;
        }
        else if (id == "stick4")
        {
            return 40000 + (info.level_power - 1) * 5000;
        }
        return 100000;
    }
}
