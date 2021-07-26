using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horse0 : Horse
{
    Rigidbody2D rig;
    //public Transform BoxHitCenter;
    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();

    }
    protected override IEnumerator AutoUpdate()
    {

        float dir = box.Instance.transform.position.x - transform.position.x;
        while (Mathf.Abs(dir) > Random.Range(0f, 0.5f)) 
        {
            dir = box.Instance.transform.position.x - transform.position.x;
            if (dir > 0)
                move(100f);
            else
                move(-100f);
            yield return null;
        }

        attack();
        yield return new WaitForSeconds(1.1f);
        if (Random.Range(0, 10) == 1)
        {
            isRandomMove = true;
            yield return new WaitForSeconds(3);
            isRandomMove = false;
        }


    }
    public Transform hitpos;
    
    castingTimeLimitChecker timelimit = new castingTimeLimitChecker(0.5f);
    public override void attack()
    {
        if (isground() && timelimit.check())
        {
            ani.SetTrigger("attack");
            SoundManager.getInstance().play("jump");
            rig.AddForce(new Vector2(0, 1300));
           
        }
    }

    public override void onBoxHit()
    {
        box.Instance.hit(getPower(), hitpos.position.x);
    }

    public override void attackLong_held()
    {

    }

    public override void attackLong_release()
    {

    }

   
}
