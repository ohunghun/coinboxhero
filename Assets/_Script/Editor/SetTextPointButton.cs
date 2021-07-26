using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(TextPoint))]
public class SetTextPointButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TextPoint generator = (TextPoint)target;
        if (GUILayout.Button("setText"))
        {
            generator.setText();
        }
    }
}
