using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horseSlime : Horse
{
    private void Start()
    {
        transform.findComponentOnChild<attackBoxOnAnimation>().power = power;
        transform.findComponentOnChild<attackBoxOnAnimation>().stat = stat;
    }
    castingTimeLimitChecker timelimit = new castingTimeLimitChecker(0.5f);
    protected override IEnumerator AutoUpdate()
    {
        return null;
    }
    public override void attack()
    {
        if ( timelimit.check())
        {
            
            ani.SetTrigger("attack");
            SoundManager.getInstance().play("jump");
          
        }
    }

    private void Update()
    {

    }
    

    public override void attackLong_held()
    {

     

    }

    public override void attackLong_release()
    {
     
    }

    public override void onBoxHit()
    {
       
    }
}
