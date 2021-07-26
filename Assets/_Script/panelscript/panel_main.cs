using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class panel_main : panel
{
    public Text coinLimit, doubleCoin,goodCoin;
    public Text coinLimit_cost, doubleCoin_cost,goodCoin_cost;
    public override void OnOpended(params object[] arg)
    {
        updateUI();
    }
    public void ClickgoodCoinUp()
    {
     
        if (cargo.Instance.isEnoughMoney((long)cost_goodCoinUp()))
        {
            cargo.Instance.decreaseMoney((long)cost_goodCoinUp());
            coinGun.Instance.GoodCoinUp();
           
            SoundManager.getInstance().play("suc");
            updateUI();
        }
        else
        {
            panelManager.Instance.openPopup(spritemanager.Instance.getSprite("money"), Lang.Instance.getString("돈부족"), "");
            SoundManager.getInstance().play("fail");
        }
    }
    public void ClickCoinLimitUp()
    {
        if(coinGun.Instance.getLimitCoin()>=coinGun.limitMax)
        {
            return;
        }
        if(cargo.Instance.isEnoughMoney((long)cost_coinLimitUp()))
        {
            cargo.Instance.decreaseMoney((long)cost_coinLimitUp());
            coinGun.Instance.maxCoinUp();
           
            SoundManager.getInstance().play("suc");
            updateUI();
        }
        else
        {
            panelManager.Instance.openPopup(spritemanager.Instance.getSprite("money"), Lang.Instance.getString("돈부족"), "");
            SoundManager.getInstance().play("fail");
        }
    }
    public void ClickDoubleCoinUp()
    {

        if (coinGun.Instance.getDoubleCoin() >= 1)
            return;
        if(cargo.Instance.isEnoughMoney((long)cost_doubleCoinUp()))
        {
            cargo.Instance.decreaseMoney((long)cost_doubleCoinUp());
            coinGun.Instance.doubleCoinUp();
            
            SoundManager.getInstance().play("suc");
            updateUI();
        }
        else
        {
      
            panelManager.Instance.openPopup(spritemanager.Instance.getSprite("money"), Lang.Instance.getString("돈부족"), "");
            SoundManager.getInstance().play("fail");
        }
    }
    void updateUI()
    {
        coinLimit.text = coinGun.Instance.getLimitCoin() + "";
        doubleCoin.text = (coinGun.Instance.getDoubleCoin()*100f).ToString("N1") + "%";
        goodCoin.text = coinGun.Instance.getGoodCoin() + "%";
        coinLimit_cost.text = cost_coinLimitUp() + "";
        doubleCoin_cost.text = cost_doubleCoinUp() + "";
        goodCoin_cost.text = cost_goodCoinUp() + "";
        if (coinGun.Instance.getLimitCoin() >= coinGun.limitMax)
            coinLimit_cost.text = "MAX";
        
        
       
    }
    int cost_coinLimitUp()
    {
        int level = coinGun.Instance.level_LimitCoin;
        if (level<10)
            return level*10;
        else
            return 20 + (level - 1)*20;
    }
    int cost_doubleCoinUp()
    {
        return 100 + (coinGun.Instance.level_DoubleCoin - 1)*500;
    }
    int cost_goodCoinUp()
    {
        return 100 + (coinGun.Instance.level_GoodCoin - 1) * 500;
    }
}
