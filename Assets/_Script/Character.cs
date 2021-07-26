using UnityEngine;
using System.Collections;

public abstract class Character : MonoBehaviour
{
    public Transform body;
    public Animator ani;
    abstract public void attack();

    float lastPos;
    private void LateUpdate()
    {
        float delta = Mathf.Abs(transform.position.x - lastPos);
        lastPos = transform.position.x;
        ani.SetFloat("speed", delta / Time.deltaTime);
    }
    public void move(float dir)
    {
        
        if (dir < 0)
            body.rotation = Quaternion.Euler(new Vector3(0, 180));
        else
            body.rotation = Quaternion.Euler(new Vector3(0, 0));
        transform.Translate(new Vector3(Time.deltaTime * 0.07f * dir, 0, 0));
        
    }
    public bool isGround;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGround = true;
            OnGround();
        }
        if(collision.gameObject.tag=="box")
        {
            OnBoxhit();
        }
    }

    virtual protected void OnBoxhit()
    {

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isGround = false;
            OnJump();
        }
    }
    virtual protected void OnGround()
    {

    }
    virtual protected void OnJump()
    {

    }


}
