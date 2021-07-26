using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainHorseControl : Horse
{
    Rigidbody2D rig;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
       

    }
    float dir = -1;
    protected override IEnumerator AutoUpdate()
    {


        dir = dir * -1;
        while (Mathf.Abs(dir) > 0.3f)
        {
            dir = box.Instance.transform.position.x - transform.position.x;
            if (dir > 0)
                move(100f);
            else
                move(-100f);
            yield return null;
        }


        yield return new WaitForSeconds(1.1f);
        if (Random.Range(0, 2) == 1)
        {
            isRandomMove = true;
            yield return new WaitForSeconds(3);
            isRandomMove = false;
        }


    }
   
    castingTimeLimitChecker timelimit = new castingTimeLimitChecker(0.5f);
    public override void attack()
    {
        if (transform.position.y < -3.7f && timelimit.check())
        {
            ani.SetTrigger("attack");
            SoundManager.getInstance().play("jump");
            rig.AddForce(new Vector2(0, 1300));
        }
    }

    public override void onBoxHit()
    {
        box.Instance.hit(power);
    }

    public override void attackLong_held()
    {

    }

    public override void attackLong_release()
    {

    }
}
