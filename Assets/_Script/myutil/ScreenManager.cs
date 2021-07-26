using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using mTypes;
public class ScreenManager : Singleton<ScreenManager>
{
    static public Rect bound;
    // Start is called before the first frame update
    public float screenWidthToWorldLength = 100;
    public float screenHeightWorldLength = 100;
    public float screenLeftToWorldX = 10;
    public float screenRightToWorldX = 10;
    public float screenTopToworld = 10;
    public float screenBotToWorld = 10;
    List<callbackVoid> list_onCameraSizeChanged = new List<callbackVoid>();
    
    public void init()
    {
       
        int defaultValue = EventSystem.current.pixelDragThreshold;
        EventSystem.current.pixelDragThreshold =
                Mathf.Max(
                     defaultValue,
                     (int)(defaultValue * Screen.dpi / 210f));
        setCameraSize(5);
        isinit = true;
    }
    bool isinit = false;
    public void addOnCameraSizeChanged(callbackVoid callback)
    {
        list_onCameraSizeChanged.Add(callback);
        if (isinit)
            callback();
    }
    public void setCameraSize(float size)
    {
      
        Camera.main.orthographicSize = size;
        screenRightToWorldX = Camera.main.ScreenToWorldPoint(
            new Vector2(Screen.width, 0)).x;
        screenLeftToWorldX = Camera.main.ScreenToWorldPoint(
            new Vector2(0, 0)).x;
        screenTopToworld = Camera.main.ScreenToWorldPoint(
            new Vector2(0, Screen.height)).y;
        screenBotToWorld = Camera.main.ScreenToWorldPoint(
            new Vector2(0, 0)).y;
        screenWidthToWorldLength = screenRightToWorldX - screenLeftToWorldX;
        screenHeightWorldLength = screenTopToworld - screenBotToWorld;
        foreach (callbackVoid callback in list_onCameraSizeChanged)
            callback();
    }
    public float getThreshold()
    {
        return EventSystem.current.pixelDragThreshold;
    }
    static public bool IsPointerOverUIObject(Vector2 position)
    {

        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(position.x, position.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
      
        int t = results.Count;
        
        return t > 0;
    }
    public float threthhold = 1;
    public bool containScreen(Vector2 pos)
    {
        return bound.Contains(pos);
    }
    public float getScreenLeft()
    {
        return 0;
    }
    public float getScreenRight()
    {
        return Screen.width;
    }
  
 
    public bool isLeft(Vector2 screenpos)
    {
        
      
        if (screenpos.x < Screen.width*0.5f)
            return true;
        return false;
    }
    
    
}
