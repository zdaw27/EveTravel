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
        private GameObject tilePrefab1;
        private GameObject tilePrefab2;

        private float tileWidth = 1f;

        private SerializedProperty objectFieldSP;

        private GameObject cursor = null;

        [MenuItem("Editor/EveMapEditor")]
        static void OpenEditor()
        {
            if (mapEditor == null)
                mapEditor = (EveMapEditor)EditorWindow.GetWindow(typeof(EveMapEditor));
            mapEditor.Show();
        }

        void OnGUI()
        {
            EditorGUILayout.EnumPopup(mode);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("ENABLE EDIT");
            EditorGUILayout.Toggle(enableEditMode);
            EditorGUILayout.EndHorizontal();
            
            tilePrefab1 = (GameObject)EditorGUILayout.ObjectField(tilePrefab1, typeof(GameObject), false);
            tilePrefab2 = (GameObject)EditorGUILayout.ObjectField(tilePrefab2, typeof(GameObject), false);
           

            Handles.SetCamera(UnityEditor.SceneView.lastActiveSceneView.camera);
            Vector3 editorFloorPos = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin;
            editorFloorPos.y = 0;
            
        }

        

        private void OnEnable()
        {
            Init();
            SceneView.duringSceneGui += SceneUpdate;

            string cursorPath = "Assets/Tile1.prefab";
            GameObject cursorPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(cursorPath, typeof(GameObject));

            if (!cursor)
            {
                cursor = (GameObject)PrefabUtility.InstantiatePrefab(cursorPrefab);
            }

            
        }

        private void OnDisable()
        {
            enableEditMode = false;
            SceneView.duringSceneGui -= SceneUpdate;

            if(cursor)
            {
                GameObject.DestroyImmediate(cursor);
            }
        }

        void Init()
        {
        }

        void SceneUpdate(SceneView sceneview)
        {
            int controlID = GUIUtility.GetControlID(FocusType.Passive);
            Event e = Event.current;

            Ray ray;
            ray = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
            Vector3 mousePos = ray.origin;

            DrawCursor(mousePos);
            DrawGrid(1, 1, Color.blue);
        }

        void DrawCursor(Vector3 mousePos)
        {
            cursor.transform.position = new Vector3(GetTilePosition(mousePos.x), GetTilePosition(mousePos.y), 0);
        }

        public float GetTilePosition(float pos)
        {

            float sign = Mathf.Sign(pos);
            pos = Mathf.Abs(pos);

            int FullTiles = (int)(pos / tileWidth);
            int partTiles = 0;

            if (pos % tileWidth > .1)
                partTiles = 1;

            float Offset = FullTiles * tileWidth + tileWidth / 2;
            Offset *= sign;
            return Offset;
        }

        void DrawGrid(float width, float height, Color color)
        {
            Vector3 camPos = Camera.current.transform.position;
            Handles.color = color;

            for (float y = camPos.y - 10.0f; y < camPos.y + 10.0f; y += height)
            {
                Handles.DrawLine(new Vector3(-10.0f, Mathf.Floor(y / height) * height, 0.0f),
                    new Vector3(10.0f, Mathf.Floor(y / height) * height, 0.0f));
            }

            for (float x = camPos.x - 10.0f; x < camPos.x + 10.0f; x += width)
            {
                Handles.DrawLine(new Vector3(Mathf.Floor(x / width) * width, -10.0f, 0.0f),
                    new Vector3(Mathf.Floor(x / width) * width, 10.0f, 0.0f));
            }
        }
    }

    public enum EditMode
    {
        Draw,
        Erase
    }
}