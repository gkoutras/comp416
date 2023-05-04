using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ResourceType
{
    Food,
    Wood,
    Stone,
    Gold
}

public class DoodadManager : MonoBehaviour
{
    public Unit unit;
    public UnityEvent valueChangeEvent;

    public int availableAmount;

    public ResourceType type;

    // set the gathering resource rate for the different units
    public void GatherResourceRate(Unit unit)
    {
        unit.SetGatherRate(type);
    }

    // called when a unit gathers a resource from a doodad
    public void GatherResourceFromDoodad(int amount, Player player)
    {
        availableAmount -= amount;

        int gainedAmount = amount;

        // checks available resource amount
        if (availableAmount < 0)
            gainedAmount = amount + availableAmount;

        player.GetResource(type, gainedAmount);

        // removes a depleted doodad
        if (availableAmount <= 0)
            Destroy(gameObject);

        if (valueChangeEvent != null)
            valueChangeEvent.Invoke();
    }
}