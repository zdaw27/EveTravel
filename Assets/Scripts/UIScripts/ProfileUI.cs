using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileUI : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private Image image;
    [SerializeField] private Text gold;

    private int lastGold = 0;

    public void Update()
    {
        if(lastGold != gameData.Gold)
            gold.text = gameData.Gold.ToString();
        image.fillAmount = (float)gameData.Player.Stat.hp / (float)gameData.Player.Stat.maxHp;
    }
}
