using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battle_box : MonoBehaviour
{
    public float hp;
    public void hit(float power)
    {
        hp -= power;
        if (hp <= 0)
            battle_judgement.Instance.victory();

    }
}
 