using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridHandler : MonoBehaviour
{
    [Serializable]
    public enum CellType { Blocked, Free, Occupied }

    public Tilemap map;

    private Dictionary<Vector3, bool> gridSpaceOccupied;
    [SerializeField] private Vector3 tileOffset;

    void Start()
    {
        gridSpaceOccupied = new Dictionary<Vector3, bool>();
    }

    public CellType CheckSpaceStatus(Vector2 mousePos)
    {
        var gridPos = map.WorldToCell(mousePos);

        if (!map.HasTile(gridPos) || map.GetSprite(gridPos).name != "grass")
            return CellType.Blocked;

        if (gridSpaceOccupied.ContainsKey(gridPos))
            return CellType.Occupied;

        return CellType.Free;
    }

    public Vector3 GetGridSnapPosition(Vector2 mousePos)
    {
        var gridPos = map.WorldToCell(mousePos);
        Vector3 pos = map.CellToWorld(gridPos);

        gridSpaceOccupied.Add(pos, true);
        return pos + tileOffset;
    }
}
