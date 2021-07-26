
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class openUFoPanel :MonoBehaviour
{
  
    private void OnMouseUpAsButton()
    {
        if(!myCollections.IsPointerOverUIObject())
        panelManager.Instance.openPanel("cloneMachine");
    }
    
   
    
}
