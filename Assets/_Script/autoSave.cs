using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoSave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        HorseManager.Instance.AddOnOwnerUpdated(OnLevelUp);
    }

    void OnLevelUp()
    {
        savemanager.Instance.save();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
