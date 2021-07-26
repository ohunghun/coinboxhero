using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class spriteHPBAR : MonoBehaviour
{
    box b;
    private void Start()
    {
        b = box.Instance;
        
    }
    
    void Update()
    {
        float boxhpratio = b.getCurrentHp() / (float)b.getMAXHP();
        transform.localScale = new Vector3(1, boxhpratio, 1);
    }
}
