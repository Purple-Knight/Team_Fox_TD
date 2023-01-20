using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacementManager : MonoBehaviour
{
    [System.Serializable]
    public struct TurretsPrefabs
    {
        public string turretType;
        public GameObject turretPrefab;
    }

    private GridHandler grid;
    [SerializeField] private List<TurretsPrefabs> allTurrets;
    private Vector3 clickedGridPostion;
    private bool alreadyClicked = false;

    [SerializeField] private GameObject turretWheel;
    [SerializeField] private GameObject upgradeWindow;

    private void Start()
    {
        grid = GetComponent<GridHandler>();
    }

    private void Update()
    {
        if (!alreadyClicked && Input.GetMouseButtonDown(0))
        {
            OnMouseClick();
        }
    }
    public void OnMouseClick()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        clickedGridPostion = mousePos;

        switch (grid.CheckSpaceStatus(mousePos))
        {
            case GridHandler.CellType.Blocked:
                //do nothing
                break;
            case GridHandler.CellType.Free:
                //show wheel
                turretWheel.transform.position = mousePos;
                turretWheel.SetActive(true);
                alreadyClicked = true;

                break;
            case GridHandler.CellType.Occupied:
                alreadyClicked = true;

                //show upgrade cost
                //raycast to find gameobject
                break;
            default:
                break;
        }
    }

    public void SpawnTuret(string turretType)
    {
        Vector3 pos = grid.GetGridSnapPosition(clickedGridPostion);

        for (int i = 0; i < allTurrets.Count; i++)
        {
            if( allTurrets[i].turretType == turretType)
            {
                Instantiate(allTurrets[i].turretPrefab, pos, Quaternion.identity);
                break;
            }
        }

        alreadyClicked = false;
        turretWheel.SetActive(false);
    }
}
