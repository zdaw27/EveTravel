using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class PlayerFollower : MonoBehaviour
    {
        [SerializeField]
        private GameData gameData = null;
        [SerializeField]
        private Transform follower = null;

        private void LateUpdate()
        {
            if(gameData.Player)
                follower.position = new Vector3(gameData.Player.transform.position.x, gameData.Player.transform.position.y, follower.position.z);
        }
    }
}