using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixRotation : MonoBehaviour
{
    Quaternion init;
    private void Start()
    {
        init = transform.rotation;
    }
    // Update is called once per frame
    void Update()
    {
        transform.rotation = init;
    }
}
