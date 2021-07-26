using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using mTypes;
using JacobGames.SuperInvoke;
public class battle1Manager : Singleton<battle1Manager>
{
    
 
    public Transform battlestickeks;
    Vector3[] pos;
    battleStick1[] sticks;
  
    List<int> intlist = new List<int>();
    public Transform own;
    public GameObject selecBtn;
    public callbackBool callback_battleEnded;
    public dialog hardcore;
    public AudioClip eyeOpenAudio;
    AudioSource ass;
    // Start is called before the first frame update
    void Start()
    {
        ass = GetComponent<AudioSource>();
        int num = getStickNum();
        sticks = new battleStick1[num];
        pos = new Vector3[battlestickeks.childCount];

        int i = 0;
        foreach (Transform child in battlestickeks)
        {
            if (i < num)
            {
                sticks[i] = child.GetComponent<battleStick1>();
                sticks[i].index = i;
                sticks[i].gameObject.SetActive(true);
            }
            pos[i] = child.position;
            intlist.Add(i);
            i++;
        }
        selecBtn.SetActive(false);

       
           
    }
    int getStickNum()
    {
        int level=ES3.Load<int>("stickBattle", 1);
        if (level == 1)
            return 11;
        else if (level == 2)
            return 15;
        else if (level == 3)
            return 18;
        else if (level == 4)
            return 26;
        return 10;
    }
    
    public void battleStart(callbackBool callback_battleEnd,bool hidebodyMode=false)
    {
        selecBtn.SetActive(false);
        this.callback_battleEnded = callback_battleEnd;
        if(!hidebodyMode)
            StartCoroutine(battleLootain());
        else
            StartCoroutine(battleLootain_hidebody());
    }
    IEnumerator battleLootain_hidebody()
    {
        LockPlayer(true);
        sticks[0].setTrueStick();
        ass.PlayOneShot(eyeOpenAudio);
        yield return new WaitForSeconds(5);
        foreach(battleStick1 stick in sticks)
        {
            stick.eyes.SetActive(true);
            stick.body.SetActive(false);
        }
        dialogManager.Instance.StartDialog(hardcore);
        yield return new WaitForSeconds(1.1f);
        for (int i = 0; i < 8; i++)
        {
            shuffle();
            yield return new WaitForSeconds(1.1f);
        }
        foreach (battleStick1 stick in sticks)
        {
            stick.eyes.SetActive(false);
            stick.body.SetActive(true);
        }
        LockPlayer(false);


    }
    IEnumerator battleLootain()
    {
        LockPlayer(true);
        sticks[0].setTrueStick();
        ass.PlayOneShot(eyeOpenAudio);
        yield return new WaitForSeconds(5);
        sticks[0].eyes.SetActive(false);
        for (int i = 0; i < 5; i++)
        {
            shuffle();
            yield return new WaitForSeconds(1.1f);
        }

        LockPlayer(false);
        

    }
    bool isSelected = false;
    public void SelectAnswer()
    {
        LockPlayer(true);
        foreach (battleStick1 s in sticks)
        {
            if (s.isSelected && s.isTrue)
            {
                sticks[0].setTrueStick();
                ass.PlayOneShot(eyeOpenAudio);
                SuperInvoke.Run(1.5f, () => { sticks[0].eyes.SetActive(false); callback_battleEnded(true); });
                
                return;
            }
            else{
                sticks[0].setTrueStick();
                ass.PlayOneShot(eyeOpenAudio);
                SuperInvoke.Run(1.5f, () => { sticks[0].eyes.SetActive(false); callback_battleEnded(false); });
                return;
            }
        }
       

    }
    
       
    
    public void appearStick()
    {
        foreach (battleStick1 stick in sticks)
        {
            stick.fadein();
        }
    }
    public void openEye()
    {
        foreach (battleStick1 stick in sticks)
        {
            stick.eyes.SetActive(true);
        }
    }
    void LockPlayer(bool val)
    {
        islockPlayer = val;
        if (val)
        {
           // battleJoystick.Instance.controlDisable();
            selecBtn.SetActive(false);
        }
        else
        {
           // battleJoystick.Instance.controlEnable();
            selecBtn.SetActive(true);
        }
        
    }
    public void shuffle()
    {
        ShuffleList<int>(intlist);
        for(int i=0;i<sticks.Length;i++)
        {
            battleStick1 stick = sticks[i];
            stick.index = intlist[i];
            stick.jump(pos[stick.index], Random.Range(1f, 2f));
        }
    }
    
    private void Update()
    {
        
        hightLightSelected();
        
    }
    bool islockPlayer = true;
    public void hightLightSelected()
    {
        if (islockPlayer)
        {
            foreach (battleStick1 stick in sticks)
            {
                stick.highlighted(false);
            }
                return;
        }

            
        float dismin = 9999999;
        battleStick1 selected = null;
        foreach (battleStick1 stick in sticks)
        {
            stick.highlighted(false);
            float distance = (own.position - stick.transform.position).sqrMagnitude;
            if (distance < dismin)
            {
                dismin = distance;
                selected = stick;
            }
        }
       
        selected.highlighted(true);
        
    }
    private List<T> ShuffleList<T>(List<T> list)
    {
        int random1, random2;
        T temp;

        for (int i = 0; i < list.Count; ++i)
        {
            random1 = Random.Range(0, list.Count);
            random2 = Random.Range(0, list.Count);

            temp = list[random1];
            list[random1] = list[random2];
            list[random2] = temp;
        }

        return list;
    }




}
