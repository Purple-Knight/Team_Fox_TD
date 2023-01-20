using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    [SerializeField] protected float radius;
    protected CircleCollider2D rangeCollider2D;
    protected SpriteRenderer sr;
    
    [Header("Targets")]
    [SerializeField] protected TargetType targetType;
    protected int numberOfTargetMax; // Set 0 if infinite
    [SerializeField] protected List<GameObject> TargetsList = new List<GameObject>();
    
    [Header("CoolDown")]
    private float coolDownBtwShot;
    protected float actualCoolDownBtwShot;
    protected float cooldownMultiplicator;
    
    [Header("Attack")]
    private int attackPower;
    [SerializeField] private GameObject projectile;

    [Header("CommonUpgrades")] 
    private int currentLevel = 0;
    [SerializeField] protected  int levelMax;
    [SerializeField] protected List<float> radiusUpdrades = new List<float>();
    [SerializeField] protected List<int> numberOfTargetMaxUpdrades = new List<int>();
    [SerializeField] private List<float> coolDownBtwShotUpdrades = new List<float>();
    [SerializeField] private List<int> attackPowerUpdrades = new List<int>();
    [SerializeField] private List<Sprite> spriteUpdrades = new List<Sprite>();
    
    protected enum TargetType
    {
        Attack,
        TowerBoost,
    }

    protected virtual void Start()
    {
        rangeCollider2D = gameObject.AddComponent<CircleCollider2D>();
        rangeCollider2D.offset = Vector2.zero;
        rangeCollider2D.isTrigger = true;
        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        
        radius = 0;
        LevelUpStats(0);
        
        rangeCollider2D.radius = radius;
    }

    private void Update()
    {
        if (actualCoolDownBtwShot <= 0)
        {
            if(TargetsList.Count == 0) return;
            Shoot();
        }
        else actualCoolDownBtwShot -= Time.deltaTime * (1 + cooldownMultiplicator);
    }

    protected List<GameObject> SetTarget()
    {
        List<GameObject> list = new List<GameObject>();
        switch (targetType)
        {
            case TargetType.Attack :
                for (int i = 0; i < TargetsList.Count;)
                {
                    if (TargetsList[i] == null)
                    {
                        TargetsList.RemoveAt(i);
                        if (TargetsList.Count == 0) break;
                        continue;
                    }

                    list.Add(TargetsList[i]);
                    i++;
                    if (numberOfTargetMax > 0 && numberOfTargetMax >= i) break;
                }
                break;
            
            case TargetType.TowerBoost :
                break;
        }

        return list;
    }

    protected virtual void Shoot()
    {
        List<GameObject> list = SetTarget();

        if(list.Count == 0) return;
        
        for (int i = 0; i < list.Count; i++)
        {
            var projo = Instantiate(projectile, transform.position, transform.rotation);
            projo.GetComponent<Projectile>().SetTarget(list[i]);
            projo.GetComponent<Projectile>().SetAttackPower(attackPower);
        }
        
        actualCoolDownBtwShot = coolDownBtwShot;
    }

    public void LevelUp()
    {
        if (currentLevel < levelMax)
        {
            currentLevel++;
            LevelUpStats(currentLevel);
        }
    }
    protected virtual void LevelUpStats(int level)
    {
        radius = radiusUpdrades[level];
        numberOfTargetMax = numberOfTargetMaxUpdrades[level];
        coolDownBtwShot = coolDownBtwShotUpdrades[level];
        attackPower = attackPowerUpdrades[level];
        sr.sprite = spriteUpdrades[level];
    }
    
    public void ChangeAttackSpeedMultiplicator(float leFloat)
    {
        cooldownMultiplicator = leFloat;
    }

    protected virtual void OnTargetEnter(Collider2D other)
    {
        if ((targetType == TargetType.Attack && other.tag == "Enemy"))
        {
            TargetsList.Add(other.gameObject);
        }

    }

    protected virtual void OnTargetExit(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            TargetsList.Remove(other.gameObject);
            ReorderList();
        }

    }

    private void ReorderList()
    {
        for (int i = 0; i < TargetsList.Count; i++)
        {
            int id = i; 
            float pathValue = TargetsList[i].GetComponent<EnemyController>().GetPathProgress;
            for (int j = i + 1; j < TargetsList.Count; j++)
            {
                float myPorgress = TargetsList[j].GetComponent<EnemyController>().GetPathProgress;
                if (myPorgress > pathValue)
                {
                    pathValue = myPorgress;
                    id = j;
                }
            }

            if (id != i)
            {
                GameObject item1 = TargetsList[i];
                TargetsList[i] = TargetsList[id];
                TargetsList[id] = item1;
            }
        }
    }




    private void OnTriggerEnter2D(Collider2D other)
    {
        OnTargetEnter(other);

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        OnTargetExit(other);
    }
    
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
