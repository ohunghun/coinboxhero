using UnityEngine;
using System.Collections;

public class weapon1 : weapon_oneShot
{
    
  
    protected override void attackOneShot()
    {
        Debug.Log("shot!!");
        GameObject g = bulletManager.Instance.getPrefab("bullet1", 3, FirePoint.position, Quaternion.identity);
        fireball f = g.GetComponent<fireball>();
        f.power = power;

        SoundManager.getInstance().play("fire", 0.01f);
    }

   
}
