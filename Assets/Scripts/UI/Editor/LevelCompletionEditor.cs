using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Game.Edit
{
    [CustomEditor(typeof(LevelCompletionHandler))]
    public class LevelCompletionEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var handler = (LevelCompletionHandler)target;
            base.OnInspectorGUI();
            if (GUILayout.Button("Display Level Complete"))
            {
                handler.DisplayLevelComplete();
            }
            if (GUILayout.Button("Hide Level Complete"))
            {
                handler.HideLevelComplete();
            }
        }
    }
}