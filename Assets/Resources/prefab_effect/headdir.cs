using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headdir : MonoBehaviour
{
    public bool test = false;
    public Vector3 t = new Vector3(0, 0, 90);
    private void Awake()
    {
        init = transform.rotation;
        
    }
    public void setInit()
    {
        if (transform.rotation.eulerAngles.y < 10 && transform.rotation.eulerAngles.y > -10 || transform.rotation.eulerAngles.y > 200)
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, init.eulerAngles.z));
        else
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, init.eulerAngles.z));
    }
    Quaternion init;
    public void setDirTo()
    {

        Vector3 dir = box.Instance.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up) * Quaternion.Euler(new Vector3(0, 90, 0));

        transform.rotation = rotation;
        if (transform.rotation.eulerAngles.y < 10 && transform.rotation.eulerAngles.y > -10 || transform.rotation.eulerAngles.y > 200)
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z));
        else
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, transform.rotation.eulerAngles.z));
        transform.Rotate(new Vector3(0, 0, offset));
    }
    public float offset;
    private void Update()
    {
        if(test)
        {
            setDirTo();
        }
    }
}
