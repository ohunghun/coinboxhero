using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(MatchSprite2Born))]
public class MatchSprite2BornButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MatchSprite2Born generator = (MatchSprite2Born)target;
        if (GUILayout.Button("Matxch"))
        {
            generator.Match();
        }
    }
}