using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnemyLevels : MonoBehaviour
{
    public GameObject[] templateObjects;
    public GameObject enemyPrefab;

    void Start()
    {
        int rand = Random.Range(0, templateObjects.Length);
        Instantiate(enemyPrefab, templateObjects[rand].transform.position, templateObjects[rand].transform.rotation);
    }
}
