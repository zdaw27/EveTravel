using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class LevelUPUI : MonoBehaviour
    {
        [SerializeField]
        private GameData gameData;
        [SerializeField]
        private GameObjectActiver activer;
        [SerializeField]
        private GameEvent statChangedEvent;
        [SerializeField]
        private UISmoothInOut uiSmoothInOut;

        public void AddHPStat()
        {
            CharacterStat stat = gameData.Player.Stat;
            stat.maxHp += 20;
            stat.hp = stat.maxHp;
            gameData.Player.Stat = stat;
            uiSmoothInOut.OutSmooth();
        }

        public void AddAttackStat()
        {
            CharacterStat stat = gameData.Player.Stat;
            stat.attack += 10;
            stat.hp = stat.maxHp;
            gameData.Player.Stat = stat;
            uiSmoothInOut.OutSmooth();
        }

        public void AddArmorStat()
        {
            CharacterStat stat = gameData.Player.Stat;
            stat.armor += 2;
            stat.hp = stat.maxHp;
            gameData.Player.Stat = stat;
            uiSmoothInOut.OutSmooth();
        }
    }
}