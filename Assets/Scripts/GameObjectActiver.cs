using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectActiver : MonoBehaviour
{
    [SerializeField] private GameObject target = null;

    public void OnActive()
    {
        target.SetActive(true);
    }

    public void OnDeActive()
    {
        target.SetActive(false);
    }
}
