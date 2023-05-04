using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIDoodad : MonoBehaviour
{
    public TextMeshProUGUI resourceAvailableText;
    public DoodadManager doodad;

    // updates available resource counter on the pop-up panel of a doodad
    public void UpdateResourceAvailable()
    {
        resourceAvailableText.text = doodad.availableAmount.ToString();
    }
}