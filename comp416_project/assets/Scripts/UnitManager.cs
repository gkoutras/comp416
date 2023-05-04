using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public LayerMask layerMask;

    private Camera cam;
    private UnitSelection unitSelection;

    void Awake()
    {
        cam = Camera.main;
        unitSelection = GetComponent<UnitSelection>();
    }

    void Update()
    {
        // checks wether right mouse button is pressed and if units are selected
        if (Input.GetMouseButtonDown(1) && unitSelection.MultipleUnitsSelected())
        {
            // shoots a raycast from mouse to see what object is hit
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // caches the selected units in an array
            Unit[] selectedUnits = unitSelection.GetSelectedUnits();

            // shoots the raycast
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                unitSelection.RemoveDeadUnitsFromSelection();

                // checks wether the ground is clicked
                if (hit.collider.CompareTag("Ground"))
                {
                    TryMoveToPosition(hit.point, selectedUnits);
                }
                // checks wether a doodad is clicked
                else if (hit.collider.CompareTag("Doodad"))
                {
                    TryGatherResource(hit.collider.GetComponent<DoodadManager>(), selectedUnits);
                }
                // checks wether an under-construction building is clicked
                else if (hit.collider.CompareTag("UC"))
                {
                    TryConstruct(hit.collider.GetComponent<BuildingUCManager>(), selectedUnits);
                }
                // checks wether an enemy is clicked
                else if (hit.collider.CompareTag("Enemy"))
                {
                    TryAttackEnemy(hit.collider.gameObject.GetComponent<Unit>(), selectedUnits);
                }
            }
        }
    }

    // called when units are commanded to move somewhere in the ground
    void TryMoveToPosition(Vector3 movePosition, Unit[] units)
    {
        Vector3[] destinations = UnitMover.GetUnitGroupDestinations(movePosition, units.Length, 2);

        for(int x = 0; x < units.Length; x++)
        {
            units[x].MoveToPosition(destinations[x]);
        }
    }

    // called when units are commanded to gather a resource from a doodad
    void TryGatherResource(DoodadManager doodad, Unit[] units)
    {
        // checks wether one unit is selected
        if (units.Length == 1)
        {
            units[0].GatherResource(doodad, UnitMover.GetUnitDestinationAroundObject(doodad.transform.position));
        }
        // checks wether more than one units are selected
        else
        {
            Vector3[] destinations = UnitMover.GetUnitGroupDestinationsAroundObject(doodad.transform.position, units.Length);

            for(int x = 0; x < units.Length; x++)
            {
                units[x].GatherResource(doodad, destinations[x]);
            }
        }
    }

    // called when units are commanded to construct a building
    void TryConstruct(BuildingUCManager building, Unit[] units)
    {
        // checks wether one unit is selected
        if (units.Length == 1)
        {
            units[0].Construct(building, UnitMover.GetUnitDestinationAroundObject(building.transform.position));
        }
        // checks wether more than one units are selected
        else
        {
            Vector3[] destinations = UnitMover.GetUnitGroupDestinationsAroundObject(building.transform.position, units.Length);

            for (int x = 0; x < units.Length; x++)
            {
                units[x].Construct(building, destinations[x]);
            }
        }
    }

    void TryAttackEnemy(Unit target, Unit[] units)
    {
        for (int x = 0; x < units.Length; x++)
        {
            units[x].AttackEnemy(target);
        }
    }
}