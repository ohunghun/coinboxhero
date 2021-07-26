using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class stickbullet : MonoBehaviour
{

    box b=null;
    Vector3 dir;
    float power;
    bulletManager bm;
    public float speed = 30;
    SortingGroup sortgroup;
    
    
    public virtual void init(float power, Vector3 initpos,SortingGroup sortgroup)
    {
        if(b==null)
        {
            b = box.Instance;
            bm = bulletManager.Instance;
            this.sortgroup = GetComponent<SortingGroup>();
        }
        this.power = power;
        transform.LookAt2d(b.transform.position);
        dir = b.transform.position - transform.position;
        dir = dir.normalized;
        this.sortgroup.sortingLayerID = sortgroup.sortingLayerID;
        this.sortgroup.sortingOrder = sortgroup.sortingOrder;
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        transform.Translate(dir * Time.deltaTime* speed, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bm.returnBullet(gameObject);
        box.Instance.hit(power);   
    }
}
