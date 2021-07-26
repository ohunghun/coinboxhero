using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flog : MonoBehaviour
{
    public Neck tongue;
    private void Start()
    {
        StartCoroutine("routain");
    }
    IEnumerator routain()
    {
        while(true)
        {
            attack();
            yield return new WaitForSeconds(2);
                
        }
    }
    void attack()
    {
        Vector3 dir=HorseManager.Instance.Own.transform.position - tongue.pivotPos.position;

        tongue.attack(HorseManager.Instance.Own.transform.position+dir);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("aaa");
    }
}
