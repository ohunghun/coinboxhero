using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class myCollections 
{

    public static GameObject findgameObj(GameObject[] list,string name)
    {
        for(int i=0;i<list.Length;i++)
        {
            GameObject obj = list[i];
            if (obj.name == name)
                return obj;
        }
        return null;
    }
    static public T string2Type<T>(string str)
    {
        return (T)Enum.Parse(typeof(T), str);
    }
    static public int getEnumCount<T>()
    {
        return  Enum.GetNames(typeof(T)).Length;

    }
    static public void setActive(GameObject[] list,bool active)
    {
        foreach (GameObject obj in list)
            obj.SetActive(active);

    }
    static public void DrawLine(Vector3 a, Vector3 b)
    {
        Gizmos.DrawLine(a, b);
    }
    static public void DrawRect(Rect rect)
    {
        Gizmos.DrawWireCube(new Vector3(rect.center.x, rect.center.y, 0.01f), new Vector3(rect.size.x, rect.size.y, 0.01f));
    }
    public static Vector2 randomVector(float min, float max)
    {
        return new Vector2(UnityEngine.Random.Range(min, max), UnityEngine.Random.Range(min, max));
    }
    public static bool IsPointerOverUIObject()
    {
        Debug.Log("sfsf");
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
           
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
      
        return results.Count > 0;
    }
}
