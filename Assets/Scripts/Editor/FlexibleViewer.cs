﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace EveTravel
{
    public class FlexibleViewer
    {
        private Vector2 scrollPos = Vector2.zero;
        float currentScrollViewHeight;
        bool resize = false;
        Rect cursorChangeRect;

        public FlexibleViewer()
        {
            EveMapEditor.onEditorSizeChanged += OnEditorSizeChanged;
        }

        public void OnEditorSizeChanged(Rect editorRect)
        {
            currentScrollViewHeight = editorRect.height / 2;
            cursorChangeRect = new Rect(0, currentScrollViewHeight, editorRect.width, 5f);
        }

        public void OnGUI()
        {
            GUILayout.BeginVertical();
            scrollPos = GUILayout.BeginScrollView(scrollPos, GUILayout.Height(currentScrollViewHeight));
            for (int i = 0; i < 20; i++)
                GUILayout.Label("dfs");
            GUILayout.EndScrollView();

            ResizeScrollView();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Lower part");

            GUILayout.EndVertical();
        }

        private void ResizeScrollView()
        {
            GUI.DrawTexture(cursorChangeRect, EditorGUIUtility.whiteTexture);
            EditorGUIUtility.AddCursorRect(cursorChangeRect, MouseCursor.ResizeVertical);

            if (Event.current.type == EventType.MouseDown && cursorChangeRect.Contains(Event.current.mousePosition))
            {
                resize = true;
            }
            if (resize)
            {
                currentScrollViewHeight = Event.current.mousePosition.y;
                cursorChangeRect.Set(cursorChangeRect.x, currentScrollViewHeight, cursorChangeRect.width, cursorChangeRect.height);
            }
            if (Event.current.type == EventType.MouseUp)
                resize = false;
        }
    }
}