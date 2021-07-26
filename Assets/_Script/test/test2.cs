using UnityEngine;
using System.Collections;

public abstract class test2 : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Debug.Log("start");
        StartCoroutine("startco");
    }

    IEnumerator startco()
    {
        while (true)
        {
            yield return a();
        }
    }
    protected abstract IEnumerator a();
}
