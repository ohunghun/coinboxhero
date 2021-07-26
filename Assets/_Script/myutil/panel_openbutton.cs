
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class panel_openbutton : MonoBehaviour
{
    public string panelName;

    void Start()
    {
        if (panelName == "close")
            GetComponent<Button>().onClick.AddListener(() => { panelManager.Instance.closeCurrent(); });
        else
            GetComponent<Button>().onClick.AddListener(() => { panelManager.Instance.openPanel(panelName); });
        
    }
 

    
}
