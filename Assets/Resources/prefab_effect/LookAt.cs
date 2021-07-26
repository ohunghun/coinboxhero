using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 boxpos = box.Instance.transform.position;
        Vector3 targetpos = new Vector3(boxpos.x, boxpos.y, transform.position.z);
        transform.LookAt(targetpos);
    }
}
