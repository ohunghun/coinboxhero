using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletManager : Singleton<bulletManager>
{
    Vector2 hidePos = new Vector2(1000, 1000);
    Dictionary<string, Queue<GameObject>> cache_effect = new Dictionary<string, Queue<GameObject>>();

    public GameObject getPrefab(string key,float returnTime,Vector2 position)
    {
        return getPrefab(key, returnTime, position, Quaternion.identity);
    }
    public GameObject getPrefab(string key, float returnTime, Vector2 position, Quaternion rotation)
    {
        if (!cache_effect.ContainsKey(key)||cache_effect[key].Count==0)
            fillCache(key);
        
        GameObject obj = cache_effect[key].Dequeue();
        
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);
        myCountDownTimer.countTImer t= myCountDownTimer.Instance.CountDown(returnTime, () => returnBullet(obj));
        callbackTImer.Add(obj.GetInstanceID(), t);
        return obj;
    }
    
    Dictionary<int, myCountDownTimer.countTImer> callbackTImer = new Dictionary<int, myCountDownTimer.countTImer>();
    public void returnBullet(GameObject obj)
    {
        Queue<GameObject> q = cache_effect[obj.name];
        obj.SetActive(false);
        obj.transform.position = new Vector3(10000, 1000, 10000);
        if (!q.Contains(obj))
        {
            waitReturn.Add(obj);
        
        }
    }
    private void FixedUpdate()
    {
        foreach (GameObject obj in waitReturn)
        {
            Queue<GameObject> q = cache_effect[obj.name];
            if (!q.Contains(obj))
            {
        
                obj.SetActive(false);
                q.Enqueue(obj);
                obj.transform.position = hidePos;
                myCountDownTimer.Instance.RemoveCountDown(callbackTImer[obj.GetInstanceID()]);
                callbackTImer.Remove(obj.GetInstanceID());
            }
        }
        waitReturn.Clear();
    }
    List<GameObject> waitReturn = new List<GameObject>();
    void fillCache(string key)
    {


        GameObject prefab = Resources.Load<GameObject>("prefab_effect/"+key);
        Queue<GameObject> array;
        if (!cache_effect.ContainsKey(key))
        {
            array = new Queue<GameObject>();
            cache_effect.Add(key, array);
        }
        else
            array = cache_effect[key];
        for (int i=0;i<4;i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.name = key;
            array.Enqueue(obj);
            prefab.transform.position = hidePos;
            prefab.SetActive(false);
        }
        
    }
}
