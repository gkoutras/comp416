using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public List<Unit> enemies = new List<Unit>();

    void Update()
    {
        EnemyLoseCondition();
    }

    // checkes the lose condition for the enemy
    public void EnemyLoseCondition()
    {
        if (enemies.Count == 0)
            UIEndGame.instance.EndGame(true);
    }
}
