using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_fallowWorldObject : MonoBehaviour
{
    public Transform target;
    public bool fixX = false;
    public bool fixY = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 screenpoint = Camera.main.WorldToScreenPoint(target.position);
        float x = screenpoint.x;
        float y= screenpoint.y;
        if (fixX)
            x = transform.position.x;
        if (fixY)
            y = transform.position.y;
        transform.position = new Vector3(x, y);
    }
}
