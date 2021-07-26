using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    public float speed = 15f;
    public float hitOffset = 0f;
    public bool UseFirePointRotation;
    public Vector3 rotationOffset = new Vector3(0, 0, 0);
    
    
    private Rigidbody2D rb;
    public GameObject[] Detached;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject flash = bulletManager.Instance.getPrefab(flashname, 5, transform.position);
        
        
	}

    void FixedUpdate ()
    {
		if (speed != 0)
        {
            //rb.velocity = transform.forward * speed;
            transform.position += transform.forward * (speed * Time.deltaTime);         
        }
	}
    public string flashname= "";
    public string hitname = "";
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Lock all axes movement and rotation
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        

        ContactPoint2D contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point + contact.normal * hitOffset;
        bulletManager.Instance.getPrefab(hitname, 1.5f,pos,rot);
        
        foreach (var detachedPrefab in Detached)
        {
            if (detachedPrefab != null)
            {
                detachedPrefab.transform.parent = null;
            }
        }
        bulletManager.Instance.returnBullet(gameObject);
    }
 
}
