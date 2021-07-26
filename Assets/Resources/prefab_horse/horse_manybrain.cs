using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horse_manybrain : Horse
{
    public override void attack()
    {

    }
    protected override IEnumerator AutoUpdate()
    {
        throw new System.NotImplementedException();
    }
    private void Update()
    {
        lazer[0].transform.LookAt(b.transform);
        lazer[1].transform.LookAt(b.transform);
        lazer[2].transform.LookAt(b.transform);
    }
    private void Start()
    {
        //    head = transform.findComponentOnChild<headdir>();
       
        Transform t = lazer[0].transform.parent;
        lazer[0].transform.parent = null;
        lazer[0].transform.localScale = new Vector3(1, 1, 1);
        lazer[0].transform.parent = t;
        lazer[0].transform.position = new Vector3(lazer[0].transform.position.x, lazer[0].transform.position.y, box.Instance.transform.position.z);
         t = lazer[1].transform.parent;
        lazer[1].transform.parent = null;
        lazer[1].transform.localScale = new Vector3(1, 1, 1);
        lazer[1].transform.parent = t;
        lazer[1].transform.position = new Vector3(lazer[1].transform.position.x, lazer[1].transform.position.y, box.Instance.transform.position.z);
        t = lazer[2].transform.parent;
        lazer[2].transform.parent = null;
        lazer[2].transform.localScale = new Vector3(1, 1, 1);
        lazer[2].transform.parent = t;
        lazer[2].transform.position = new Vector3(lazer[2].transform.position.x, lazer[2].transform.position.y, box.Instance.transform.position.z);

        b = box.Instance;
    }
    public GameObject []lazer;

    //headdir head;
    public override void attackLong_held()
    {

        forwardBox();
        ani.SetBool("attack", true);


        box.Instance.hit((int)(power * Time.deltaTime) + 1);
        
        //lazer[(int)Time.realtimeSinceStartup % 3].GetComponent<LaserController2D>().setTarget(box.Instance.transform.position);
        lazer[(int)Time.realtimeSinceStartup % 3].SetActive(true);
      //  lazer[((int)Time.realtimeSinceStartup+1) % 3].GetComponent<LaserController2D>().setTarget(box.Instance.transform.position);
        lazer[((int)Time.realtimeSinceStartup+1) % 3].SetActive(true);
        lazer[((int)Time.realtimeSinceStartup + 2) % 3].SetActive(false);
        //  head.setDirTo();
       
    }
    box b;
    public override void attackLong_release()
    {
        //head.setInit();
        ani.SetBool("attack", false);
        lazer[0].SetActive(false);
        lazer[1].SetActive(false);
        lazer[2].SetActive(false);
    }

    public override void onBoxHit()
    {

    }
}
