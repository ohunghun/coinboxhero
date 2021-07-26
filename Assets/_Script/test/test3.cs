using UnityEngine;
using System.Collections;

public class test3 : test2
{
    protected override IEnumerator a()
    {
        yield return new WaitForSeconds(1);

    }

 
}
