using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon2 : weapon_oneShot
{
    protected override void attackOneShot()
    {
        GameObject g = bulletManager.Instance.getPrefab("bullet2", 3, FirePoint.position, Quaternion.identity);
        fireball f = g.GetComponent<fireball>();
        f.power = power;

        SoundManager.getInstance().play("fire", 0.01f);
    }
}
