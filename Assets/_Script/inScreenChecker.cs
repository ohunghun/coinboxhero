using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inScreenChecker : MonoBehaviour
{
    float left=-10;
    float right=10;
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine("init");
        
    }
    IEnumerator init()
    {
        yield return null;
        left = ScreenManager.Instance.screenLeftToWorldX;
        right = ScreenManager.Instance.screenRightToWorldX;

    }
    // Update is called once per frame
    void Update()
    {

        if (transform.position.x < left)
        {
            
            transform.position = new Vector3(left, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > right)
            transform.position = new Vector3(right, transform.position.y, transform.position.z);
    }
}
