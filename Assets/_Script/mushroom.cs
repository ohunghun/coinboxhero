using mTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mushroom : MonoBehaviour
{
   
    public Rect initRect;
    ScreenManager sm;

    [HideInInspector]
    public float posRatio = 0;
    Vector2 endpos;
    float heightpos;
    float speed = 1;
    
    public void init( Vector2 endpos, float heightpos)
    {
        if (sm == null)
            sm = ScreenManager.Instance;
        posRatio = 0;
        this.endpos = new Vector2(Mathf.Clamp(endpos.x * Random.Range(0.4f, 2f), sm.getScreenLeft() + 0.23f, sm.getScreenRight() - 0.23f), endpos.y );
        this.heightpos = heightpos * Random.Range(1f, 1.5f);
        speed = Random.Range(0.8f, 1.2f);
    }

    Rect getRect()
    {
        Rect temp = new Rect(initRect.center - new Vector2(initRect.width, initRect.height) + (Vector2)transform.position, initRect.size);
        return temp;
    }
    private void Start()
    {
        sm = ScreenManager.Instance;
        
    }
    public bool isContain(Rect rect)
    {
        return getRect().Overlaps(rect);
    }
    private void FixedUpdate()
    {
        posRatio += Time.deltaTime * speed;
        posRatio = Mathf.Clamp(posRatio, 0, 1);
        transform.position = MathParabola.Parabola(Vector2.zero, endpos, heightpos, posRatio);
    }
    private void Update()
    {
        coinGather owner = HorseManager.Instance.Own.GetComponent<coinGather>();
        if(isContain(owner.getRect()))
        {
            gameObject.SetActive(false);
            HorseManager.Instance.levelUp();
            
        }
    }
    void OnDrawGizmos()
    {
        // Green
        Gizmos.color = new Color(0.0f, 1.0f, 0.0f);
        myCollections.DrawRect(getRect());
    }
}
