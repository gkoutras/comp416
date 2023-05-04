using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUnitHealthBar : MonoBehaviour
{
    public GameObject healthBarBG;
    public RectTransform HP;

    private float maxSize;

    void Awake()
    {
        maxSize = HP.sizeDelta.x;
        healthBarBG.SetActive(false);
    }

    public void UpdateHealthBar(int currentHP, int maxHp)
    {
        healthBarBG.SetActive(true);

        float health = (float)currentHP / (float)maxHp;
        HP.sizeDelta = new Vector2(maxSize * health, HP.sizeDelta.y);
    }
}
