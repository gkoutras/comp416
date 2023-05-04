using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float moveSpeed = 20f;
    public Vector2 moveLimit;

    public float scrollSpeed = 10f;
    public float minY = 15f;
    public float maxY = 50f;

    void Update()
    {
        Vector3 position = transform.position;
        // moves camera position forward
        if (Input.GetKey("w"))
        {
            position.z += moveSpeed * Time.deltaTime;
        }
        // moves camera position back
        if (Input.GetKey("s"))
        {
            position.z -= moveSpeed * Time.deltaTime;
        }
        // moves camera position right
        if (Input.GetKey("d"))
        {
            position.x += moveSpeed * Time.deltaTime;
        }
        // moves camera position left
        if (Input.GetKey("a"))
        {
            position.x -= moveSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        position.y -= scroll * scrollSpeed * 100f * Time.deltaTime;

        // computes the new camera position upon zooming in and out;
        position.x = Mathf.Clamp(position.x, -moveLimit.x, moveLimit.x);
        position.y = Mathf.Clamp(position.y, minY, maxY);
        position.z = Mathf.Clamp(position.z, -moveLimit.y, moveLimit.y);

        transform.position = position;
    }
}
