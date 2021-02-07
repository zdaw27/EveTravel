using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EditorData", menuName = "ScriptableObjects/Editor/EditorData", order = 1)]
public class EditorData : ScriptableObject
{
    public GameObject tile1;
    public GameObject tile2;
    public int width;
    public int height;
}
