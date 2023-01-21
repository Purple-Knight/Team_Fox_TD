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
        public int price;
    }

    private GridHandler grid;
    [SerializeField] private List<TurretsPrefabs> allTurrets;
    private Vector3 clickedGridPostion;
    private bool isWheelOpened = false;
    private BaseTower currentTurret;

    [SerializeField] private GameObject turretWheel;
    [SerializeField] private GameObject upgradeWindow;

    private void Start()
    {
        grid = GetComponent<GridHandler>();
        CloseWheels();
    }

    private void Update()
    {
        if (!isWheelOpened && Input.GetMouseButtonDown(0))
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
                isWheelOpened = true;

                break;
            case GridHandler.CellType.Occupied:
                isWheelOpened = true;
                upgradeWindow.transform.position = mousePos;
                upgradeWindow.SetActive(true);
                RaycastHit2D hit = Physics2D.CircleCast(mousePos, .1f, Vector2.right);
                currentTurret = hit.collider.GetComponent<BaseTower>();
                Debug.Log(currentTurret.gameObject.name);

                break;
        }
    }

    public void SpawnTuret(string turretType)
    {
        for (int i = 0; i < allTurrets.Count; i++)
        {
            if( allTurrets[i].turretType == turretType)
            {
                if (!MoneyManager.Instance.CanBuy(allTurrets[i].price))
                    break;
                MoneyManager.Instance.UpdateCoin(0, allTurrets[i].price);

                Vector3 pos = grid.GetGridSnapPosition(clickedGridPostion);
                Instantiate(allTurrets[i].turretPrefab, pos, Quaternion.identity);
                break;
            }
        }

        CloseWheels();
    }

    public void UpgradeTower()
    {
        if (!MoneyManager.Instance.CanBuy(100))
            return;
        
        if (currentTurret.LevelUp()) 
        {
            MoneyManager.Instance.UpdateCoin(0, 100);
        }

        CloseWheels();
    }

    public void CloseWheels()
    {
        isWheelOpened = false;
        turretWheel.SetActive(false);
        upgradeWindow.SetActive(false);
    }
}
