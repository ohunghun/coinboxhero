using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
public class stickguntype : stick
{
   
    Animator animator;
    public Transform attackPos;
    public string bulletname = "stick1_bullet";
    [HideInInspector]
    public SortingGroup sortgroup;
    override protected void Start()
    {
        base.Start();
        animator = transform.GetChild(0).GetChild(0).GetComponent<Animator>();
        sortgroup = GetComponent<SortingGroup>();
    }
    
    public void attack()
    {
        bulletManager.Instance.getPrefab(bulletname, 10, attackPos.position).GetComponent<stickbullet>().init(power, attackPos.position, sortgroup);
    }

    void OnEnable()
    {
      
        StartCoroutine("attackLoop");
    }
    float progress = 0;
    public float delay = 1;
    public float attackTiming = 0.8f;
    bool isattack = false;
    
    IEnumerator  attackLoop()
    {
        yield return new WaitForSeconds(Random.Range(0f,2f));
        while(true)
        {
            
            progress += Time.deltaTime*2;
            animator.SetFloat("progress", progress);
            if (!isattack && progress >attackTiming)
            {
                attack();
                isattack = true;
                
            }
            if (progress>=1)
            {
                progress = 0;
                isattack = false;
                yield return new WaitForSeconds(Random.Range(1.2f,delay));
            }

            yield return null;
        }
    }
   
}
