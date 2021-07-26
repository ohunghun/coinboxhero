using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class tweenColor : MonoBehaviour
{
    SpriteRenderer sp;
    public Color to;
    public float duration = 1;
    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        
    }
    object id;
    private void OnEnable()
    {
        id = sp.DOColor(to, duration).SetLoops(-1, LoopType.Yoyo).id;
    }
    private void OnDisable()
    {
        DOTween.Complete(id);
    }


}
