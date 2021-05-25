using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways][RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColour = Color.white, blockedColour = Color.gray, exploredColour = Color.yellow, pathColour = new Color(1f,0f,0.5f);

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        DisplayCoordinates();
        SetLabelColour();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
            label.enabled = true;
        }
        SetLabelColour();
        ToggleCoordinates();
    }

    void ToggleCoordinates()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }

    void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.position.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.position.z / gridManager.UnityGridSize);

        label.text = $"({coordinates.x},{coordinates.y})";
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }

    void SetLabelColour()
    {
        if(gridManager == null) { return; }

        Node node = gridManager.GetNode(coordinates);
        if (node == null) { return; }

        if (!node.isWalkable)
        {
            label.color = blockedColour;
        }
        else if (node.isPath)
        {
            label.color = pathColour;
        }
        else if (node.isExplored)
        {
            label.color = exploredColour;
        }
        else
        {
            label.color = defaultColour;
        }
    }
}
