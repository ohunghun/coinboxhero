using UnityEngine;
using System.Collections;

public class castingTimeLimitChecker 
{
    float limit;
    public castingTimeLimitChecker(float limit)
    {
        this.limit = limit;
    }
    float lastCheckTIme = 0;
    public bool check()
    {
        float span = Time.timeSinceLevelLoad - lastCheckTIme;
        if (span < limit)
            return false;
        else
        {
            lastCheckTIme = Time.timeSinceLevelLoad;
            return true;
        }

    }
    
}
