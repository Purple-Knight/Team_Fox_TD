using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedTower : BaseTower
{
    [Header("SpeedTower")]
    [SerializeField] private float multiplicatorBoost;
    protected override void OnTargetEnter(Collider2D other)
    {
        if ((targetType == TargetType.TowerBoost && other.tag == "Tower"))
        {
            other.transform.parent.GetComponent<BaseTower>().ChangeAttackSpeedMultiplicator(multiplicatorBoost);
        }

    }

    protected override void OnTargetExit(Collider2D other)
    {
        if (other.tag == "Tower")
        {
            other.transform.parent.GetComponent<BaseTower>().ChangeAttackSpeedMultiplicator(0f);
        }

    }

}
