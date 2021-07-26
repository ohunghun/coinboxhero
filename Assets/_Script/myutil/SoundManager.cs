using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public AudioSource speaker;

    AudioClip[] clips;
    Hashtable hash = new Hashtable();
    private void Awake()
    {
        instance = this;
        clips = Resources.LoadAll<AudioClip>("sounds");

    }
    AudioClip getClip(string id)
    {
        if (hash.ContainsKey(id))
            return (AudioClip)hash[id];
        else
        {
            foreach (AudioClip sp in clips)
            {
                if (sp.name == id)
                {
                    hash.Add(id, sp);
                    return sp;
                }
            }
        }
        return null;
    }

    public AudioSource backgorundSource;
    public void backgroundOff()
    {
        backgorundSource.Pause();
    }
    public void backgroundOn()
    {
        backgorundSource.Play();
    }
    public void play(string id)
    {
        speaker.PlayOneShot(getClip(id));
        
    }
    public void play(string id,float volume)
    {
        speaker.PlayOneShot(getClip(id),volume);

    }
    static SoundManager instance;
  
    public static SoundManager getInstance()
    {
        return instance;
    }
}
