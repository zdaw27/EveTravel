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
            mode = (EditMode)EditorGUILayout.EnumPopup(mode);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("ENABLE EDIT");
            EditorGUILayout.Toggle(enableEditMode);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField("TILE1");
            editorData.Tile1 = (GameObject)EditorGUILayout.ObjectField( editorData.Tile1, typeof(GameObject), false);
            EditorGUILayout.LabelField("TILE2");
            editorData.Tile2 = (GameObject)EditorGUILayout.ObjectField( editorData.Tile2, typeof(GameObject), false);
            EditorGUILayout.LabelField("Target MAP");
            editorData.EveMap = (EveMap)EditorGUILayout.ObjectField(editorData.EveMap, typeof(EveMap), true);

            EditorGUILayout.LabelField("Map width");
            editorData.Width = EditorGUILayout.IntField(editorData.Width);
            EditorGUILayout.LabelField("Map height");
            editorData.Height = EditorGUILayout.IntField(editorData.Height);
            EditorUtility.SetDirty(editorData);

            if (GUILayout.Button("Init Map"))
            {
                InitMap(editorData.Width, editorData.Height);
            }
        }

        private void OnEnable()
        {
            SceneView.duringSceneGui += SceneUpdate;

            string cursorPath = "Assets/Prefabs/Cursor.prefab";
            GameObject cursorPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(cursorPath, typeof(GameObject));
            editorData = (EditorData)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Editor/ScriptableObjects/EditorData.asset", typeof(EditorData));
            if (!cursor)
            {
                cursor = (GameObject)PrefabUtility.InstantiatePrefab(cursorPrefab);
                cursor.name = "Cursor";
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

        void InitMap(int width, int height)
        {
            editorData.EveMap.TileCell.Clear();
            ClearTileGameObject();

            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    int index = x * width + y;
                    CreateTile(new Vector3(x, y, 0f));
                }
            }
        }

        void CreateTile(Vector3 tilePos)
        {
            editorData.EveMap.TileCell.Add(TileCell.Create(TileType.walkable));
            GameObject newTilePrefab = GetTilePrefab(tilePos);
            GameObject createdTile = GameObject.Instantiate(newTilePrefab, editorData.EveMap.transform);
            createdTile.layer = 8;
            createdTile.transform.position = tilePos;
        }

        void ClearTileGameObject()
        {
            for (int i = editorData.EveMap.transform.childCount; i > 0; --i)
                DestroyImmediate(editorData.EveMap.transform.GetChild(0).gameObject);
        }

        GameObject GetTilePrefab(Vector3 mousePos)
        {
            if ((int)mousePos.y % 2 == 0)
            {
                if ((int)mousePos.x % 2 == 0)
                    return editorData.Tile1;
                else
                    return editorData.Tile2;
            }
            else
            {
                if ((int)mousePos.x % 2 == 0)
                    return editorData.Tile2;
                else
                    return editorData.Tile1;
            }
        }

        

        void SceneUpdate(SceneView sceneview)
        {
            int controlID = GUIUtility.GetControlID(FocusType.Passive);
            Event e = Event.current;

            //Debug.Log(e.mousePosition);
            //Ray ray;
            //ray = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
            Vector3 mousePos = HandleUtility.GUIPointToWorldRay(e.mousePosition).origin;

            DrawCursor(mousePos);
            //DrawGrid(1, 1, Color.blue);

            if (e.control && (e.type == EventType.MouseDown) && (e.button == 0))
            {
                int tileIndex = GetIndex((int)cursor.transform.position.x, (int)cursor.transform.position.y);
                if (cursor.transform.position.x < editorData.Width && cursor.transform.position.y < editorData.Height 
                    && cursor.transform.position.x >= 0 && cursor.transform.position.y >= 0)
                {
                    Debug.Log("click" + tileIndex);
                }
                
            }
        }

        void DrawCursor(Vector3 mousePos)
        {
            Vector3 currentTile = new Vector3(GetTilePosition(mousePos.x), GetTilePosition(mousePos.y), -10f);
            //if (Vector3.Distance(currentTile, cursor.transform.position) > .1f)
                cursor.transform.position = currentTile;
        }

        public int GetIndex(int xPos, int yPos)
        {
            return xPos * editorData.Height + yPos;
        }

        public float GetTilePosition(float pos)
        {

            float sign = Mathf.Sign(pos);
            pos = Mathf.Abs(pos);

            int FullTiles = (int)(pos + 0.5f / tileWidth);

            float Offset = FullTiles * tileWidth;
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
        Erase,
        PlayerSpawn,
        EnemySpawn,
        Store,
        Exit,
    }
}