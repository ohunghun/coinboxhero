using DG.Tweening;
using mTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class panel_dialog : panel
{
    public Queue<string> cur;
    public Queue<Sprite> cur_sp;
    
    
    public override void OnOpended(params object[] arg)
    {

        cur = (Queue<string>)arg[0];
        cur_sp = (Queue<Sprite>)arg[1];
    
        text.text = "";
        nextSign.SetActive(false);
        string str = cur.Dequeue();
        Sprite sp = null;
        if (cur_sp!=null&&cur_sp.Count>0)
            sp = cur_sp.Dequeue();
        img.sprite = sp;
        text.DOText(str, 1.5f).OnComplete(() => nextSign.SetActive(true));
        if(HorseManager.Instance.getOwnStat().level==1)
        {
            mainMusicManager.Instance.playConfuse();
        }
    }
    public override void OnClosed()
    {

    }

    public Text text;
    public Image img;
    public GameObject nextSign;
    public void Next()
    {
        
        if (nextSign.activeSelf)
        {
            if (cur.Count == 0)
            {
                
                text.text = "";
                img.sprite = null;
                panelManager.Instance.closeCurrent();

                mainMusicManager.Instance.playNormalSound();


            }
            else
            {
                nextSign.SetActive(false);
                text.text = "";
                string str = cur.Dequeue();
                Sprite sp = null;
                if (cur_sp != null && cur_sp.Count > 0)
                    sp = cur_sp.Dequeue();
                text.DOText(str, 1).OnComplete(() => nextSign.SetActive(true));
                img.sprite = sp;
            }
        }
    }
}
