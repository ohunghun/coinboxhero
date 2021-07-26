using UnityEngine;
using System.Collections;

public class horse3 : Horse
{
    GameObject prefabFire;
    public GameObject FirePoint;

    protected override IEnumerator AutoUpdate()
    {
        attack();
        yield return new WaitForSeconds(0.5f);
        if (Random.Range(0, 35) == 1)
        {
            isRandomMove = true;
            yield return new WaitForSeconds(3);
            isRandomMove = false;
        }
    }
    
    public override void onBoxHit()
    {
       // box.Instance.hit(power, stat.GoodCoin);
    }
    public override void attack()
    {
        forwardBox();
        Vector2 dir = box.Instance.transform.position + new Vector3(0, Random.Range(-0.2f, 0.2f)) - FirePoint.transform.position;
        Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
        GameObject g= bulletManager.Instance.getPrefab("horse3",3, FirePoint.transform.position,rotation);
    
        fireball f = g.GetComponent<fireball>();
        f.power = getPower();
     
        
        SoundManager.getInstance().play("fire",0.1f);
        ani.SetTrigger("attack");
    }

    public override void attackLong_held()
    {
      
    }

    public override void attackLong_release()
    {
        
    }

  
}
