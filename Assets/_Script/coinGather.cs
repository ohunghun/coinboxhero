using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class coinGather : MonoBehaviour
{
    coinGun coingun;
   
    public Rect initRect;
    private void Start()
    {
        coingun = coinGun.Instance;   
    }
    

    private void Update()
    {

        LinkedList<coin> coinOnField = coingun.list_coinOnField;

        if (coinOnField != null)
        {
            var node = coinOnField.First;
            while (node != null)
            {
                var next = node.Next;
                coin c = node.Value;
                if (c.isContain(getRect()))
                {
                 
                    coingun.returnCoin(c);
                    coinOnField.Remove(node);
                    
                }
                node = next;
            }
        }
     
    }
    public Rect getRect()
    {
        Rect temp = new Rect(initRect.center-new Vector2(initRect.width, initRect.height) + (Vector2)transform.position, initRect.size);
        return temp;
    }
    
    void OnDrawGizmos()
    {
        // Green
        Gizmos.color = Color.blue;
        myCollections.DrawRect(getRect());
    }
}
