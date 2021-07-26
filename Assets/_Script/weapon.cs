using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static joystick;
using mTypes;
public abstract class weapon : MonoBehaviour
{

    public bool isStop = false;
    public float power;
    public Transform FirePoint;
    protected bool isAuto = false;
    EventCallback joystickCallback;
    public void initWeapon(bool isAuto)
    {
        this.isAuto = isAuto;
        if (!isAuto)
        {
            joystickCallback = new EventCallback(move, attackOneShot, attackHold, attackRelease);
            joystick.Instance.AddCallback(joystickCallback);
        }
       
        onInited();
    }
    protected abstract void onInited();
    protected abstract void attackOneShot();
    protected abstract void attackHold();
    protected abstract void attackRelease();

    private void move(float t)
    {

    }
    private void OnDestroy()
    {
        if(!isAuto)
        {
            joystick.Instance.removeCallback(joystickCallback);
        }
    }


}
