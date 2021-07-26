using UnityEngine;
using System.Collections;

public class horse2 : Horse
{
    
    Rigidbody2D rig;
    
    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
  

    }
    public override void onBoxHit()
    {
        box.Instance.hit(getPower(), hitpos.position.x);
    }
    int restCount = 0;
    float randomMoveTarget = 0;
    protected override IEnumerator AutoUpdate()
    {


        float dir = box.Instance.transform.position.x - transform.position.x;


        if (Mathf.Abs(dir) > Random.Range(0f,0.5f) && restCount <= 0)
        {
            if (dir > 0)
                move(100f);
            else if (dir < 0)
                move(-100f);
        }
        else if (restCount > 0)
        {
            dir = randomMoveTarget - transform.position.x;
            if (dir > 0)
                move(100f);
            else if (dir < 0)
                move(-100f);
            if (Mathf.Abs(dir) < 0.4f)
            {
                restCount = 0;
            }
            restCount--;
        }
        else if (restCount <= 0)
        {
            if (Random.Range(0, 10) == 1)
            {
                restCount = Random.Range(300, 400);
                randomMoveTarget = Random.Range(-10, 10);
            }
            attack();
            yield return new WaitForSeconds(1);
        }
        yield return null;

    }
    public Transform hitpos;
    castingTimeLimitChecker timelimit = new castingTimeLimitChecker(0.2f);
    public override void attack()
    {
        if (transform.position.y < -3.3f && timelimit.check())
        {
            ani.SetTrigger("attack");
            SoundManager.getInstance().play("jump");
            rig.AddForce(new Vector2(0, 1300));
           
        }
    }

    public override void attackLong_held()
    {
       
    }

    public override void attackLong_release()
    {
     
    }

  
}
