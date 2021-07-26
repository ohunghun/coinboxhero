using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using mytimer;
public class sunAndmoon : MonoBehaviour
{
    public Vector3 daypos;
    public Vector3 nightpos;
    // Start is called before the first frame update
    void Start()
    {
        Timer.getInstance().AddDayChangeListener(OnDayChanged);
    }
    void OnDayChanged()
    {
        if(Timer.getInstance().getSun())
        {
            transform.DOLocalMove(daypos,1);
         
        }
        else
        {
            transform.DOLocalMove(nightpos, 1);
        }

    }
}
