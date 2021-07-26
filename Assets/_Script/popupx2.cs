using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class popupx2 : MonoBehaviour
{
    public void shoot(Vector3 initpos)
    {
        transform.position = initpos;
        DOTween.Complete(transform);
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOScale(0.15f, 0.1f));
        mySequence.AppendInterval(0.3f);
        mySequence.Append(transform.DOScale(0, 0.1f));
    }
}
