using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackBoxOnAnimation : MonoBehaviour
{
    public float power;
    public Stat stat;
    
   public void attackBox()
    {
        if (Mathf.Abs(transform.position.x - box.Instance.transform.position.x) < 0.8)
        {
            box.Instance.hit(power,transform.position.x/0.8f);
            bulletManager.Instance.getPrefab("hit2", 0.2f,myCollections.randomVector(-0.5f,0.5f)+ (Vector2)box.Instance.transform.position);
        }
    }
}
