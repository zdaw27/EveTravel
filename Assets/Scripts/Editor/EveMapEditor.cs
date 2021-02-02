using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace EveTravel
{
    public class EveMapEditor : EditorWindow
    {
        static private EveMapEditor mapEditor = null;
        private EditMode mode;
        private bool enableEditMode = false;


        [MenuItem("Editor/EveMapEditor")]
        static void OpenEditor()
        {
            Init();
            mapEditor.Show();
        }
        

        static void Init()
        {
            if (mapEditor == null)
                mapEditor = (EveMapEditor)EditorWindow.GetWindow(typeof(EveMapEditor));
        }

        void OnGUI()
        {
            EditorGUILayout.EnumPopup(mode);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("ENABLE EDIT");
            EditorGUILayout.Toggle(enableEditMode);
            EditorGUILayout.EndHorizontal();
        }

        private void OnDisable()
        {
            enableEditMode = false;
        }
    }

    public enum EditMode
    {
        Draw,
        Erase
    }
}