using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public GameObject buildingPrefabFood;
    public GameObject buildingPrefab2;
    public GameObject buildingPrefab3;
    public GameObject buildingPrefab4;
    public GameObject buildingPrefab5;

    public GameObject buildingCreateButtonFood;
    public GameObject buildingUpgradeButtonFood;
    public GameObject buildingCreateButton2;
    public GameObject buildingCreateButton3;
    public GameObject buildingCreateButton4;
    public GameObject buildingCreateButton5;

    public GameObject underConstructionPrefabFood;
    public GameObject underConstructionPrefab2;
    public GameObject underConstructionPrefab3;
    public GameObject underConstructionPrefab4;
    public GameObject underConstructionPrefab5;

    public GameObject unitPrefab1;
    public GameObject unitPrefab2;
    public GameObject unitPrefab3;
    public GameObject unitPrefab4;
    public GameObject unitPrefab5;

    public GameObject unitCreateButton1;
    public GameObject unitCreateButton2;
    public GameObject unitCreateButton3;
    public GameObject unitCreateButton4;
    public GameObject unitCreateButton5;

    public Transform buildingSpawnPositionFood;
    public Transform buildingSpawnPosition2;
    public Transform buildingSpawnPosition3;
    public Transform buildingSpawnPosition4;
    public Transform buildingSpawnPosition5;

    public Transform unitSpawnPosition1;
    public Transform unitSpawnPosition2;
    public Transform unitSpawnPosition3;
    public Transform unitSpawnPosition4;
    public Transform unitSpawnPosition5;

    public int food;
    public int wood;
    public int stone;
    public int gold;

    public readonly int unitCost1 = 10;
    public readonly int unitCost2 = 30;
    public readonly int unitCost3 = 50;
    public readonly int unitCost4 = 70;
    public readonly int unitCost5f = 50;
    public readonly int unitCost5g = 100;

    public readonly int buildingCostFood = 5;
    public readonly int buildingCost2 = 10;
    public readonly int buildingCost3 = 100;
    public readonly int buildingCost4w = 200;
    public readonly int buildingCost4s = 200;
    public readonly int buildingCost5w = 500;
    public readonly int buildingCost5s = 500;
    public readonly int buildingCostFoodUpgrade = 50;

    private float interval;

    public List<Unit> units = new List<Unit>();

    [System.Serializable]
    public class UnitCreatedEvent : UnityEvent<Unit> { }
    public UnitCreatedEvent unitCreatedEvent;

    void Start()
    {
        UIPlayer.instance.UpdateUnitCountText(units.Count);

        UIPlayer.instance.UpdateFoodText(food);
        UIPlayer.instance.UpdateWoodText(wood);
        UIPlayer.instance.UpdateStoneText(stone);
        UIPlayer.instance.UpdateGoldText(gold);

        unitCreateButton1.SetActive(false);

        buildingCreateButtonFood.SetActive(true);
        buildingUpgradeButtonFood.SetActive(false);

        buildingCreateButton2.SetActive(true);
        unitCreateButton2.SetActive(false);

        buildingCreateButton3.SetActive(true);
        unitCreateButton3.SetActive(false);

        buildingCreateButton4.SetActive(true);
        unitCreateButton4.SetActive(false);

        buildingCreateButton5.SetActive(true);
        unitCreateButton5.SetActive(false);

        interval = 5;

        food += unitCost1;

        CreateNewUnit1();
    }

    void Update()
    {
        UIPlayer.instance.UpdateUnitCountText(units.Count);
        PlayerLoseCondition();
    }

    // player creates new unit to collect food
    public void CreateNewUnit1()
    {
        if (food - unitCost1 < 0)
            return;

        food -= unitCost1;
        UIPlayer.instance.UpdateFoodText(food);

        GameObject gameObject = Instantiate(unitPrefab1, unitSpawnPosition1.position, unitSpawnPosition1.transform.rotation, transform);
        Unit unit = gameObject.GetComponent<Unit>();

        units.Add(unit);
        unit.player = this;

        if (unitCreatedEvent != null)
            unitCreatedEvent.Invoke(unit);

        //UIPlayer.instance.UpdateUnitCountText(units.Count);
    }

    // player creates unit that collects wood
    public void CreateNewUnit2()
    {
        if (food - unitCost2 < 0)
            return;

        food -= unitCost2;
        UIPlayer.instance.UpdateFoodText(food);

        GameObject gameObject = Instantiate(unitPrefab2, unitSpawnPosition2.position, unitSpawnPosition2.transform.rotation, transform);
        Unit unit = gameObject.GetComponent<Unit>();

        units.Add(unit);
        unit.player = this;

        if (unitCreatedEvent != null)
            unitCreatedEvent.Invoke(unit);

        UIPlayer.instance.UpdateUnitCountText(units.Count);
    }

    // player creates unit that collects stone
    public void CreateNewUnit3()
    {
        if (food - unitCost3 < 0)
            return;

        food -= unitCost3;
        UIPlayer.instance.UpdateFoodText(food);

        GameObject gameObject = Instantiate(unitPrefab3, unitSpawnPosition3.position, unitSpawnPosition3.transform.rotation, transform);
        Unit unit = gameObject.GetComponent<Unit>();

        units.Add(unit);
        unit.player = this;

        if (unitCreatedEvent != null)
            unitCreatedEvent.Invoke(unit);

        UIPlayer.instance.UpdateUnitCountText(units.Count);
    }

    // player creates unit that collects gold
    public void CreateNewUnit4()
    {
        if (food - unitCost4 < 0)
            return;

        food -= unitCost4;
        UIPlayer.instance.UpdateFoodText(food);

        GameObject gameObject = Instantiate(unitPrefab4, unitSpawnPosition4.position, unitSpawnPosition4.transform.rotation, transform);
        Unit unit = gameObject.GetComponent<Unit>();

        units.Add(unit);
        unit.player = this;

        if (unitCreatedEvent != null)
            unitCreatedEvent.Invoke(unit);

        UIPlayer.instance.UpdateUnitCountText(units.Count);
    }

    // player creates unit that can fight enemies
    public void CreateNewUnit5()
    {
        if (food - unitCost5f < 0 || gold - unitCost5g < 0)
            return;

        food -= unitCost5f;
        UIPlayer.instance.UpdateFoodText(food);
        gold -= unitCost5g;
        UIPlayer.instance.UpdateGoldText(gold);

        GameObject gameObject = Instantiate(unitPrefab5, unitSpawnPosition5.position, unitSpawnPosition5.transform.rotation, transform);
        Unit unit = gameObject.GetComponent<Unit>();

        units.Add(unit);
        unit.player = this;

        if (unitCreatedEvent != null)
            unitCreatedEvent.Invoke(unit);

        UIPlayer.instance.UpdateUnitCountText(units.Count);
    }

    // player creates building that produces food over a time interval
    public void CreateBuildingFood()
    {
        if (wood - buildingCostFood < 0)
            return;

        buildingCreateButtonFood.SetActive(false);

        wood -= buildingCostFood;
        UIPlayer.instance.UpdateWoodText(wood);

        GameObject gameObject = Instantiate(underConstructionPrefabFood, buildingSpawnPositionFood.position, buildingSpawnPositionFood.transform.rotation, transform);
    }

    // player creates building that spawns units for wood
    public void CreateBuilding2()
    {
        if (wood - buildingCost2 < 0)
            return;

        buildingCreateButton2.SetActive(false);

        wood -= buildingCost2;
        UIPlayer.instance.UpdateWoodText(wood);

        GameObject gameObject = Instantiate(underConstructionPrefab2, buildingSpawnPosition2.position, buildingSpawnPosition2.transform.rotation, transform);
    }

    // player creates building that spawns units for stone
    public void CreateBuilding3()
    {
        if (wood - buildingCost3 < 0)
            return;

        buildingCreateButton3.SetActive(false);

        wood -= buildingCost3;
        UIPlayer.instance.UpdateWoodText(wood);

        GameObject gameObject = Instantiate(underConstructionPrefab3, buildingSpawnPosition3.position, buildingSpawnPosition3.transform.rotation, transform);
    }

    // player creates building that spawns units for gold
    public void CreateBuilding4()
    {
        if (wood - buildingCost4w < 0 || stone - buildingCost4s < 0)
            return;

        buildingCreateButton4.SetActive(false);

        wood -= buildingCost4w;
        UIPlayer.instance.UpdateWoodText(wood);
        stone -= buildingCost4s;
        UIPlayer.instance.UpdateStoneText(stone);

        GameObject gameObject = Instantiate(underConstructionPrefab4, buildingSpawnPosition4.position, buildingSpawnPosition4.transform.rotation, transform);
    }

    // player creates building that spawns units who can fight enemies
    public void CreateBuilding5()
    {
        if (wood - buildingCost5w < 0 || stone - buildingCost5s < 0)
            return;

        buildingCreateButton5.SetActive(false);

        wood -= buildingCost5w;
        UIPlayer.instance.UpdateWoodText(wood);
        stone -= buildingCost5s;
        UIPlayer.instance.UpdateStoneText(stone);

        GameObject gameObject = Instantiate(underConstructionPrefab5, buildingSpawnPosition5.position, buildingSpawnPosition5.transform.rotation, transform);
    }

    // player upgrades food building
    public void UpgradeFoodBuilding()
    {
        if (wood - buildingCostFoodUpgrade < 0)
            return;

        buildingUpgradeButtonFood.SetActive(false);

        interval = 1;
        CancelInvoke("ProduceFood");
        InvokeRepeating("ProduceFood", 0, interval);

        wood -= buildingCostFood;
        UIPlayer.instance.UpdateWoodText(wood);
    }

    // called when a resource is gathered for the player
    public void GetResource(ResourceType type, int amount)
    {
        switch (type)
        {
            case ResourceType.Food:
                {
                    food += amount;
                    UIPlayer.instance.UpdateFoodText(food);
                    break;
                }
            case ResourceType.Wood:
                {
                    wood += amount;
                    UIPlayer.instance.UpdateWoodText(wood);
                    break;
                }
            case ResourceType.Stone:
                {
                    stone += amount;
                    UIPlayer.instance.UpdateStoneText(stone);
                    break;
                }
            case ResourceType.Gold:
                {
                    gold += amount;
                    UIPlayer.instance.UpdateGoldText(gold);
                    break;
                }
        }
    }

    // called when a building has finished its construction
    public void FinishConstruction(BuildingResourceType type)
    {
        switch (type)
        {
            case BuildingResourceType.Food:
                {
                    InvokeRepeating("ProduceFood", 0, interval);
                    GameObject gameObject = Instantiate(buildingPrefabFood, buildingSpawnPositionFood.position, buildingSpawnPositionFood.transform.rotation, transform);
                    break;
                }
            case BuildingResourceType.Wood:
                {
                    GameObject gameObject = Instantiate(buildingPrefab2, buildingSpawnPosition2.position, buildingSpawnPosition2.transform.rotation, transform);
                    break;
                }
            case BuildingResourceType.Stone:
                {
                    GameObject gameObject = Instantiate(buildingPrefab3, buildingSpawnPosition3.position, buildingSpawnPosition3.transform.rotation, transform);
                    break;
                }
            case BuildingResourceType.Gold:
                {
                    GameObject gameObject = Instantiate(buildingPrefab4, buildingSpawnPosition4.position, buildingSpawnPosition4.transform.rotation, transform);
                    break;
                }
            case BuildingResourceType.Enemies:
                {
                    GameObject gameObject = Instantiate(buildingPrefab5, buildingSpawnPosition5.position, buildingSpawnPosition5.transform.rotation, transform);
                    break;
                }
        }
    }

    // called whenever a building is selected 
    public void ActivateButtons(bool state, int type)
    {
        unitCreateButton1.SetActive(false);
        unitCreateButton2.SetActive(false);
        unitCreateButton3.SetActive(false);
        unitCreateButton4.SetActive(false);
        unitCreateButton5.SetActive(false);
        buildingUpgradeButtonFood.SetActive(false);

        if (type == 1)
            unitCreateButton1.SetActive(state);
        else if (type == 2)
            unitCreateButton2.SetActive(state);
        else if (type == 3)
            unitCreateButton3.SetActive(state);
        else if (type == 4)
            unitCreateButton4.SetActive(state);
        else if (type == 5)
            unitCreateButton5.SetActive(state);
        else if (type == 6)
            buildingUpgradeButtonFood.SetActive(state);
    }

    // called every time interval once the food building is built
    public void ProduceFood()
    {
        food++;
        UIPlayer.instance.UpdateFoodText(food);
    }

    // returns wether a unit belongs to player for the selection
    public bool IsPlayerUnit(Unit unit)
    {
        return units.Contains(unit);
    }

    // checkes the lose condition for the player
    public void PlayerLoseCondition()
    {
        if (units.Count == 0)
            UIEndGame.instance.EndGame(false);
    }
}