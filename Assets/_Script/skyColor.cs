using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mytimer;
using DG.Tweening;
public class skyColor : MonoBehaviour
{
    public SpriteRenderer sp;
    public Color colorDay;
    Color night;
    void Start()
    {
        night = colorDay * 0.3f;
        night.a = 1;
        Timer.getInstance().AddDayChangeListener(OnDayChanged);
       
    }
    void OnDayChanged()
    {
        if (Timer.getInstance().getSun())
        {
            sp.DOColor(colorDay, 1);
            
        }
        else
        {
          
            sp.DOColor(night, 1);
        }

    }
}
