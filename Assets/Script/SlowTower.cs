using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTower : BaseTower
{
    [Header("SlowTower")]
    [SerializeField] private List<float> multiplicatorSlowUpgrade;
    private float multiplicatorSlow;
    
    protected override void OnTargetEnter(Collider2D other)
    {
        if ((targetType == TargetType.Attack && other.tag == "Enemy"))
        {
            other.transform.GetComponent<EnemyController>().ChangeSpeedMultiplicator(multiplicatorSlow);
        }

    }

    protected override void OnTargetExit(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.transform.GetComponent<EnemyController>().ChangeSpeedMultiplicator(0f);
        }

    }

    protected override void LevelUp(int level)
    {
        base.LevelUp(level);
        if (multiplicatorSlowUpgrade[level] > multiplicatorSlow)
            multiplicatorSlow = multiplicatorSlowUpgrade[level];
    }
}
