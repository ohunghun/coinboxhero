using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class UihpText : MonoBehaviour
{
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    public void Shoot(string str,Vector2 startpos)
    {
        DOTween.Complete(transform);
        DOTween.Complete(text);
        text.text = str;
        transform.position = startpos;
        
            
        Sequence sq = DOTween.Sequence();
        sq.Append(text.DOFade(1, 0.01f));
        sq.Append(transform.DOMove(startpos +new Vector2(0,100), 0.4f));
        sq.AppendInterval(0.2f);
        sq.Append(text.DOFade(0, 0.2f));
        sq.Play();
    }
}
