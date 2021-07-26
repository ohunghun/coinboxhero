using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UFOStuff_Button : MonoBehaviour
{
    public Shop.itemNames item;
    public GameObject imglock, imgItem;
    public Text text_cost,text_level;
    
    void Update()
    {
        if (Shop.Instance.isBuy(item))
        {
            imglock.SetActive(false);
            imgItem.SetActive(true);
            text_cost.text = Shop.Instance.getCostLevelUp(item) + "";
            ;text_level.text = "lv. "+ Shop.Instance.getLevel(item);
        }
        else
        {
            imglock.SetActive(true);
            imgItem.SetActive(false);
            text_cost.text = Shop.Instance.getBuyCost(item) + "";
            text_level.text = "";
        }
    }
    public void OnClick()
    {
        if (Shop.Instance.isBuy(item))
        {
            if (Shop.Instance.levelUp(item))
            {
                Debug.Log("a");
                SoundManager.getInstance().play("suc");
            }
            else
                SoundManager.getInstance().play("fail");
        }
        else
        {
            if (Shop.Instance.buy(item))
                SoundManager.getInstance().play("suc");
            else
                SoundManager.getInstance().play("fail");
        }
    }
    
}
