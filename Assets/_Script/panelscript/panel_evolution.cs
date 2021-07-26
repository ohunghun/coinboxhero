using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class panel_evolution : panel
{
    public Animator ani;
    public Image origin, evol;
    public override void OnOpended(params object[] arg)
    {

        origin.sprite = spritemanager.Instance.getSprite("horse" + (HorseManager.Instance.getOwnStat().level - 1));
        evol.sprite = spritemanager.Instance.getSprite("horse" + HorseManager.Instance.getOwnStat().level);
        ani.SetFloat("Progress", 0);
        DOTween.To(() => ani.GetFloat("Progress"), x => ani.SetFloat("Progress", x), 1, 1.7f).OnComplete(OnAnimationComplete);
      
    }
    public override void OnClosed()
    {
        Debug.Log("evolutionClosed");
    }
    void OnAnimationComplete()
    {
        StartCoroutine("closepanel");
    }
    IEnumerator closepanel()
    {
        yield return new WaitForSeconds(1.2f);
       panelManager.Instance.closeCurrent();
      

    }
 

}
