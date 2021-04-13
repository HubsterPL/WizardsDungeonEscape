using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Game;

namespace Game.Edit
{
    [CustomEditor(typeof(ScreenBlackoutHandler))]
    public class ScreenBlackoutEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            ScreenBlackoutHandler handler = (ScreenBlackoutHandler)target;

            if (GUILayout.Button("HUD Fade In"))
            {
                handler.HUD_FadeIn();
            }
            if (GUILayout.Button("HUD Fade Out"))
            {
                handler.HUD_FadeOut();
            }
        }
    }
}