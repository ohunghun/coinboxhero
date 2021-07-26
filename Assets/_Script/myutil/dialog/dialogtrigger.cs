using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogtrigger : MonoBehaviour
{
    public dialog dialog;
    public bool isAuto;
    public void TriggerDialogue()
    {
        dialogManager.Instance.StartDialog(dialog);
        dialogManager.Instance.setAuto(isAuto);
    }
}
