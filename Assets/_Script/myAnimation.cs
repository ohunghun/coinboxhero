using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mTypes;
public abstract class myAnimation : MonoBehaviour
{
    public string id;
    callbackVoid callbackEnd;

    abstract public void StartAnimation(callbackVoid callback);
}
