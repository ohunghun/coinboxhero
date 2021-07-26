using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UI;
static public class extendtionFundtions 
{


    public static void LookAt2d(this Transform transform, Vector3 target)
    {

        Vector3 dir = target - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public static Transform findChildByName(this Transform transform,string name)
    {

        if (transform.name==name)
        {
            return transform;
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform t=findChildByName(transform.GetChild(i), name);
                if (t != null)
                    return t;
            }
        }
        return default;
    }
    public static T findComponentOnParents<T>(this Transform transform)
    {
        Transform parent = transform.parent;
        while(parent!=null)
        {
            if (parent.GetComponent<T>() != null)
                return parent.GetComponent<T>();
            parent = parent.parent;
        }
        return default(T);
    }
    public static T findComponentOnChild<T>(this Transform transform)
    {
        
        if(transform.GetComponent<T>()!=null)
        {
            
            return transform.GetComponent<T>();
        }
        else
        {
            Debug.Log(transform.gameObject.name);

            for (int i = 0; i < transform.childCount; i++)
            {
                T t=findComponentOnChild<T>(transform.GetChild(i));
                if (t != null )
                    return t;
            }
            
        }
        return default(T);
    }
    public static Transform removeAllChild(this Transform transform)
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        return transform;
    }

    public static void ScrollmoveToItem(this ScrollRect instance, RectTransform child)
    {

        float normalizePosition = instance.GetComponent<RectTransform>().anchorMin.y - child.anchoredPosition.y;
        normalizePosition += (float)child.transform.GetSiblingIndex() / (float)instance.content.transform.childCount;
        normalizePosition /= 1000f;
        normalizePosition = Mathf.Clamp01(1 - normalizePosition);
        instance.verticalNormalizedPosition = normalizePosition;
    }
}