using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Main))]
[CanEditMultipleObjects]
public class GoldEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("골드 증가"))
        {
            Main.Instance.AnIncreaseInGold(10000000f);
        }
        if(GUILayout.Button("룬 증가"))
        {
            Main.Instance.AnIncreaseInRune(10000000f);
        }
    }
}
