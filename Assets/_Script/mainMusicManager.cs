using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMusicManager : Singleton<mainMusicManager>
{
    public AudioClip level0,  confuse, horse4,stick;
    public AudioSource source;
    private void Start()
    {
        HorseManager.Instance.AddOnInit(onlevelup);
        //HorseManager.Instance.AddOnOwnerUpdated(onlevelup);
    }
    
    private void onlevelup()
    {
        playNormalSound();
    }
    public void playNormalSound()
    {
        int level = HorseManager.Instance.getOwnStat().level;

        if (level == 0|| level == 1 || level == 2)
        {
            source.clip = level0;
            source.Play();
        }
        else
        {
            source.clip = horse4;
            source.Play();
        }
    }
    public void playStick()
    {
        source.clip = stick;
        source.Play();
    }
    public void playConfuse()
    {
        source.clip = confuse;
        source.Play();
    }

}
