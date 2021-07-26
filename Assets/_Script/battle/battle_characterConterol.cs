using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mTypes;
using UnityEngine.Rendering;

public class battle_characterConterol : MonoBehaviour
{
    Rigidbody2D rig;
    public float limitHigh = 2;
    public float limitLow = -4;
    SortingGroup sg;
    // Start is called before the first frame update
    void Start()
    {
        rig= gameObject.GetComponent<Rigidbody2D>();
        battleJoystick.Instance.addCallbackMove(onmove);
        sg = GetComponent<SortingGroup>();
        
    }
    public float speed = 4;
    void onmove(Vector2 dir)
    {
       
        transform.Translate(dir * Time.deltaTime * speed);
       
    }

    
    
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > limitHigh)
            transform.position = new Vector3(transform.position.x, limitHigh, transform.position.z);
        else if (transform.position.y < limitLow)
            transform.position = new Vector3(transform.position.x, limitLow, transform.position.z);
        sg.sortingOrder = (int)(10000 - (transform.position.y * 1000));
    }
}
