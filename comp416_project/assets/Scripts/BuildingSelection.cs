using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingSelection : MonoBehaviour
{
    public LayerMask buildingLayerMask;

    private Camera cam;
    private Player player;

    void Awake()
    {
        cam = Camera.main;
        player = GetComponent<Player>();
    }

    void Update()
    {
        // checks wether left mouse button is down
        if (Input.GetMouseButtonDown(0))
        {
            SelectBuilding(Input.mousePosition);
        }
    }

    // called when a building is clicked
    void SelectBuilding(Vector2 mousePosition)
    {
        Ray ray = cam.ScreenPointToRay(mousePosition);

        // toggles interactable building UI buttons
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, buildingLayerMask))
        {
            if (hit.collider.CompareTag("Building1"))
                player.ActivateButtons(true, 1);

            else if (hit.collider.CompareTag("Building2"))
                player.ActivateButtons(true, 2);

            else if (hit.collider.CompareTag("Building3"))
                player.ActivateButtons(true, 3);

            else if (hit.collider.CompareTag("Building4"))
                player.ActivateButtons(true, 4);

            else if (hit.collider.CompareTag("Building5"))
                player.ActivateButtons(true, 5);

            else if (hit.collider.CompareTag("Building6"))
                player.ActivateButtons(true, 6);

            else if (hit.collider.CompareTag("Ground") && !EventSystem.current.IsPointerOverGameObject())
            {
                player.ActivateButtons(false, 1);
                player.ActivateButtons(false, 2);
                player.ActivateButtons(false, 3);
                player.ActivateButtons(false, 4);
                player.ActivateButtons(false, 5);
            }
        }
    }
}
