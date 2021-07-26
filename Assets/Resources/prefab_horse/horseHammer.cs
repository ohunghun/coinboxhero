using UnityEngine;
using System.Collections;

public class horseHammer : Horse
{
  

    castingTimeLimitChecker timeBreak = new castingTimeLimitChecker(0.05f);
    public override void attack()
    {
        if (!timeBreak.check())
            return;
        SoundManager.getInstance().play("hammer");
        forwardBox();
        ani.SetTrigger("attack");
        float length = Mathf.Abs(box.Instance.transform.position.x - transform.position.x);
        if (length < 2.6f)
        {
            Invoke("boxHit", 0.1f);
        }
    }
    public void boxHit()
    {
        box.Instance.hit(power);
        SoundManager.getInstance().play("hammerSmath");
        bulletManager.Instance.getPrefab("horse4", 0.2f, box.Instance.transform.position);

    }

    public override void onBoxHit()
    {
    }



    public override void attackLong_held()
    {

    }

    public override void attackLong_release()
    {

    }

    protected override IEnumerator AutoUpdate()
    {
        float target = box.Instance.transform.position.x + Random.Range(-1.5f, 1.5f);
        float dir = target - transform.position.x;
        while (Mathf.Abs(dir) > 0.3f)
        {
            dir = target - transform.position.x;
            if (dir > 0)
                move(100f);
            else
                move(-100f);
            yield return null;
        }

        attack();
        yield return new WaitForSeconds(1f);
        if (Random.Range(0, 40) == 1)
        {
            isRandomMove = true;
            yield return new WaitForSeconds(3);
            isRandomMove = false;
        }
    }
}
