
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class evolutionAnimation : MonoBehaviour
{

    public Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        HorseManager.Instance.AddOnOwnerUpdated(onOwnerUpdated);
        beforeLevel = HorseManager.Instance.getOwnStat().level;
    }
    int beforeLevel = 0;
    void onOwnerUpdated()
    {
        int cur=  HorseManager.Instance.getOwnStat().level;
        if (beforeLevel < cur)
        {
            joystick.Instance.controlDisable();
            HorseManager.Instance.PauseHorse();
            StartCoroutine("anima");
            beforeLevel = cur;
        }
    }
    IEnumerator anima()
    {
        
        if (HorseManager.Instance.getOwnStat().level == 1)
        {
            panelManager.Instance.openPanel("evolutionAni", animationFirstEvolution);
        }
        else if (HorseManager.Instance.getOwnStat().level == 11)
        {
            panelManager.Instance.openPanel("evolutionAni", animationEndding1);
        }
        else
        {
            panelManager.Instance.openPanel("evolutionAni", animationN);
        }
        yield return new WaitForSeconds(1);
    }
   
    void animationEndding1()
    {
        Queue<string> strs = new Queue<string>();
        Queue<Sprite> sps = new Queue<Sprite>();
        strs.Enqueue(Lang.Instance.getString("진화11_1"));
        strs.Enqueue(Lang.Instance.getString("진화11_2"));
        strs.Enqueue(Lang.Instance.getString("진화11_3"));
        sps.Enqueue(spritemanager.Instance.getSprite("horse11"));
        sps.Enqueue(spritemanager.Instance.getSprite("horse11"));
        sps.Enqueue(spritemanager.Instance.getSprite("horse11_sad"));
        panelManager.Instance.openPanel("dialog", endingplay, strs, sps);
    }
    void endingplay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("endding");
    }
    void animationFirstEvolution()
    {
        Queue<string> strs = new Queue<string>();
        Queue<Sprite> sps = new Queue<Sprite>();
        strs.Enqueue(Lang.Instance.getString("오프닝0"));
        sps.Enqueue(spritemanager.Instance.getSprite("horseScream"));
        panelManager.Instance.openPanel("dialog", OnOpeningDialogEnded, strs,sps);
    }
    void animationN()
    {
        int n = HorseManager.Instance.getOwnStat().level;
        Queue<string> strs = new Queue<string>();
        Queue<Sprite> sps = new Queue<Sprite>();
        strs.Enqueue(Lang.Instance.getString("진화"+n));
        sps.Enqueue(spritemanager.Instance.getSprite("horse"+n));
        panelManager.Instance.openPanel("dialog", () => { joystick.Instance.controlEnable(); HorseManager.Instance.ResumeHorse(); }, strs, sps);
    }
    void OnOpeningDialogEnded()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    
}
