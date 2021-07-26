using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horse_nudeskin : Horse
{
    
    public GameObject FirePoint;
    headdir head;
    private void Start()
    {
      
        head = transform.findComponentOnChild<headdir>();
    }
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
        head.setDirTo();
        Vector2 dir = box.Instance.transform.position + new Vector3(0, Random.Range(-0.2f, 0.2f)) - FirePoint.transform.position;
        Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
        GameObject g = bulletManager.Instance.getPrefab("poison", 3, FirePoint.transform.position, rotation);

        fireball f = g.GetComponent<fireball>();
        f.power = getPower();


        SoundManager.getInstance().play("fire", 0.1f);
        ani.SetTrigger("attack");
    }
    

    public override void attackLong_held()
    {
        
    }
    castingTimeLimitChecker time = new castingTimeLimitChecker(0.14f);
    public override void attackLong_release()
    {
        head.setInit();
    }
}
