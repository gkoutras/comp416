using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public LayerMask layerMask;

    private Unit unit;

    public float checkRate = 1f;
    public float attackRange;

    void Awake ()
    {
        unit = GetComponent<Unit>();
    }

    void Start()
    {
        InvokeRepeating("Check", 0f, checkRate);
    }

    void Check()
    {
        if (unit.state != UnitState.MoveToEnemy && unit.state != UnitState.Attack)
        {
            Unit playerUnit = CheckForPlayerUnits();

            if (playerUnit != null)
                unit.AttackEnemy(playerUnit);
       }
    }
    
    // checks nearby units/enemies
    Unit CheckForPlayerUnits()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, attackRange, Vector3.up, layerMask);

        GameObject closestUnit = null;
        float closestUnitDistance = 0f;

        for (int x = 0; x < hits.Length; x++)
        {
            if (hits[x].collider.CompareTag("Unit"))
            {
                if (!closestUnit || Vector3.Distance(transform.position, hits[x].transform.position) < closestUnitDistance)
                {
                    closestUnit = hits[x].collider.gameObject;
                    closestUnitDistance = Vector3.Distance(transform.position, hits[x].transform.position);
                }
            }
        }

        if (closestUnit != null)
            return closestUnit.GetComponent<Unit>();
        else
            return null;
    }
}
