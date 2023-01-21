using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedTower : BaseTower
{
    [Header("SpeedTower")]
    [SerializeField] private List<float> multiplicatorBoostUpgrade;
    private float multiplicatorBoost;
    
    
    protected override void OnTargetEnter(Collider2D other)
    {
        if ((targetType == TargetType.TowerBoost && other.tag == "Tower"))
        {
            other.transform.GetComponent<BaseTower>().ChangeAttackSpeedMultiplicator(multiplicatorBoost);
        }

    }

    protected override void OnTargetExit(Collider2D other)
    {
        if (other.tag == "Tower")
        {
            other.transform.parent.GetComponent<BaseTower>().ChangeAttackSpeedMultiplicator(0f);
        }

    }

    protected override void LevelUpStats(int level)
    {
        base.LevelUpStats(level);
        if (multiplicatorBoostUpgrade[level] > multiplicatorBoost)
            multiplicatorBoost = multiplicatorBoostUpgrade[level];
    }
}
