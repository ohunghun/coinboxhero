using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horseMonsterEyes : Horse
{
    public Neck front, side;
    castingTimeLimitChecker t = new castingTimeLimitChecker(0.4f);
    public override void attack()
    {
        if (t.check())
        {
            ani.SetTrigger("attack");
            forwardBox();
            front.attack(box.Instance.transform.position);
            myCountDownTimer.Instance.CountDown(0.15f, () =>
            {
                bulletManager.Instance.getPrefab("blood", 0.2f, (Vector2)box.Instance.transform.position + myCollections.randomVector(-0.4f, 0.4f));
                box.Instance.hit(power, transform.position.x / (ScreenManager.Instance.screenWidthToWorldLength * 0.6f));
            });

            myCountDownTimer.Instance.CountDown(0.25f, () =>
            {
                side.attack(box.Instance.transform.position);

                myCountDownTimer.Instance.CountDown(0.15f, () =>
                {
                    bulletManager.Instance.getPrefab("blood", 0.2f, (Vector2)box.Instance.transform.position + myCollections.randomVector(-0.4f, 0.4f));
                    box.Instance.hit(power,  transform.position.x / (ScreenManager.Instance.screenWidthToWorldLength * 0.6f));
                });
            });
        }
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

    protected override IEnumerator AutoUpdate()
    {
        throw new System.NotImplementedException();
    }
}
