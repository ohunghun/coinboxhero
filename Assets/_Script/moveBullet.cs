using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBullet : fireball
{
    public float speed=2;
    private void Update()
    {
        Vector3 dir = box.Instance.transform.position - transform.position;

        transform.Translate(dir.normalized * speed*Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        box.Instance.hit(power);
        bulletManager.Instance.returnBullet(gameObject);
    }
}
