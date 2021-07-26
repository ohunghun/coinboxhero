using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.Rendering;
public abstract class stick : MonoBehaviour
{
    protected float power;
    public void setPower(float power)
    {
        this.power = power;
    }
    protected virtual void Start()
    {
        if (transform.position.y > -4.3)
            GetComponent<SortingGroup>().sortingLayerName = "stick1";
        else
            GetComponent<SortingGroup>().sortingLayerName = "stick2";
        GetComponent<SortingGroup>().sortingOrder = 1000 - (int)(transform.position.y * 100);
    }
  
}
