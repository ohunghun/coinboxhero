using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hulkwing : Horse
{
    public Transform head;

    public Transform target;
    private void Start()
    {
       

    }
    protected override IEnumerator AutoUpdate()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 2f));
        while (true)
        {
            float total = 0;
            while (true)
            {
                attackLong_held();
                total += Time.deltaTime;
                yield return null;
                if (total > 5)
                    break;
            }
            attackLong_release();
            if (Random.Range(0, 3) == 1)
            {
                isRandomMove = true;
                yield return new WaitForSeconds(3);
                isRandomMove = false;
            }

        }
    }

    public override void attack()
    {
      
    }

    public override void onBoxHit()
    {
        throw new System.NotImplementedException();
    }

    public GameObject lazer;
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
        box.Instance.hit((int)(power * Time.deltaTime)+1);

        lazer.SetActive(true);
       
    }
    public override void attackLong_release()
    {
        Debug.Log("aa");
        ani.SetBool("attack", false);
        lazer.SetActive(false);
      
    }
}
