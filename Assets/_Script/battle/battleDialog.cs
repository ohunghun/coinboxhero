using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
public class battleDialog : MonoBehaviour
{
    public Queue<string> cur=new Queue<string>();
    


    void Start()
    {
        cur.Enqueue(Lang.Instance.getString("battle_open1"));
        cur.Enqueue(Lang.Instance.getString("battle_open2"));
        cur.Enqueue(Lang.Instance.getString("battle_open3"));
        text.text = "";
        
        string str = cur.Dequeue(); 
        text.DOText(str, 1.5f).OnComplete(() => nextSign.SetActive(true));
       
    }
   
    public Text text;
    public PlayableDirector playgameani;
    public GameObject nextSign;
    public void Next()
    {

        if (nextSign.activeSelf)
        {
            if (cur.Count == 0)
            {

                text.text = "";
                playgameani.Play();
                gameObject.SetActive(false);


            }
            else
            {
                nextSign.SetActive(false);
                text.text = "";
                string str = cur.Dequeue();
                text.DOText(str, 1).OnComplete(() => nextSign.SetActive(true));

            }
        }
    }
}
