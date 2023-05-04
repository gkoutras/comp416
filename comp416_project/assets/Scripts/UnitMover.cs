using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMover : MonoBehaviour
{
    // returns an array of positions spaced as a formation on a given ground destination
    public static Vector3[] GetUnitGroupDestinations(Vector3 pos, int unitsTotal, float unitGap)
    {
        // initiates an array for the returned destinations
        Vector3[] destinations = new Vector3[unitsTotal];

        // calculates the rows and columns
        int rows = Mathf.RoundToInt(Mathf.Sqrt(unitsTotal));
        int columns = Mathf.CeilToInt((float)unitsTotal / (float)rows);

        int currentRow = 0;
        int currentColumn = 0;

        float width = ((float)rows - 1) * unitGap;
        float length = ((float)columns - 1) * unitGap;

        for(int x = 0; x < unitsTotal; x++)
        {
            destinations[x] = pos + (new Vector3(currentRow, 0, currentColumn) * unitGap) - new Vector3(length / 2, 0, width / 2);
            currentColumn++;

            if(currentColumn == rows)
            {
                currentColumn = 0;
                currentRow++;
            }
        }

        return destinations;
    }

    // returns an array of positions spaced evenly around a give object destination
    public static Vector3[] GetUnitGroupDestinationsAroundObject(Vector3 pos, int unitsTotal)
    {
        // initiates an array for the returned destinations
        Vector3[] destinations = new Vector3[unitsTotal];

        // calculates the distance between units placed around a object
        float unitGap = 360.0f / (float)unitsTotal;

        for(int x = 0; x < unitsTotal; x++)
        {
            float angle = unitGap * x;
            Vector3 dir = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
            destinations[x] = pos + dir;
        }

        return destinations;
    }

    // returns a single position around a object
    public static Vector3 GetUnitDestinationAroundObject(Vector3 pos)
    {
        float angle = Random.Range(0, 360);
        Vector3 dir = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));

        return pos + dir;
    }
}