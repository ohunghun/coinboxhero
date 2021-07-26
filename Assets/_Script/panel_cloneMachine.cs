using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class panel_cloneMachine : panel
{

    public Text cost_clone, cost_max, curmax, curclone;
    public Image img1, img2;
    public Animator ani;
 
    private void Start()
    {
        //ani = GetComponent<Animator>();
        uiUpdate();
    }
    public override void OnOpended(params object[] arg)
    {
        uiUpdate();
        ani.SetFloat("progress", 0);
        
    }
    void uiUpdate()
    {

        img2.sprite=img1.sprite = spritemanager.Instance.getSprite("horse" + HorseManager.Instance.getOwnStat().level);
        cost_clone.text= ""+getCostMakeClone(HorseManager.Instance.getCurHorse(),HorseManager.Instance.getCurHorse(), HorseManager.Instance.getCurmax());
        cost_max.text = "" + getCostMax(HorseManager.Instance.getCurmax() );
        curmax.text = "" + HorseManager.Instance.getCurmax();
        curclone.text = "" + HorseManager.Instance.getCurHorse();
       
    }
    public void makeClone()
    {
        long cost = (long)getCostMakeClone(HorseManager.Instance.getCurHorse(),HorseManager.Instance.getCurHorse(), HorseManager.Instance.getCurmax());

        if (cargo.Instance.isEnoughMoney(cost))
        {
           
        
            float prog = 0.2f;
            DOTween.To(() => prog, x =>
            {
                prog = x;
                ani.SetFloat("progress", prog);
            }, 1, 1);
            
            HorseManager.Instance.MakeClone();
            cargo.Instance.decreaseMoney(cost);
            uiUpdate();
        }
        else
        {
            SoundManager.getInstance().play("fail");
            panelManager.Instance.openPopup(null,"!!", Lang.Instance.getString("돈부족")); 
        }


    }
    
    public void increaseMax()
    {
        long cost = (long)getCostMax(HorseManager.Instance.getCurmax() );
        if (cargo.Instance.isEnoughVib(cost))
        {
            HorseManager.Instance.increaseMaxClone(5);
            cargo.Instance.decreaseVib(cost);
            SoundManager.getInstance().play("suc");
            uiUpdate();
        }
        else
        {
            SoundManager.getInstance().play("false");
            panelManager.Instance.openPopup(null, "!!", Lang.Instance.getString("비브라늄부족"));
        }
    }
    int getCostMakeClone(int curClone, int level, int max)
    {


        if (curClone ==0)
            return 100;
        if (curClone == 1)
            return 150;
        else if (curClone == 2)
            return 180;
        else if (curClone == 3)
            return 500;
        else if (curClone == 4)
            return 1000;
        else if (curClone == 5)
            return 2000;
        else
            return 2000 + 1000 * (1 - level)+(curClone-5) *10000;
    }
    int getCostMax(int max)
    {
        return 1000;
    }
    private void Update()
    {

    }
}
