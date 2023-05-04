using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum BuildingResourceType
{
    Food, 
    Wood,
    Stone,
    Gold,
    Enemies
}

public class BuildingUCManager : MonoBehaviour
{
    public Unit unit;
    public UnityEvent valueChangeEvent;

    public int currentPercentage;
    public int constructionMultiplier;

    public BuildingResourceType type;

    // called when a unit gives a construction percentage to a building 
    public void ConstructBuilding(int percentage, Player player)
    {
        currentPercentage += constructionMultiplier * percentage;

        // removes building scaffolds
        if (currentPercentage == 100)
        {
            Destroy(gameObject);
            player.FinishConstruction(type);
        }

        if (valueChangeEvent != null)
            valueChangeEvent.Invoke();
    }
}
