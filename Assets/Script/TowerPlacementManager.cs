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
    private BaseTower currentTurret;

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

                break;
            case GridHandler.CellType.Free:
                turretWheel.transform.position = mousePos;
                turretWheel.SetActive(true);
                alreadyClicked = true;

                break;
            case GridHandler.CellType.Occupied:
                alreadyClicked = true;
                upgradeWindow.transform.position = mousePos;
                upgradeWindow.SetActive(true);
                RaycastHit2D hit = Physics2D.CircleCast(mousePos, .1f, Vector2.right);
                currentTurret = hit.collider.GetComponent<BaseTower>();
                Debug.Log(currentTurret.gameObject.name);

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

    public void UpgradeTower()
    {
        if (currentTurret.LevelUp()) 
        {
            Debug.Log("upgraded");
        }
        //remove money

        alreadyClicked = false;
        upgradeWindow.SetActive(false);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(currentTurret.transform.position, 0.1f);
    }
}
