using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
public class timelineDialogTrigger : Singleton<timelineDialog>
{
    public PlayableDirector director;
    public timelineDialogunit[] sentances;
    int curindex=0;
    public void triggerDialog()
    {
        timelineDialogunit cur = sentances[curindex];

        timelineDialog.Instance.showDialog(director, Lang.Instance.getString(cur.langid), Lang.Instance.getString(cur.npcid), cur.isauto,cur.delay);
        curindex = (curindex + 1) % sentances.Length;
    }
}
