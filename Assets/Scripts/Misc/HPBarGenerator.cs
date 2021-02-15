using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBarGenerator : MonoBehaviour
{
    [SerializeField] private GameObject hPBarPrefab;

    private void Awake()
    {
        GameObject gameObject = GameObject.Instantiate(hPBarPrefab, transform);
    }
}
