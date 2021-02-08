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

        private void OnGUI()
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
            EditorGUILayout.LabelField("WALL TILE");
            editorData.WallTile = (GameObject)EditorGUILayout.ObjectField(editorData.WallTile, typeof(GameObject), false);
            EditorGUILayout.LabelField("Target MAP");
            editorData.EveMap = (EveMap)EditorGUILayout.ObjectField(editorData.EveMap, typeof(EveMap), true);

            EditorGUILayout.LabelField("Map width");
            editorData.EveMap.Width = EditorGUILayout.IntField(editorData.EveMap.Width);
            EditorGUILayout.LabelField("Map height");
            editorData.EveMap.Height = EditorGUILayout.IntField(editorData.EveMap.Height);
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

        private void InitMap(int width, int height)
        {
            editorData.EveMap.TileCell.Clear();
            editorData.EveMap.PlayerSpawnIndex.Clear();
            editorData.EveMap.EnemySpawnIndex.Clear();
            editorData.EveMap.TreasureIndex.Clear();
            editorData.EveMap.ExitIndex.Clear();
            ClearTileGameObject();

            for (int y = 0; y < height; ++y)
                for (int x = 0; x < width; ++x)
                {
                    int index = x  + y * width;
                    CreateTile(new Vector3(x, y, 0f));
                }
        }

        private void CreateTile(Vector3 tilePos)
        {
            editorData.EveMap.TileCell.Add(TileCell.Create(TileType.walkable));
            GameObject newTilePrefab = GetTilePrefab(tilePos);
            GameObject createdTile = GameObject.Instantiate(newTilePrefab, editorData.EveMap.transform);
            createdTile.layer = 8;
            createdTile.transform.position = tilePos;
        }

        private void ClearTileGameObject()
        {
            for (int i = editorData.EveMap.transform.childCount; i > 0; --i)
                DestroyImmediate(editorData.EveMap.transform.GetChild(0).gameObject);
        }

        private GameObject GetTilePrefab(Vector3 mousePos)
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

        private void SceneUpdate(SceneView sceneview)
        {
            int controlID = GUIUtility.GetControlID(FocusType.Passive);
            Event e = Event.current;

            //Ray ray = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
            Vector3 mousePos = HandleUtility.GUIPointToWorldRay(e.mousePosition).origin;

            //Draw Logics
            DrawCursor(mousePos);

            foreach (int index in editorData.EveMap.PlayerSpawnIndex)
            {
                DrawRectLine(IndexToPos(index), Color.blue);
            }

            foreach (int index in editorData.EveMap.EnemySpawnIndex)
            {
                DrawRectLine(IndexToPos(index), Color.red);
            }

            foreach (int index in editorData.EveMap.TreasureIndex)
            {
                DrawRectLine(IndexToPos(index), Color.yellow);
            }

            foreach (int index in editorData.EveMap.ExitIndex)
            {
                DrawRectLine(IndexToPos(index), Color.magenta);
            }

            foreach (int index in editorData.EveMap.StoreIndex)
            {
                DrawRectLine(IndexToPos(index), Color.white);
            }

            //Event Logics
            if (e.control && (e.type == EventType.MouseDown) && (e.button == 0))
            {
                int tileIndex = GetIndex((int)cursor.transform.position.x, (int)cursor.transform.position.y);
                if (cursor.transform.position.x < editorData.Width && cursor.transform.position.y < editorData.Height 
                    && cursor.transform.position.x >= 0 && cursor.transform.position.y >= 0)
                {
                    switch (mode)
                    {
                        case EditMode.Tile:
                            if (editorData.EveMap.TileCell[tileIndex].Type == TileType.walkable)
                            {
                                Transform childTr = editorData.EveMap.transform.GetChild(tileIndex);
                                GameObject newTileObj = GameObject.Instantiate(editorData.WallTile, editorData.EveMap.transform);
                                newTileObj.transform.position = childTr.position;
                                GameObject.DestroyImmediate(childTr.gameObject);
                                newTileObj.transform.SetSiblingIndex(tileIndex);
                                editorData.EveMap.TileCell[tileIndex].Type = TileType.noneWlakable;
                            }
                            else
                            {
                                Transform childTr = editorData.EveMap.transform.GetChild(tileIndex);
                                GameObject newTileObj = GameObject.Instantiate(GetTilePrefab(cursor.transform.position), editorData.EveMap.transform);
                                newTileObj.transform.position = childTr.position;
                                GameObject.DestroyImmediate(childTr.gameObject);
                                newTileObj.transform.SetSiblingIndex(tileIndex);
                                editorData.EveMap.TileCell[tileIndex].Type = TileType.walkable;
                            }
                            break;
                        case EditMode.PlayerSpawn:
                            if (editorData.EveMap.PlayerSpawnIndex.Contains(tileIndex))
                                editorData.EveMap.PlayerSpawnIndex.Remove(tileIndex);
                            else
                                editorData.EveMap.PlayerSpawnIndex.Add(tileIndex);
                            break;
                        case EditMode.EnemySpawn:
                            if (editorData.EveMap.EnemySpawnIndex.Contains(tileIndex))
                                editorData.EveMap.EnemySpawnIndex.Remove(tileIndex);
                            else
                                editorData.EveMap.EnemySpawnIndex.Add(tileIndex);
                            break;
                        case EditMode.TreasureSpawn:
                            if (editorData.EveMap.TreasureIndex.Contains(tileIndex))
                                editorData.EveMap.TreasureIndex.Remove(tileIndex);
                            else
                                editorData.EveMap.TreasureIndex.Add(tileIndex);
                            break;
                        case EditMode.Store:
                            if (editorData.EveMap.StoreIndex.Contains(tileIndex))
                                editorData.EveMap.StoreIndex.Remove(tileIndex);
                            else
                                editorData.EveMap.StoreIndex.Add(tileIndex);
                            break;
                        case EditMode.Exit:
                            if (editorData.EveMap.ExitIndex.Contains(tileIndex))
                                editorData.EveMap.ExitIndex.Remove(tileIndex);
                            else
                                editorData.EveMap.ExitIndex.Add(tileIndex);
                            break;
                        
                    }

                }

            }
        }

        private void DrawCursor(Vector3 mousePos)
        {
            Vector3 currentTile = new Vector3(GetTilePosition(mousePos.x), GetTilePosition(mousePos.y), -10f);
            //if (Vector3.Distance(currentTile, cursor.transform.position) > .1f)
                cursor.transform.position = currentTile;
        }

        private void DrawRectLine(Vector3 position, Color color)
        {
            Handles.color = color;

            Handles.DrawLine(position + Vector3.left * 0.5f + Vector3.down * 0.5f, position + Vector3.right * 0.5f + Vector3.down * 0.5f);
            Handles.DrawLine(position + Vector3.right * 0.5f + Vector3.down * 0.5f, position + Vector3.right * 0.5f + Vector3.up * 0.5f);
            Handles.DrawLine(position + Vector3.right * 0.5f + Vector3.up * 0.5f, position + Vector3.left * 0.5f + Vector3.up * 0.5f);
            Handles.DrawLine(position + Vector3.left * 0.5f + Vector3.up * 0.5f, position + Vector3.left * 0.5f + Vector3.down * 0.5f);
        }

        private int GetIndex(int xPos, int yPos)
        {
            return xPos + yPos * editorData.Width;
        }

        private Vector3 IndexToPos(int index)
        {
            return new Vector3(index % editorData.Width, index / editorData.Width);
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
        Tile,
        PlayerSpawn,
        EnemySpawn,
        TreasureSpawn,
        Store,
        Exit,
    }
}