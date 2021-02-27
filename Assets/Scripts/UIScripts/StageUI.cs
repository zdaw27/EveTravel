using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;


public class StageUI : MonoBehaviour
{
    [SerializeField]
    GameData gameData;
    [SerializeField]
    private Text stageText;
    private StringBuilder sb = new StringBuilder();

    public void UpdateStageUI()
    {
        sb.Clear();
        sb.Append("Stage : ").Append((gameData.StageLevel + 1).ToString());
        stageText.text = sb.ToString();
    }
}
