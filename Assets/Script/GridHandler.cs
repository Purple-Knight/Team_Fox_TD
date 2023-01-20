using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridHandler : MonoBehaviour
{
    [Serializable]
    public enum CellType { Blocked, Free, Occupied }

    public Tilemap map;

    public GameObject towerPrefab;

    private Vector3Int selectedTilePosition;

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

        selectedTilePosition = gridPos;

        Instantiate(towerPrefab, map.CellToWorld(selectedTilePosition), Quaternion.identity);
    }
}
