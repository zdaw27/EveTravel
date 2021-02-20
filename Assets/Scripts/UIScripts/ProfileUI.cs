using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileUI : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private Image image;
    [SerializeField] private Text goldText;
    [SerializeField] private Text levelText;

    public void Update()
    {
        image.fillAmount = (float)gameData.Player.Stat.hp / (float)gameData.Player.Stat.maxHp;
    }

    public void UpdateGoldText()
    {
        goldText.text = gameData.Gold.ToString();
    }

    public void UpdateLevelText()
    {
        levelText.text = gameData.PlayerLevel.ToString();
    }
}
