using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using JacobGames.SuperInvoke;
public class battleSequenceManager : MonoBehaviour
{
    public PlayableDirector openingani;
    public PlayableDirector loseani;
    public PlayableDirector winani;
    public PlayableDirector wingameani;
    public PlayableDirector losegameani;

    battle1Manager battle1Manager;
    public Text textScore;
    int win = 0;
    int lose = 0;
    string id;
    private void Start()
    {
        openingani.Play();
        this.id = ES3.Load<string>("battlestickid",defaultValue: "stick1");
        battle1Manager = battle1Manager.Instance;
        SuperInvoke.Run(10, () => battle1Manager.Instance.battleStart(onBattleEnded));
        
    }
    Dictionary<string, stickInfo> dic_stick;
    
    private void onBattleEnded(bool result)
    {

        if (result)
            win++;
        else
            lose++;
        textScore.text = win+" : "+lose;
        if (win == 5)
        {
            wingameani.Play();
            SuperInvoke.Run(2.3f,()=>
            {
                addStick();
                SceneManager.LoadScene(2);
            });
            return;
        }
        else if(lose==5)
        {
            losegameani.Play();
            SuperInvoke.Run(2.3f, () => SceneManager.LoadScene(2));
            return;
        }
        ISuperInvokeSequence sq = SuperInvoke.CreateSequence();
       
        if (result)
        {
            if(win==4)
            {
                winani.Play();

                sq.AddMethod(3, () => textScore.GetComponent<Animator>().SetTrigger("appear"));
                sq.AddMethod(1, () => battle1Manager.battleStart(onBattleEnded, true));
                sq.Run();
                //SuperInvoke.Run(3, () => battle1Manager.battleStart(onBattleEnded,true));
            }
            else
            {
                winani.Play();
                textScore.GetComponent<Animator>().SetTrigger("appear");
                
                sq.AddMethod(4, () => battle1Manager.battleStart(onBattleEnded));
                sq.Run();
                // SuperInvoke.Run(3, () => battle1Manager.battleStart(onBattleEnded));
            }
            
        }
        else
        {
            loseani.Play();
            sq.AddMethod(4, () => textScore.GetComponent<Animator>().SetTrigger("appear"));
            sq.AddMethod(2, () => battle1Manager.battleStart(onBattleEnded));
            sq.Run();
            //SuperInvoke.Run(5, () => battle1Manager.battleStart(onBattleEnded));
        }
    }
    public void addStick()
    {
        dic_stick = ES3.Load<Dictionary<string, stickInfo>>("stickManagerDic", dic_stick);
        stickInfo info = dic_stick[id];
        info.count++;
        ES3.Save<Dictionary<string, stickInfo>>("stickManagerDic", dic_stick);
    }
    
}
