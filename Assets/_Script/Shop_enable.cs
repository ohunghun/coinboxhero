using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Shop_enable : MonoBehaviour
{
    
    Vector3 initScale;
    readonly Vector3 popScale = new Vector3(1, 1, 1);
    
    // Update is called once per frame
    private void Start()
    {
        initScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }
    bool isHorseIn = false;
    void Update()
    {
        float distance=Mathf.Abs(HorseManager.Instance.Own.transform.position.x - transform.position.x);
        if (distance<2&&!isHorseIn)
        {
            transform.DOScale(popScale, 0.2f);
            isHorseIn = true;
        }else if(isHorseIn&&distance >2)
        {
            isHorseIn = false;
            transform.DOScale(Vector3.zero, 0.2f);
        }

        
    }
}
