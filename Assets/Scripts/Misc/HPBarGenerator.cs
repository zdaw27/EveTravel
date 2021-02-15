using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class HPBarGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject hPBarPrefab;
        [SerializeField] private Character character;

        private void Awake()
        {
            GameObject gameObject = GameObject.Instantiate(hPBarPrefab, transform);
            gameObject.GetComponent<HPBar>().character = character;
        }
    }
}