using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

[CustomEditor(typeof(HealthBar))]
public class HealthBarDrawer : SliderEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        HealthBar healthBar = (HealthBar)target;
        healthBar.HealthChangedEvent = EditorGUILayout.ObjectField("On Health Changed Event", healthBar.HealthChangedEvent, typeof(IntEvent), true) as IntEvent;
    }
}
