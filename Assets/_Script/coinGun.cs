using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;
public class coinGun : Singleton<coinGun>
{
    public GameObject prefab_Coin;
    Queue<coin> coin_spare=new Queue<coin>();
    public LinkedList<outScreenCoin> list_outScreenCoin = new LinkedList<outScreenCoin>();
    public LinkedList<coin> list_coinOnField = new LinkedList<coin>();
    Queue<coin> list_coinWaitToReturn = new Queue<coin>();
  
    public const int limitMax = 6000;
    public ObscuredInt level_LimitCoin = 1;
    public ObscuredInt level_DoubleCoin = 1;
    public ObscuredInt level_GoodCoin = 1;
    spritemanager spmanager;
    ScreenManager screenmanager;
    myRandom random1;
    myRandom random01;
    myRandom randomcoinspeed;
    doubleEffectManager doubleeffect;
    // Start is called before the first frame update
    void Awake()
    {
        screenmanager = ScreenManager.Instance;
        spmanager = spritemanager.Instance;
        random1 = new myRandom(-1f, 1f);
        random01 = new myRandom(0f, 1f);
        randomcoinspeed = new myRandom(0.9f, 1.4f);
        HorseManager.Instance.AddOnOwnerUpdated(onOwnerUpdate);
        doubleeffect = doubleEffectManager.Instance;
        setCoinColorPrepareTable();
        updateArg();

    }
    public void save()
    {
        ES3.Save<int>("level_LimitCoin", level_LimitCoin);
        ES3.Save<int>("level_DoubleCoin", level_DoubleCoin);
        ES3.Save<int>("level_GoodCoin", level_GoodCoin);
    }

    public void load()
    {
        level_LimitCoin=ES3.Load<int>("level_LimitCoin", 1);
        level_LimitCoin= ES3.Load<int>("level_DoubleCoin", 1);
        level_LimitCoin= ES3.Load<int>("level_GoodCoin", 1);
    }

    public void onload()
    {
        
    }
    public void init()
    {
        
    }
    void onOwnerUpdate()
    {
      
        updateArg();
     
    }
    
    private void Start()
    {
        fillCoin();
        StartCoroutine("_init");
    }
    IEnumerator _init()
    {
        yield return null;
        updateArg();
    }
    public int Num_coinOnField()
    {
        return list_coinOnField.Count;
    }
    public void maxCoinUp()
    {
        level_LimitCoin++;
    }
    public int maxGoodCoin()
    {
        return 49;
    }
    public int getLimitCoin()
    {
        return 10 + (level_LimitCoin-1) * 50;
    }
    public void doubleCoinUp()
    {
        level_DoubleCoin++;
    }
    public void GoodCoinUp()
    {
        level_GoodCoin++;
        setCoinColorPrepareTable();
    }
    public float getGoodCoin()
    {
        return  0+ (level_GoodCoin - 1) * 2f;
    }
    public float getDoubleCoin()
    {
        return 0.01f + (level_DoubleCoin-1) * 0.05f;
    }
    private void FixedUpdate()
    {
        processWaitReturnCoin();
        gatherOutScreenCoin();
    }
    public class outScreenCoin
    {
        public coin c;
        public float time;
        public  outScreenCoin(coin c, float time)
        {
            this.c = c;
            this.time = time;
        }
        public bool isTimeout()
        {
            return Time.realtimeSinceStartup - time > 2f;
        }
    }
    
    private void gatherOutScreenCoin()
    {
        LinkedListNode<outScreenCoin> current = list_outScreenCoin.First;
        int i = 0;
        while(current!=null)
        {
            if (i++ > 200)
                break;
            LinkedListNode<outScreenCoin> temp = current;
            current = current.Next;
            if (temp.Value.isTimeout())
            {
                list_outScreenCoin.Remove(temp);
                coin_spare.Enqueue(temp.Value.c);
                temp.Value.c.gameObject.SetActive(false);
            }
        }
    }
    void processWaitReturnCoin()
    {
        long total = 0;
        
        if (list_coinWaitToReturn.Count > 30)
            SoundManager.getInstance().play("32coin" + Random.Range(1, 4),0.5f);
        else if(list_coinWaitToReturn.Count > 8)
            SoundManager.getInstance().play("8coins", 0.5f);
        else if(list_coinWaitToReturn.Count>0)
            SoundManager.getInstance().play("coin", 0.5f);
        for (int i = 0; i <300;i++)
        {
            if (list_coinWaitToReturn.Count == 0)
                break;
            coin coin = list_coinWaitToReturn.Dequeue();
            
          
            coin_spare.Enqueue(coin);
            coin.gameObject.SetActive(false);
            total += (long)coin.myColor;
        }  
        cargo.Instance.increaseMoney(total);
    }
    void fillCoin()
    {
        int curTotal = coin_spare.Count + list_coinOnField.Count;
        int remain = limitMax - curTotal;
        for (int i = 0; i < remain; i++)
        {
            coin coin = Instantiate(prefab_Coin).GetComponent<coin>();
            coin.gameObject.SetActive(false);
            coin_spare.Enqueue(coin);
        }
    }
   
    int count = 0;
    int lucky = 1;
    public void upgradeLucky()
    {
        lucky = Mathf.Clamp(lucky, 1, 50);
        
    }
    int randomCount = 400;
   public  float padding = -4;
    public float val2 = 0.4f;
    float minheight=3;
    
    public void shootCoin(float power,float position,bool rainbow=false)
    {
        
        int coinNum =(int) (power+1);
        if (getDoubleCoin() > random01.get())
        {
            coinNum *= 2;
            doubleeffect.ShootEffect();
        }


        if (coinNum > coin_spare.Count)
            coinNum = coin_spare.Count;
        if (coinNum == 0)
            return;
        if(coinNum+Num_coinOnField()>=getLimitCoin())
        {
            coinNum = getLimitCoin() - Num_coinOnField();
        }

        float landingpos = getLandingpos2(position);

        
        for (int i=0;i<coinNum;i++)
        {
            coin coin = coin_spare.Dequeue();
            coin.gameObject.SetActive(true);
            Vector2 rpos = coinposition.get(landingpos);
            
          
            if (!rainbow)
            {
                coin.init(listPrepareCoinColor[curCoinColorIndex], rpos, (random1.get() + 1) * randomheightTick + minheight, randomcoinspeed.get() + timeSlow);
                curCoinColorIndex = (curCoinColorIndex + 1) % listPrepareCoinColor.Count;
            }
            else
            {
                coin.init(coin.CoinColor.rainbow, rpos, (random1.get()+1) * randomheightTick + minheight, randomcoinspeed.get() + timeSlow);
            }
            coin.transform.position = transform.position+new Vector3(0,0.5f,0);
            if (rpos.x < screenmanager.screenLeftToWorldX || rpos.x > screenmanager.screenRightToWorldX)
            {
                list_outScreenCoin.AddLast(new LinkedListNode<outScreenCoin>(new outScreenCoin(coin, Time.realtimeSinceStartup)));
            }
            else
            {
                list_coinOnField.AddFirst(coin);
            }
        }

    }
    int curCoinColorIndex = 0;
    List<coin.CoinColor> listPrepareCoinColor = new List<coin.CoinColor>();
    void setCoinColorPrepareTable()
    {
        Debug.Log("set");
        listPrepareCoinColor.Clear();
        for (int i = 0; i < 10000; i++)
        {
            float r = random01.get() * 100f;
            float ag = getGoodCoin();

            if (r < 99 - ag)
                listPrepareCoinColor.Add(coin.CoinColor.copper);
            else if (r < (1099 - ag) * 0.09f)
                listPrepareCoinColor.Add(coin.CoinColor.silver);
            else
                listPrepareCoinColor.Add(coin.CoinColor.gold);
        }
    }
    
    public mushroom myMushroom;
    public void ShootMushroom(float position)
    {
        myMushroom.gameObject.SetActive(true);
        myMushroom.transform.position = transform.position + new Vector3(0, 0.5f, 0);
        myMushroom.init(new Vector2(5,-4), Random.Range(3f, 7f));
    }
    public bool isMushroomOnField()
    {
        return myMushroom.gameObject.activeSelf;
    }
    public void returnCoin(coin coin)
    {
       
        list_coinWaitToReturn.Enqueue(coin);
        
    }
    coinGroundPostionGenerator coinposition;
 
    float randomheightTick;
    float screenWidthToWorldLength;
    float timeSlow;
    void updateArg()
    {

        
        int level = HorseManager.Instance.getOwnStat().level;
        timeSlow = -Mathf.Clamp(0.025f * level, 0, 0.25f);
        
        float screenRightToWorldX = Camera.main.ScreenToWorldPoint(
           new Vector2(Screen.width, 0)).x;
        float screenLeftToWorldX = Camera.main.ScreenToWorldPoint(
            new Vector2(0, 0)).x;
        float screenTopToworld = Camera.main.ScreenToWorldPoint(
            new Vector2(0, Screen.height)).y;
        float screenBotToWorld = Camera.main.ScreenToWorldPoint(
            new Vector2(0, 0)).y;
         screenWidthToWorldLength = screenRightToWorldX - screenLeftToWorldX;
        float screenHeightWorldLength = screenTopToworld - screenBotToWorld;

        if (level <= 2)
        {
            Debug.Log("SDgsdg");
            randomheightTick = ScreenManager.Instance.screenHeightWorldLength * 0.15f;
        }
        else if (level <= 4)
            randomheightTick = ScreenManager.Instance.screenHeightWorldLength * 0.3f;
        else if (level <= 5)
            randomheightTick = ScreenManager.Instance.screenHeightWorldLength * 0.5f;
        else if (level < 7)
            randomheightTick = ScreenManager.Instance.screenHeightWorldLength * 0.6f;
        else
            randomheightTick = ScreenManager.Instance.screenHeightWorldLength * 0.65f;


        coinposition = new coinGroundPostionGenerator(screenWidthToWorldLength);
    }
    public bool isCoinFieldFull()
    {
        if (Num_coinOnField() >= getLimitCoin())
            return true;
        return false;
    }
    public float getLandingpos2(float hitpos)
    {

        return screenWidthToWorldLength * 2 * hitpos * hitpos* hitpos;
    }

   
}
