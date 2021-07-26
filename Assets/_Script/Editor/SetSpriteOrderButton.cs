using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(SetspriteOrder))]
public class SetSpriteOrderButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SetspriteOrder generator = (SetspriteOrder)target;
        if (GUILayout.Button("SetOrder"))
        {
            generator.setOrder();
        }
    }
}
