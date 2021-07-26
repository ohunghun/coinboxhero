using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mRotate : MonoBehaviour {
    public Vector3 amount;
    public bool israndomStart = false;
    public bool isStop = false;
	// Use this for initialization
	
    private void OnEnable()
    {
        StartCoroutine("rot");
    }
    IEnumerator rot()
    {
        if (israndomStart)
            yield return new WaitForSeconds(Random.Range(0, 2.4f));
        while(true)
        {
            if(!isStop)
                transform.Rotate(Time.deltaTime * amount);
            yield return null;
        }
    }
}
