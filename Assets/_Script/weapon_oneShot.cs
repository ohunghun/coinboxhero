using UnityEngine;
using System.Collections;

public abstract class weapon_oneShot : weapon
{
    public float interval=2;
    protected override void onInited()
    {
      
        if (isAuto)
        {
            StartCoroutine("_attack");
           
        }

    }
    IEnumerator _attack()
    {
        while(true)
        {
            attackOneShot();
            yield return new WaitForSeconds(interval);
        }
    }
    protected override abstract void attackOneShot();

    protected override void attackHold()
    {

    }
    protected override void attackRelease()
    {
       
    }

    

   
}
