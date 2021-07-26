using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horse_pyramid : Horse
{
    protected override IEnumerator AutoUpdate()
    {
        throw new System.NotImplementedException();

    }
    public override void attack()
    {

    }

    private void Update()
    {
        lazer.transform.LookAt(b.transform);
    }
    public float gethitpos()
    {
        float hitpos = transform.position.x / (ScreenManager.Instance.screenWidthToWorldLength * 0.4f);
        hitpos = hitpos + Random.Range(-0.2f, 0.2f);
        return hitpos;
    }
    private void Start()
    {
        Transform t = lazer.transform.parent;
        lazer.transform.parent = null;
        lazer.transform.localScale = new Vector3(1, 1, 1);
        lazer.transform.parent = t;
        lazer.transform.position = new Vector3(lazer.transform.position.x, lazer.transform.position.y, box.Instance.transform.position.z);

        b = box.Instance;

    }
    box b;
    public GameObject lazer;

    
    public override void attackLong_held()
    {

       //forwardBox();
        ani.SetBool("attack", true);


        box.Instance.hit((int)(power * Time.deltaTime) + 1, gethitpos());
   //     lazer.GetComponent<LaserController2D>().setTarget(box.Instance.transform.position);
        lazer.SetActive(true);
    

    }

    public override void attackLong_release()
    {
    
        ani.SetBool("attack", false);
        lazer.SetActive(false);
    }

    public override void onBoxHit()
    {

    }
}
