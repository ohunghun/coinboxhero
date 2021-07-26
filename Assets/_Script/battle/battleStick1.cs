using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;

public class battleStick1 : MonoBehaviour
{
    public GameObject eyes;
    public GameObject body;
    [HideInInspector]
    public int index;
    Vector2 initpos;
    float posRatio = 0;
    Vector2 endpos;
    float heightpos;
    float speed = 1;
    public Animator ani;
    SortingGroup sg;
    public SpriteRenderer sp;
    public bool isTrue = false;
    public bool isSelected = false;
    private void Start()
    {
        
        
        initpos= endpos = transform.position;
        sg = GetComponent<SortingGroup>();
    }
    public void setTrueStick()
    {
        isTrue = true;
        eyeBlight();
    }
    public void eyeBlight()
    {
        eyes.SetActive(true);
        float prog = 0;
        DOTween.To(() => prog, x => 
        {
            
            ani.SetFloat("progress", x);
        }
        , 0.99f, 2f);

        

    }
    public void highlighted(bool set)
    {
        isSelected = set;
        if (set)
        {
            sp.material.SetFloat("_OutlineAlpha", 1);
            sp.material.SetFloat("_OutlineGlow", 15);
           
        }
        else{
            sp.material.SetFloat("_OutlineAlpha", 0);
            sp.material.SetFloat("_OutlineGlow", 15);
        }

    }
    public void jump(Vector2 endpos,float time)
    {
        if (!iscomplete)
        {
            transform.position = this.endpos;
        }
        posRatio = 0;
        this.endpos = endpos;
        initpos = transform.position;
        heightpos = 4 + 3 * Random.Range(0f, 1f);
        iscomplete = false;
    }
    bool iscomplete = false;
    protected  void Update()
    {
        posRatio += Time.deltaTime * speed;
        posRatio = Mathf.Clamp(posRatio, 0, 1);
        transform.position = MathParabola.Parabola(initpos, endpos, heightpos, posRatio);
        if(posRatio>=0.99f)
            iscomplete = true;
        sg.sortingOrder = (int)(10000 - (transform.position.y * 1000));

    }
    public void fade()
    {
        float prog = 0;
        DOTween.To(() => prog, x =>
           {
               sp.material.SetFloat("_FadeAmount", x);
           }, 1f, 2);
    }
    public void fadein()
    {
        body.SetActive(true);
        float prog = 1;
        DOTween.To(() => prog, x =>
        {
            sp.material.SetFloat("_FadeAmount", x);
            
        }, 0f, 2);
    }
}
