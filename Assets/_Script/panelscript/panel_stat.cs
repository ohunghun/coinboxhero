using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class panel_stat : panel
{
    public Image horseimg,weaponimg;
    public TextMeshProUGUI Mushroom,power,BonusPower, speed,weapon;
    public Text cost_BonusPower,  cost_speed, cost_weapon;
    private void Start()
    {
        HorseManager.Instance.AddOnOwnerUpdated(updateUI);
    }
    public override void OnOpended(params object[] arg)
    {
        updateUI();
    }
    void updateUI()
    {
        Stat curstat = HorseManager.Instance.getOwnStat();
        Horse curhorse = HorseManager.Instance.Own.GetComponent<Horse>();
        BonusPower.text = curstat.BonusPower + "";
        speed.text = curhorse.getSpeed() + "";
        Mushroom.text = (curstat.level) + "/11";
        weapon.text = curstat.weaponLevel + "lv";
        power.text = curhorse.power+" + " + curstat.BonusPower + "";
        cost_BonusPower.text = getCostBonuspowerUp(curstat.BonusPower+1)+"";
        cost_speed.text = getCostSpeed(curstat.speed + 1) + "";
        cost_weapon.text = getCostWeaponUp(curstat.weaponLevel) + "";
        horseimg.sprite = spritemanager.Instance.getSprite("horse" + curstat.level);
        weaponimg.sprite = spritemanager.Instance.getSprite("weapon" + curstat.weaponLevel);
    }
  
    public void ClickWeapon()
    {
        Stat curstat = HorseManager.Instance.getOwnStat();
        long cost = getCostWeaponUp(curstat.weaponLevel + 1);
        if (cargo.Instance.isEnoughMoney(cost))
        {
            cargo.Instance.decreaseMoney(cost);
            HorseManager.Instance.WeaponUp();
            SoundManager.getInstance().play("suc");
        }
        else
        {
            panelManager.Instance.openPopup(spritemanager.Instance.getSprite("money"), Lang.Instance.getString("돈부족"), "");
            SoundManager.getInstance().play("fail");
        }
    }
    public void ClickBonusPower()
    {
        Stat curstat = HorseManager.Instance.getOwnStat();
        long cost = getCostBonuspowerUp(curstat.BonusPower + 1);
        if (cargo.Instance.money >= cost)
        {
            cargo.Instance.decreaseMoney(cost);
            HorseManager.Instance.bonusPowerUp();
            SoundManager.getInstance().play("suc");
        }else
        {
            panelManager.Instance.openPopup(spritemanager.Instance.getSprite("money"), Lang.Instance.getString("돈부족"), "");
            SoundManager.getInstance().play("fail");
        }
    }
    public void Clickspeed()
    {
 
        Stat curstat = HorseManager.Instance.getOwnStat();
        long cost = getCostSpeed(curstat.speed + 1);
        if (cargo.Instance.money >= cost)
        {
            cargo.Instance.decreaseMoney(cost);
            HorseManager.Instance.speedUp();
            SoundManager.getInstance().play("suc");
        }
        else
        {
            panelManager.Instance.openPopup(spritemanager.Instance.getSprite("money"), Lang.Instance.getString("돈부족"), "");
            SoundManager.getInstance().play("fail");
        }
    }
    public long getCostWeaponUp(int level)
    {
        return 10000 + ((long)level - 1) * 10000;
    }
    long getCostBonuspowerUp(int level)
    {
        return 100 + ((long)level - 1) * 50;
    }
    long getCostGoodCoin(int level)
    {
        return 500 + ((long)level - 1) * 1000;
    }
    long getCostdoubleCoin(int level)
    {
        return 400 + ((long)level - 1) * 2000;
    }
    long getCostSpeed(int level)
    {
        return 400 + ((long)level - 1) * 2000;
    }
}
