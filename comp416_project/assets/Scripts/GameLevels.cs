using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevels : MonoBehaviour
{
    public GameObject[] templateObjects;

    void Start()
    {
        int rand = Random.Range(0, templateObjects.Length);
        Instantiate(templateObjects[rand], transform.position, Quaternion.identity);
    }
}
