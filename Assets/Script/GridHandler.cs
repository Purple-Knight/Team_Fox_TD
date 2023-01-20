using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridHandler : MonoBehaviour
{
    public Tilemap map;

    private Vector2Int selectedTilePosition;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnMouseClick();
    }

    private void OnMouseClick()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var gridPos = map.WorldToCell(mousePos);

        if (!map.HasTile(gridPos))
            return;

        selectedTilePosition.x = gridPos.x;
        selectedTilePosition.y = gridPos.y;

        Debug.Log($"Tile pos {selectedTilePosition}");
    }
}
