using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using mTypes;
using CodeStage.AntiCheat.ObscuredTypes;
public class box : Singleton<box>, saveObj
{
    [HideInInspector]
     ObscuredFloat hp = 20;

    [HideInInspector]
    int level = 1;
    ArrayList list_onBoxHited=new ArrayList();
    ArrayList list_onBoxZeroHp = new ArrayList();
    coinGun coingun;
    public float powerAmp = 1;
    // Start is called before the first frame update
    private void Awake()
    {
        coingun = coinGun.Instance;
 
      
    }

    public bool isRainbow = false;

    public float getCurrentHp()
    {
        return hp;
    }
    public float getMAXHP()
    {
        return 25 + (level - 1) * 1000;
    }
    public void hit(float power)
    {
        hit(power, 0);
    }
    public void hit(float power,float xposition)
    {
        xposition = Mathf.Clamp(xposition, -0.6f, 0.6f);

        hit(power, xposition,true);
    }

    public void hit(float power,float position,bool sound=true)
    {

        if(sound)
            SoundManager.getInstance().play("box");
        hitAnimation();

        coingun.shootCoin(power, position, isRainbow);
        if (!coingun.isMushroomOnField()&&!coingun.isCoinFieldFull())
        {
            this.hp -= power* powerAmp;

            if (hp <= 0)
            {
                coingun.ShootMushroom(position);
                level += 1;
                hp = getMAXHP();
                foreach (callbackVoid callback in list_onBoxZeroHp)
                    callback();
                
            }
            foreach (callbackFloat callback in list_onBoxHited)
                callback(power);
        }
     
    }
   
    public void AddBoxHitCallback(callbackFloat callback)
    {
        list_onBoxHited.Add(callback);
    }
    public void AddBoxZeroHp(callbackVoid callback)
    {
        list_onBoxZeroHp.Add(callback);
    }
    void hitAnimation()
    {
        transform.DOComplete();
        transform.DOPunchPosition(new Vector3(Random.Range(-0.1f, 0.1f), 0.1f + Random.Range(0, 0.3f), 0), 0.2f, 20, 1, false);
    }
    
    public void save()
    {
        ES3.Save("box_hp", (float)hp);
        ES3.Save("box_level", level);
   
    }

    public void load()
    {
        
        level = ES3.Load("box_level", 1);
        hp = ES3.Load("box_hp", (float)getMAXHP());

    }

    public void onload()
    {
        
    }

    public void init()
    {
        
            
    }
}
