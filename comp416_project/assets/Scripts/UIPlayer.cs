using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPlayer : MonoBehaviour
{
    public TextMeshProUGUI unitCountText;
    public TextMeshProUGUI foodCountText;
    public TextMeshProUGUI woodCountText;
    public TextMeshProUGUI stoneCountText;
    public TextMeshProUGUI goldCountText;

    public GameObject missionLog;

    public static UIPlayer instance;

    void Awake()
    {
        instance = this;
    }

    // updates unit counter on game UI
    public void UpdateUnitCountText(int value)
    {
        unitCountText.text = value.ToString();
    }

    // updates food counter on game UI
    public void UpdateFoodText(int value)
    {
        foodCountText.text = value.ToString();
    }

    // updates wood counter on game UI
    public void UpdateWoodText(int value)
    {
        woodCountText.text = value.ToString();
    }

    // updates stone counter on game UI
    public void UpdateStoneText(int value)
    {
        stoneCountText.text = value.ToString();
    }

    // updates gold counter on game UI
    public void UpdateGoldText(int value)
    {
        goldCountText.text = value.ToString();
    }

    public void HideMissionLog()
    {
        missionLog.SetActive(false);
    }
}