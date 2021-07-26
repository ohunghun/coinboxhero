using UnityEngine;
using System.Collections;
using DG.Tweening;
public abstract class Horse : MonoBehaviour
{
    public Stat stat;
    protected Transform body;
    protected Animator ani;
    float lastPos;
    public bool isAuto = true;
    [HideInInspector]
    public float power = 1;
    [HideInInspector]
    public float powerAmp = 1;
    [HideInInspector]
    public float weaponId = -1;
    public Transform weaponPoint;
    weapon current_weaponObj = null;
    
    public float moveSpeed = 1;
    protected bool isRandomMove = false;
    GameObject effect_power;
    public bool isAutoStop = false;
    ScreenManager sm;
    private void Awake()
    {
        body = transform.GetChild(0);
        ani = body.GetComponent<Animator>();
        effect_power=body.findChildByName("effect_power").gameObject;
        StartCoroutine("randomMove");
        effect_power.SetActive(false);
        StartCoroutine("autoLoop");
        sm = ScreenManager.Instance;
    }
  
    public void powerAmpUp(float amp, float time)
    {
        effect_power.SetActive(true);
        effect_power.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.3f);
        powerAmp = amp;
        myCountDownTimer.Instance.CountDown(time, () =>
        {
            effect_power.SetActive(false);
            powerAmp = 1;
        });
    }
    LayerMask layer;
    public bool isground()
    {
        layer= LayerMask.GetMask("barrier");
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        CircleCollider2D col2 = GetComponent<CircleCollider2D>();
        if(col!=null)
        {
            RaycastHit2D raycast = Physics2D.Raycast(col.bounds.center, Vector2.down, col.bounds.extents.y + 0.1f, layer);
            return raycast.collider != null;
        }else
        {
            RaycastHit2D raycast = Physics2D.Raycast(col2.bounds.center, Vector2.down, col2.bounds.extents.y + 0.1f, layer);
            return raycast.collider != null;
        }

    }
    IEnumerator autoLoop()
    {
        
        yield return new WaitForSeconds(Random.Range(0.5f,3f));
        
        while (isAuto)
        {
        
            while(isAutoStop)
                yield return null;
            yield return AutoUpdate();
            yield return null;
        }
      
    }
    abstract protected  IEnumerator AutoUpdate();

    IEnumerator randomMove()
    {
        while (true)
        {
            while (!isRandomMove)
            {
                yield return null;
            }
            float targetPos = Random.Range(ScreenManager.Instance.getScreenLeft(), ScreenManager.Instance.getScreenRight());
            float dir = transform.position.x - targetPos;
            while (Mathf.Abs(dir) > 1f && isRandomMove)
            {
                dir = targetPos - transform.position.x;
                if (dir > 0)
                    move(100f);
                else if (dir < 0)
                    move(-100f);
                yield return null;
            }
        }
    }
    joystick.EventCallback joystickEvent;
    public void init(Stat stat, bool isauto)
    {
        this.isAuto = isauto;
        if (!isAuto)
        {
            joystickEvent = new joystick.EventCallback(move, _attack, attackLong_held, attackLong_release);
            joystick.Instance.AddCallback(joystickEvent);
        }
        this.stat = stat;
        power = float.Parse(DataBase.Instance.getRecord("horse" + stat.level)["power"]);
        
        if(stat.weaponLevel!=0)
            weaponEquip(stat.weaponLevel);


    }
 

    private void weaponEquip(int level)
    {
        Debug.Log("aaaaaa  isauto "+isAuto);
        string weaponNanme = "weapon" + level;
        GameObject prefab = PrefabManager.Instance.getPrefab(weaponNanme);
        current_weaponObj = Instantiate(prefab).GetComponent<weapon>();
        current_weaponObj.transform.localScale = new Vector3(1, 1, 1);
        current_weaponObj.transform.parent = weaponPoint;
        current_weaponObj.transform.localPosition = Vector3.zero;
        current_weaponObj.initWeapon(isAuto);

        
    }
    abstract public void attackLong_held();
    abstract public void attackLong_release();

    private void LateUpdate()
    {


        float delta = Mathf.Abs(transform.position.x - lastPos);
        lastPos = transform.position.x;
        ani.SetFloat("speed", delta / Time.deltaTime);

        if (transform.position.x < sm.screenLeftToWorldX)
        {
            transform.position = new Vector3(sm.screenLeftToWorldX, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > sm.screenRightToWorldX)
            transform.position = new Vector3(sm.screenRightToWorldX, transform.position.y, transform.position.z);
    }
    public void initRot()
    {

        if(body.rotation.eulerAngles.y<10&& body.rotation.eulerAngles.y>-10 || body.rotation.eulerAngles.y > 200)
            body.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        else 
            body.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        
    }
    public void backwordBox()
    {
        float dir = box.Instance.transform.position.x - transform.position.x;
        if (dir * flip > 0)
        {
            body.rotation = Quaternion.Euler(new Vector3(0, 180));
        }
        else
        {
            body.rotation = Quaternion.Euler(new Vector3(0, 0));
        }
    }
    public void forwardBox()
    {
        float dir = box.Instance.transform.position.x - transform.position.x;
        if (dir * flip < 0)
        {
            body.rotation = Quaternion.Euler(new Vector3(0, 180));
        }
        else
        {
            body.rotation = Quaternion.Euler(new Vector3(0, 0));
        }
    }
    public void forwardTarget(Transform target)
    {
        float dir = target.position.x - transform.position.x;
        if (dir * flip < 0)
        {
            body.rotation = Quaternion.Euler(new Vector3(0, 180));
        }
        else
        {
            body.rotation = Quaternion.Euler(new Vector3(0, 0));
        }
    }
    public int flip = -1;
    public float getPower()
    {
        return power + stat.BonusPower;
    }
    public float getSpeed()
    {
        return 3 + stat.speed*0.8f;
    }
    public void move(float dir)
    {
        if (dir == 0)
            return;

        float absdir = Mathf.Abs(dir);
        float np= absdir / dir;
        float clampdir = Mathf.Clamp(absdir, 0.5f, 1);
        dir = clampdir * np;
        
        if (dir * flip < 0)
        {
            body.rotation = Quaternion.Euler(new Vector3(0, 180));
        }
        else
        {
            body.rotation = Quaternion.Euler(new Vector3(0, 0));
        }


        transform.Translate(new Vector3(Time.deltaTime * dir * getSpeed(), 0, 0));

    }
    int count = 0;

    castingTimeLimitChecker boxSpeedLimit = new castingTimeLimitChecker(0.05f);
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "box")
        {

            if (boxSpeedLimit.check())
            {
                onBoxHit();
                
            }

        }
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "box")
        {

            if (boxSpeedLimit.check())
                onBoxHit();

        }
    }

    abstract public void onBoxHit();
    
  
    

 
    void _attack()
    {
        
            attack();
        
    }
    abstract public void attack();
    private void OnDestroy()
    {
        joystick.Instance.removeCallback(joystickEvent);
    }

}
