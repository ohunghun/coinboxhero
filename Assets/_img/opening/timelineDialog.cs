using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
public class timelineDialog : Singleton<timelineDialog>
{
    public Text text,text_npc;
    public GameObject body;
    bool isAuto;
    float delay;
    PlayableDirector director;
    
    public void showDialog(PlayableDirector director, string str,string npcname, bool isAuto, float delay)
    {
        StopAllCoroutines();
        body.SetActive(true);
        this.director = director;
        this.delay = delay;
        this.isAuto = isAuto;

        text_npc.text = npcname;
        director.Pause();
        StartCoroutine(typeText(str));
    }
    IEnumerator typeText(string sentence)
    {
        text.text = "";
        foreach (char ch in sentence.ToCharArray())
        {
            text.text += ch;
            yield return new WaitForSeconds(0.1f);
        }
        if (isAuto)
        {
            director.Resume();
            yield return new WaitForSeconds(delay);
            body.SetActive(false);
        }
    }
    public void close()
    {
        Debug.Log("AA");
          
        body.SetActive(false);
        director.Resume();
    }
}
