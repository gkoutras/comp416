using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using System;

public enum UnitState
{
    Idle,
    Move,
    MoveToDoodad,
    GatherResource,
    MoveToBuilding,
    Construct,
    MoveToEnemy,
    Attack
}

public class Unit : MonoBehaviour
{
    public Player player;
    public Enemy enemy;
    public UIUnitHealthBar healthBar;
    public GameObject selectionVisual;

    private NavMeshAgent navAgent;
    private DoodadManager doodad;
    private BuildingUCManager building;

    private Unit currentEnemyTarget;

    public bool isPlayerUnit;

    public int gatherAmount;

    public float gatherRateFood;
    public float gatherRateWood;
    public float gatherRateStone;
    public float gatherRateGold;

    public int constructAmount;

    private float constructRate = 1f;
    private float lastConstructTime;

    public int maxHP;
    public int currentHP;
    public int attackDamage;

    public float attackRate;
    public float attackDistance;

    private float gatherRate;
    private float lastGatherTime;
    private float lastAttackTime;

    private float pathUpdateRate = 1f;
    private float lastPathUpdateTime;

    [System.Serializable]
    public class StateChangeEvent : UnityEvent<UnitState> { }
    public StateChangeEvent stateChangeEvent;

    public UnitState state;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();

        SetState(UnitState.Idle);
    }

    void SetState(UnitState unitState)
    {
        state = unitState;

        if (stateChangeEvent != null)
            stateChangeEvent.Invoke(state);

        if(unitState == UnitState.Idle)
        {
            navAgent.isStopped = true;
            navAgent.ResetPath();
        }
    }

    void Update()
    {
        switch(state)
        {
            case UnitState.Move:
                {
                    MoveUpdate();
                    break;
                }
            case UnitState.MoveToDoodad:
                {
                    MoveToDoodadUpdate();
                    break;
                }
            case UnitState.GatherResource:
                {
                    GatherResourceUpdate();
                    break;
                }
            case UnitState.MoveToBuilding:
                {
                    MoveToBuildingUpdate();
                    break;
                }
            case UnitState.Construct:
                {
                    ConstructUpdate();
                    break;
                }
            case UnitState.MoveToEnemy:
                {
                    MoveToEnemyUpdate();
                    break;
                }
            case UnitState.Attack:
                {
                    AttackUpdate();
                    break;
                }
        }
        
    }

    // called every frame the Move state is active
    void MoveUpdate()
    {
        if(Vector3.Distance(transform.position, navAgent.destination) == 0.0f)
            SetState(UnitState.Idle);
    }

    // called every frame the MoveToDoodad state is active
    void MoveToDoodadUpdate()
    {
        if (doodad == null)
        {
            SetState(UnitState.Idle);
            return;
        }

        if (Vector3.Distance(transform.position, navAgent.destination) == 0.0f)
            SetState(UnitState.GatherResource);
    }

    // called every frame the GatherResource state is active
    void GatherResourceUpdate()
    {
        if (doodad == null)
        {
            SetState(UnitState.Idle);
            return;
        }

        FacePosition(doodad.transform.position);

        doodad.GatherResourceRate(this);

        if (Time.time - lastGatherTime > gatherRate)
        {
            lastGatherTime = Time.time;
            doodad.GatherResourceFromDoodad(gatherAmount, player);
        }
    }

    // called every frame the MoveToBuilding state is active
    void MoveToBuildingUpdate()
    {
        if (building == null)
        {
            SetState(UnitState.Idle);
            return;
        }

        if (Vector3.Distance(transform.position, navAgent.destination) == 0.0f)
            SetState(UnitState.Construct);
    }

    // called every frame the Construct state is active
    void ConstructUpdate()
    {
        if (building == null)
        {
            SetState(UnitState.Idle);
            return;
        }

        FacePosition(building.transform.position);

        if (Time.time - lastConstructTime > constructRate)
        {
            lastConstructTime = Time.time;
            building.ConstructBuilding(constructAmount, player);
        }
    }

    // called every frame the MoveToEnemy state is active
    void MoveToEnemyUpdate()
    {
        if (currentEnemyTarget == null)
        {
            SetState(UnitState.Idle);
            return;
        }

        if (Time.time - lastPathUpdateTime > pathUpdateRate)
        {
            lastPathUpdateTime = Time.time;

            navAgent.isStopped = false;
            navAgent.SetDestination(currentEnemyTarget.transform.position);
        }

        if (Vector3.Distance(transform.position, currentEnemyTarget.transform.position) <= attackDistance)
            SetState(UnitState.Attack);
    }

    //  called every frame the Attack state is active
    void AttackUpdate()
    {
        if (currentEnemyTarget == null)
        {
            SetState(UnitState.Idle);
            return;
        }

        if (!navAgent.isStopped)
            navAgent.isStopped = true;

        if(Time.time - lastAttackTime > attackRate)
        {
            lastAttackTime = Time.time;
            
            currentEnemyTarget.TakeDamage(attackDamage);
        }

        FacePosition(currentEnemyTarget.transform.position);

        if (Vector3.Distance(transform.position, currentEnemyTarget.transform.position) > attackDistance)
            SetState(UnitState.MoveToEnemy);
    }

    // moves unit to a specific position on ground
    public void MoveToPosition(Vector3 pos)
    {
        SetState(UnitState.Move);

        navAgent.isStopped = false;
        navAgent.SetDestination(pos);

    }

    // moves unit to a specific doodad and start resource gathering
    public void GatherResource(DoodadManager fromDoodad, Vector3 pos)
    {
        doodad = fromDoodad;

        SetState(UnitState.MoveToDoodad);

        navAgent.isStopped = false;
        navAgent.SetDestination(pos);
    }

    // moves unit to a specific building and start constructing
    public void Construct(BuildingUCManager fromBuilding, Vector3 pos)
    {
        building = fromBuilding;

        SetState(UnitState.MoveToBuilding);

        navAgent.isStopped = false;
        navAgent.SetDestination(pos);
    }

    // moves unit to an enemy and makes them attack the enemy
    public void AttackEnemy(Unit target)
    {
        currentEnemyTarget = target;

        SetState(UnitState.MoveToEnemy);
    }

    // sets the unit resource gathering rate of the targeted doodad
    public void SetGatherRate(ResourceType type)
    {
        switch(type)
        {
            case ResourceType.Food:
                {
                    gatherRate = gatherRateFood;
                    break;
                }
            case ResourceType.Wood:
                {
                    gatherRate = gatherRateWood;
                    break;
                }
            case ResourceType.Stone:
                {
                    gatherRate = gatherRateStone;
                    break;
                }
            case ResourceType.Gold:
                {
                    gatherRate = gatherRateGold;
                    break;
                }
        }
    }

    // rotates unit to face the given position
    void FacePosition(Vector3 pos)
    {
        Vector3 dir = (pos - transform.position).normalized;
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    // toggles the selection visual of a unit
    public void ToggleSelectionVisual(bool selected)
    {
        if (this != null)
            selectionVisual.SetActive(selected);
    }

    // called when a unit takes damage
    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
            GetKilled();

        healthBar.UpdateHealthBar(currentHP, maxHP);
    }

    // called when a unit gets killed
    void GetKilled()
    {
        // checks if the unit killed belongs to the player or if it is an enemy
        if (isPlayerUnit)
            player.units.Remove(this);
        else
            enemy.enemies.Remove(this);

        Destroy(gameObject);       
    }    
}