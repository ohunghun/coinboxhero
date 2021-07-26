using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseNeckCut : Horse
{
    castingTimeLimitChecker t = new castingTimeLimitChecker(0.4f);
    public override void attack()
    {
        if (t.check())
        {
            ani.SetTrigger("attack");
            forwardBox();
            neck.attack(box.Instance.transform.position);
            myCountDownTimer.Instance.CountDown(0.15f, () =>
            {
                bulletManager.Instance.getPrefab("blood", 0.2f, (Vector2)box.Instance.transform.position + myCollections.randomVector(-0.4f, 0.4f));
                box.Instance.hit(power,transform.position.x/(ScreenManager.Instance.screenWidthToWorldLength*0.6f));
            });
        }
    }
    protected override IEnumerator AutoUpdate()
    {
        throw new System.NotImplementedException();
    }
    public override void attackLong_held()
    {
       
    }

    public override void attackLong_release()
    {
        
    }
    public Neck neck;
    public override void onBoxHit()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
