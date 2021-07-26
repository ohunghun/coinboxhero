using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using mTypes;
public class joystick : Singleton<joystick>
{
    public GameObject pivot,stick;

    mtouch moveTouch;
    Vector3 pivot_initpos, stick_initpos;
    float maxLength ;
    List<EventCallback> callbacks = new List<EventCallback>();
   // EventCallback callback = null;
    public Canvas mainCanvas;
    // Start is called before the first frame update
    void Start()
    {

        pivot_initpos = pivot.transform.position;
        stick_initpos = stick.transform.position;
        Rect rect = pivot.GetComponent<RectTransform>().rect;
        maxLength=rect.width* mainCanvas.scaleFactor*0.5f;
    }
    public class EventCallback
    {
        public callbackVoid attack;
        public callbackFloat move;
        public callbackVoid attackLong_held;
        public callbackVoid attackLong_release;
        public EventCallback(callbackFloat callbackMove, callbackVoid callbackAttack,callbackVoid attackLong_held,callbackVoid attackLong_release)
        {
            attack = callbackAttack;
            move = callbackMove;
            this.attackLong_held = attackLong_held;
            this.attackLong_release = attackLong_release;
        }
    }
    public void attack_long_held()
    {
        foreach(EventCallback callback in callbacks)
        callback.attackLong_held();
    }
    public void attack_long_release()
    {
        foreach (EventCallback callback in callbacks)
            callback.attackLong_release();
    }
    public void AddCallback(EventCallback callback)
    {
        callbacks.Add(callback);
    }
    public void removeCallback(EventCallback callback)
    {
        callbacks.Remove(callback);
    }
    void OnTouch()
    {
        if (!ScreenManager.Instance.isLeft(Input.mousePosition) && moveTouch == null)
        {
            attack();
        }
    }
    bool disable = false;
    public void controlDisable()
    {
        disable = true;
    }
    public void controlEnable()
    {
        disable = false;
    }
    private void Update()
    {
        
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)&& !ScreenManager.IsPointerOverUIObject(Input.mousePosition)&&!disable)
        {

            
            if (ScreenManager.Instance.isLeft(Input.mousePosition) && moveTouch == null)
            {
                moveTouch = new mtouch(0);
                moveTouch.pivot = Input.mousePosition;
                moveTouch.lastPos = Input.mousePosition;
            }
            else
                attack();
        }
        else
        {
            if (moveTouch != null)
            {
                if(Input.GetMouseButton(0) )
                {
                    Vector2 direction = (Vector2)Input.mousePosition - moveTouch.pivot;
                    moveTouch.lastPos = Input.mousePosition;
                    float length = direction.magnitude;
                    if (length > maxLength)
                    {
                        moveTouch.pivot = Vector2.Lerp(moveTouch.pivot, Input.mousePosition, 1 - (maxLength / length));
                    }
                    direction = (Vector2)Input.mousePosition - moveTouch.pivot;
                    direction=direction / maxLength;
                    move(direction);
                }
                else if(Input.GetMouseButtonUp(0) )
                {
                    moveTouch = null;
                }
            }
           
        }
#else
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            int id = touch.fingerId;
            if (touch.phase == TouchPhase.Began&& !ScreenManager.IsPointerOverUIObject(Input.mousePosition)&&!disable)
            {

                if (ScreenManager.Instance.isLeft(touch.position) && moveTouch == null)
                {
                    moveTouch = new mtouch(touch.fingerId);
                    moveTouch.pivot = touch.position;
                    moveTouch.lastPos = touch.position;
                }
                else
                    attack();
            }
            else
            {
                if (moveTouch != null)
                {
                    if ((touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) && moveTouch.id == id)
                    {
                        Vector2 direction = touch.position - moveTouch.pivot;
                        moveTouch.lastPos = touch.position;
                        float length = direction.magnitude;
                        if (length > maxLength)
                        {
                            moveTouch.pivot = Vector2.Lerp(moveTouch.pivot, touch.position, 1 - (maxLength / length));
                        }
                        direction = touch.position - moveTouch.pivot;
                        move(direction.x/ maxLength);
                    }
                    else if (touch.phase == TouchPhase.Ended && moveTouch.id == id)
                    {
                        moveTouch = null;
                    }
                }
            }

        }
#endif
        drawjoystick();
    }
    void drawjoystick()
    {
        if (moveTouch != null)
        {
            pivot.transform.position = moveTouch.pivot;
            stick.transform.position = moveTouch.lastPos;
        }
        else
        {
            pivot.transform.position = pivot_initpos;
            stick.transform.position = stick_initpos;
        }
    }
   
    protected virtual void move(Vector2 direction)
    {

        foreach (EventCallback callback in callbacks)
            callback.move(direction.x);
    }
    public void attack()
    {
        foreach (EventCallback callback in callbacks)
            callback.attack();
    }
    class mtouch
    {
        public int id;
        public Vector2 pivot;
        //public static float maxLength = -1;
        public Vector2 lastPos;
        public mtouch(int id)
        {
          //  if (maxLength == -1)
            //    maxLength = ScreenManager.Instance.getThreshold() * 5f;
            this.id = id;
        }

    }
}
