using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using mTypes;
public class dialogManager : Singleton<dialogManager>
{
    callbackVoid onEnded;
    public Text text;
    Queue<string> sentances=new Queue<string>();
    bool isAuto = false;
    public float delay=1.2f;
    AudioSource ass;
    public AudioClip cliptype;
    private void Start()
    {
        ass = GetComponent<AudioSource>();
    }
    public void StartDialog(dialog dialog)
    {
        onEnded = null;
        sentances.Clear();
        foreach (string str in dialog.sentances)
            sentances.Enqueue(str);
        displayNext();
    }
    public void StartDialog(dialog dialog, callbackVoid onended)
    {
        this.onEnded = onended;
        StartDialog(dialog);
    }
    public void setAuto(bool val)
    {
        isAuto = val;
    }
    public void displayNext()
    {
        if(sentances.Count==0)
        {
            
            EndDialogue();
            return;
        }
        string sent=sentances.Dequeue();
        
        StopAllCoroutines();
        StartCoroutine(typeText(Lang.Instance.getString(sent)));
        
    }
    IEnumerator typeText(string sentence)
    {
        text.text = "";
        foreach(char ch in sentence.ToCharArray())
        {
            text.text += ch;
            if(cliptype!=null)
            ass.PlayOneShot(cliptype);
            yield return new WaitForSeconds(0.1f);
        }
        if (isAuto)
        {
            yield return new WaitForSeconds(delay);
            displayNext();
        }
    }
    public void EndDialogue()
    {
        isAuto = false;
        text.text = "";
        if (onEnded != null)
            onEnded();
    }
}
