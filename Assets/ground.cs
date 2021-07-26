using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ground : Singleton<ground>
{
    public float y;
    public bool isUnderGround(float y)
    {
        return this.y > y;
            
    }
    

    void OnDrawGizmos()
    {
        // Green
        Gizmos.color = Color.yellow;

        myCollections.DrawLine(new Vector3(-1000, y, 0), new Vector3(1000, y, 0));
    }
}
