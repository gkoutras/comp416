using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    public LayerMask unitLayerMask;
    public RectTransform selectionBox;

    private Camera cam;
    private Player player;

    private Vector2 startPosition;

    private List<Unit> selectedUnits = new List<Unit>();

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
            ToggleSelectionVisual(false);

            startPosition = Input.mousePosition;

            selectedUnits = new List<Unit>();
            SelectUnit(Input.mousePosition);
        }

        // checks wether left mouse button is up
        if (Input.GetMouseButtonUp(0))
        {
            ReleaseSelectionBox();
        }
        // checks wether left mouse button is held down
        else if (Input.GetMouseButton(0))
        {
            CreateSelectionBox(Input.mousePosition);
        }
    }

    // removes all null units from the selected list
    public void RemoveDeadUnitsFromSelection()
    {
        for (int x = 0; x < selectedUnits.Count; x++)
        {
            if (selectedUnits[x] == null)
                selectedUnits.RemoveAt(x);
        }   

        //selectionBox.gameObject.SetActive(false);
    }

    // called when a unit is clicked
    void SelectUnit(Vector2 mousePosition)
    {
        Ray ray = cam.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, unitLayerMask))
        {
            Unit unit = hit.collider.GetComponent<Unit>();

            if (player.IsPlayerUnit(unit))
            {
                selectedUnits.Add(unit);
                unit.ToggleSelectionVisual(true);
            }
        }
    }

    // toggles the selection visual of units
    void ToggleSelectionVisual(bool selected)
    {
        foreach (Unit unit in selectedUnits)
        {
            unit.ToggleSelectionVisual(selected);
        }
    }

    // called when a selection box is created
    void CreateSelectionBox(Vector2 mousePosition)
    {
        if (!selectionBox.gameObject.activeInHierarchy)
            selectionBox.gameObject.SetActive(true);

        float width = mousePosition.x - startPosition.x;
        float height = mousePosition.y - startPosition.y;

        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectionBox.anchoredPosition = startPosition + new Vector2(width / 2, height / 2);
    }

    // called when the selection box is releashed
    void ReleaseSelectionBox()
    {
        selectionBox.gameObject.SetActive(false);

        Vector2 min = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
        Vector2 max = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);

        foreach (Unit unit in player.units)
        {
            Vector3 screenPosition = cam.WorldToScreenPoint(unit.transform.position);

            if (screenPosition.x > min.x && screenPosition.x < max.x && screenPosition.y > min.y && screenPosition.y < max.y)
            {
                selectedUnits.Add(unit);
                unit.ToggleSelectionVisual(true);
            }
        }
    }

    // returns whether there are multiple units selected
    public bool MultipleUnitsSelected()
    {
        if (selectedUnits.Count > 0)
            return true;

        return false;
    }

    // returns the selected units
    public Unit[] GetSelectedUnits()
    {
        return selectedUnits.ToArray();
    }
}