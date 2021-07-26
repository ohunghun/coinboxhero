using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseManyLeg : Horse
{
 
    public override void attack()
    {
       
    }

    private void Update()
    {
        lazer.transform.LookAt(b.transform);
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
    public Transform head;
    public override void attackLong_held()
    {

        forwardBox();
        ani.SetBool("attack", true);

        Vector3 dir = box.Instance.transform.position - head.transform.position;
        Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up) * Quaternion.Euler(new Vector3(0, 90, 0));

        head.transform.rotation = rotation;
        if (head.rotation.eulerAngles.y < 10 && head.rotation.eulerAngles.y > -10 || head.rotation.eulerAngles.y > 200)
            head.rotation = Quaternion.Euler(new Vector3(0, 0, head.rotation.eulerAngles.z));
        else
            head.rotation = Quaternion.Euler(new Vector3(0, 180, head.rotation.eulerAngles.z));
        box.Instance.hit((int)(power * Time.deltaTime) + 1, gethitpos());
        // lazer.GetComponent<LaserController2D>().setTarget(box.Instance.transform.position);
        lazer.SetActive(true);

    }
    public float gethitpos()
    {
        float hitpos = transform.position.x / (ScreenManager.Instance.screenWidthToWorldLength * 0.6f);
        hitpos = hitpos + Random.Range(-0.2f, 0.2f);
        return hitpos;
    }
    public override void attackLong_release()
    {
        ani.SetBool("attack", false);
       lazer.SetActive(false);
    }

    public override void onBoxHit()
    {
       
    }

    protected override IEnumerator AutoUpdate()
    {
        throw new System.NotImplementedException();
    }
}
