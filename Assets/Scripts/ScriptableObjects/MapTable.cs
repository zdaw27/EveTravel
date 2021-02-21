using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    [CreateAssetMenu(fileName = "MapTable", menuName = "ScriptableObjects/MapTable", order = 1)]
    public class MapTable : ScriptableObject
    {
        [SerializeField]
        private List<EveMap> maps = new List<EveMap>();

        public List<EveMap> Maps { get => maps; set => maps = value; }
    }
}
