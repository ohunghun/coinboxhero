using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Neck : MonoBehaviour
{
    
    public Transform pivotPos;
    public Transform head;
    
    
    public void attack(Vector3 target)
    {
        DOTween.Complete(head);
        Vector3 dir = target - pivotPos.transform.position;
        
        Vector3[] way = new Vector3[2];
        way[0] = target+ dir.normalized * 2 ;
        way[1] = pivotPos.position;

        head.DOPath(way, 0.6f);

    }
}
