using mTypes;
using UnityEngine;
using UnityEditor;

public class coin : MonoBehaviour
{
    public Sprite[] sprites_coin;
    public SpriteRenderer spritefront;
    coinGun parent;
    float rotSpeed = 200;
    public Rect initRect;
    ScreenManager sm;
    public enum CoinColor{ copper=1,silver=2,gold=3,rainbow=10};
    public CoinColor myColor=CoinColor.copper;
    private void Awake()
    {
        
    }
    Rect getRect()
    {
        Rect temp = new Rect(initRect.center - new Vector2(initRect.width, initRect.height) + (Vector2)transform.position, initRect.size);
        return temp;
    }
    private void Start()
    {
        sm = ScreenManager.Instance;
        rotSpeed = Random.Range(270, 360);
        transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 300)));
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        parent = coinGun.Instance;
        
    }
    [HideInInspector]
    public float posRatio = 0;
    Vector2 endpos;
    float heightpos;
    float speed = 1;
   
    public void init(CoinColor color,Vector2 endpos,float heightpos,float speed)
    {
      
      
        myColor = color;
        posRatio = 0;
        switch(color)
        {
            case CoinColor.copper:
                spritefront.sprite = sprites_coin[0];
                break;
            case CoinColor.silver:
                spritefront.sprite = sprites_coin[1];
                break;
            case CoinColor.gold:
                spritefront.sprite = sprites_coin[2];
                break;
            case CoinColor.rainbow:
                spritefront.sprite = sprites_coin[3];
                break;
            default:
                spritefront.sprite = sprites_coin[0];
                break;
        }

        this.endpos = endpos;
        this.heightpos = heightpos;
        spritefront.sortingOrder = Mathf.RoundToInt(this.endpos.y * 100f) * -1;

        this.speed = speed;
    }
    void OnDrawGizmos()
    {
        // Green
        Gizmos.color = new Color(0.0f, 1.0f, 0.0f);
        myCollections.DrawRect(getRect());
    }
   
    
    public bool isContain(Rect rect)
    {
        return getRect().Overlaps(rect);
    }
    
    private void FixedUpdate()
    {
        posRatio += Time.deltaTime * speed;
        posRatio=Mathf.Clamp(posRatio,0,1);
        transform.Rotate(myMath.vectorY * Time.deltaTime * rotSpeed);
        
        transform.position = MathParabola.Parabola(Vector2.zero, endpos, heightpos , posRatio);
       
    }

}