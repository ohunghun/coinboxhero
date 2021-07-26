using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class stickani : MonoBehaviour
{
    public string id;
    Animator ani;
    Transform body;
    BoxCollider col;
    private void Start()
    {
        body = transform.GetChild(0);
        col = GetComponent<BoxCollider>();
        HorseManager.Instance.AddOnInit(OnHorseInit);
        HorseManager.Instance.AddOnOwnerUpdated(updateani);
        stickManager.Instance.addOnLoaded(onstickinit);
        stickManager.Instance.addOnStickUpdated(updateani);
       
        ani = body.GetComponent<Animator>();
       
    }
    void onstickinit()
    {
        stickinit = true;
        if (horseinit && stickinit)
            updateani();
    }
    void OnHorseInit()
    {
        horseinit = true;
        if (horseinit && stickinit)
            updateani();
    }
    bool horseinit = false;
    bool stickinit = false;
    private void updateani()
    {
        int curHorseLevel = HorseManager.Instance.getOwnStat().level;
        int curStickCount = stickManager.Instance.curStickInfo(id).count;
        if(curStickCount>0)
        {
            body.gameObject.SetActive(false);
            col.enabled = false;
            return;
        }
        else
        {

            if (id == "stick1")
            {
                
                if (curHorseLevel >= 2)
                {
                    body.gameObject.SetActive(true);
                    col.enabled = true;
                }
                else
                {
                    body.gameObject.SetActive(false);
                    col.enabled = false;
                }
            }
            else if (id == "stick2")
            {
                if (curHorseLevel >= 3)
                {
                    body.gameObject.SetActive(true);
                    col.enabled = true;
                }
                else
                {
                    body.gameObject.SetActive(false);
                    col.enabled = false;
                }
            }
            else if (id == "stick3")
            {
                if (curHorseLevel >= 4)
                {
                    body.gameObject.SetActive(true);
                    col.enabled = true;
                }
                else
                {
                    body.gameObject.SetActive(false);
                    col.enabled = false;
                }
            }
            else if (id == "stick4")
            {
                if (curHorseLevel >= 5)
                {
                    body.gameObject.SetActive(true);
                    col.enabled = true;
                }
                else
                {
                    body.gameObject.SetActive(false);
                    col.enabled = false;
                }
            }
        }
        
    }

    bool interLock = false;
    private void OnMouseUpAsButton()
    {
      
       if (ScreenManager.IsPointerOverUIObject(Input.mousePosition)||interLock)
           return;

        interLock = true;
        Queue<string> d = new Queue<string>();
        Queue<Sprite> s = new Queue<Sprite>();
        d.Enqueue(Lang.Instance.getString(id+"_1"));
        d.Enqueue(Lang.Instance.getString(id + "_2"));
        d.Enqueue(Lang.Instance.getString(id + "_3"));
        s.Enqueue(spritemanager.Instance.getSprite(id));
        s.Enqueue(spritemanager.Instance.getSprite(id));
        s.Enqueue(spritemanager.Instance.getSprite(id));
        object[] o = new object[2];
        o[0] = d;
        o[1] = s;
        float f = 0;
        DOTween.To(() => f, x =>
           {
               ani.SetFloat("progress", x);
           }, 0.99f, 1).OnComplete(() =>
           {
               ani.SetFloat("progress", 0.99f);
               mainMusicManager.Instance.playStick();
               panelManager.Instance.openPanel("dialog",()=>
               {
                   stickManager.Instance.addStick(id);
                   mainMusicManager.Instance.playNormalSound();
               }, o);
           }).Play();

    }
}
