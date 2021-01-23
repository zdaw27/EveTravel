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
        static FlexibleViewer flexibleViewer = null;

        static private Rect _editorRect = new Rect();
        static public Rect editorRect { get { return _editorRect; } set { _editorRect = value;
                if(_editorRect.size != value.size)
                    onEditorSizeChanged(value); } }
        static public System.Action<Rect> onEditorSizeChanged { get; set; }


        [MenuItem("Editor/EveMapEditor")]
        static void OpenEditor()
        {
            Init();
            mapEditor.Show();
        }

        private void OnEnable()
        {
            _editorRect = this.position;
        }

        static void Init()
        {
            if (flexibleViewer == null)
                flexibleViewer = new FlexibleViewer();

            if (mapEditor == null)
                mapEditor = (EveMapEditor)EditorWindow.GetWindow(typeof(EveMapEditor));
        }

        void OnGUI()
        {
            editorRect = this.position;

            flexibleViewer.OnGUI();
            Repaint();
        }
    }
}