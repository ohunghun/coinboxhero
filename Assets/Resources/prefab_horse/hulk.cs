using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class hulk : Horse
{
    public Rigidbody2D rig;
    // public Transform body;
    castingTimeLimitChecker timelock = new castingTimeLimitChecker(0.3f);
    protected override IEnumerator AutoUpdate()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 2f));
        while (true)
        {


            attack();
            yield return new WaitForSeconds(1f);
            if (Random.Range(0, 15) == 1)
            {
                isRandomMove = true;
                yield return new WaitForSeconds(3);
                isRandomMove = false;
            }

        }
    }
    public override void attack()
    {
        if (!timelock.check())
            return;


        ani.SetTrigger("attack");
        Vector3 dir = box.Instance.transform.position - body.transform.position + new Vector3(0, 1.2f, 0);
        Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up) * Quaternion.Euler(new Vector3(0, 90, 0));
        body.rotation = rotation;
        if (body.rotation.eulerAngles.y < 10 && body.rotation.eulerAngles.y > -10 || body.rotation.eulerAngles.y > 200)
            body.rotation = Quaternion.Euler(new Vector3(0, 0, body.rotation.eulerAngles.z));
        else
            body.rotation = Quaternion.Euler(new Vector3(0, 180, body.rotation.eulerAngles.z));


        rig.AddForce(dir.normalized * 30000);

        float t = 0;
        DOTween.To(() => 0, x => t = x, 100, 0.2f).OnComplete(rotationset);


    }

    void rotationset()
    {
        //rig.velocity = new Vector3(rig.velocity.x * 0.4f, rig.velocity.y * 0.7f, 0);

        initRot();
    }


    private void Update()
    {


    }
    public override void onBoxHit()
    {
        rig.velocity = new Vector3(rig.velocity.x * 0.34f, rig.velocity.y * 0.7f, 0);
        box.Instance.hit(power);
        SoundManager.getInstance().play("hammerSmath");
        bulletManager.Instance.getPrefab("horse4", 0.2f, box.Instance.transform.position);


    }

    public override void attackLong_held()
    {

    }

    public override void attackLong_release()
    {

    }
}
