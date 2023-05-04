using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIBuilding : MonoBehaviour
{
    public TextMeshProUGUI currentPercentageText;
    public BuildingUCManager building;

    // updates current percentage counter on the pop-up panel of a building under construction
    public void UpdateCurrentPercentage()
    {
        currentPercentageText.text = building.currentPercentage.ToString();
    }
}
