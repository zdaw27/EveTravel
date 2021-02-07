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
        private float tileWidth = 1f;
        private EditorData editorData;
         
        private SerializedProperty objectFieldSP;
        private GameObject cursor = null;
        private EveMap eveMap;

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

            EditorGUILayout.LabelField("TILE1");
            editorData.tile1 = (GameObject)EditorGUILayout.ObjectField( editorData.tile1, typeof(GameObject), false);
            EditorGUILayout.LabelField("TILE2");
            editorData.tile2 = (GameObject)EditorGUILayout.ObjectField( editorData.tile2, typeof(GameObject), false);
            EditorGUILayout.LabelField("Target MAP");
            eveMap = (EveMap)EditorGUILayout.ObjectField(eveMap, typeof(EveMap), true);

            EditorGUILayout.LabelField("Map width");
            editorData.width = EditorGUILayout.IntField(editorData.width);
            EditorGUILayout.LabelField("Map height");
            editorData.height = EditorGUILayout.IntField(editorData.height);
            EditorUtility.SetDirty(editorData);


            if (GUILayout.Button("Init Map"))
            {

            }
        }

        

        private void OnEnable()
        {
            Init();
            SceneView.duringSceneGui += SceneUpdate;

            string cursorPath = "Assets/Prefabs/Tiles/Tile1.prefab";
            GameObject cursorPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(cursorPath, typeof(GameObject));
            editorData = (EditorData)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Editor/ScriptableObjects/EditorData.asset", typeof(EditorData));
            if (!cursor)
            {
                cursor = (GameObject)PrefabUtility.InstantiatePrefab(cursorPrefab);
                cursor.layer = 8;
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

            Debug.Log(e.mousePosition);
            //Ray ray;
            //ray = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
            Vector3 mousePos = HandleUtility.GUIPointToWorldRay(e.mousePosition).origin;

            DrawCursor(mousePos);
            DrawGrid(1, 1, Color.blue);
        }

        void DrawCursor(Vector3 mousePos)
        {
            Vector3 currentTile = new Vector3(GetTilePosition(mousePos.x), GetTilePosition(mousePos.y), 0);
            if (Vector3.Distance(currentTile, cursor.transform.position) > .1f)
                cursor.transform.position = currentTile;
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
                Handles.DrawLine(new Vector3(-10.0f + camPos.x, Mathf.Floor(y / height) * height, 0.0f),
                    new Vector3(10.0f + camPos.x, Mathf.Floor(y / height) * height, 0.0f));
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