using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class evolutionEffect : Singleton<evolutionEffect>
{
    SpriteRenderer sp;
    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        
    }
    public void Effect(float sec,Vector3 position)
    {
        DOTween.Complete(sp);
        DOTween.Complete(transform);
        sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1);
        transform.localScale = Vector3.zero;
        sp.DOFade(0, sec+1).SetDelay(sec).OnComplete(() => { transform.position = new Vector3(100, 100, 100); });
        transform.DOScale(100, sec);
        transform.position = position;
    }
    
}
