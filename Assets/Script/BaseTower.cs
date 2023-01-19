using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    [SerializeField] private float radius;
    private CircleCollider2D rangeCollider2D;
    
    [SerializeField] private TargetType targetType;
    [SerializeField] private int numberOfTargetMax; // Set 0 if infinite
    [SerializeField] private List<GameObject> enemisList = new List<GameObject>();
    
    [SerializeField] private float coolDownBtwShot;
    private float actualCoolDownBtwShot;
    
    [SerializeField] private int attackPower;
    [SerializeField] private GameObject projectile;
    
    
    private enum TargetType
    {
        Attack,
        TowerBoost,
    }

    protected void Start()
    {
        rangeCollider2D = gameObject.AddComponent<CircleCollider2D>();
        rangeCollider2D.radius = radius;
        rangeCollider2D.offset = Vector2.zero;
        rangeCollider2D.isTrigger = true;
    }

    private void Update()
    {
        if (actualCoolDownBtwShot <= 0)
        {
            if(enemisList.Count == 0) return;
            Shoot();
        }
        else actualCoolDownBtwShot -= Time.deltaTime;
    }

    protected List<GameObject> SetTarget()
    {
        List<GameObject> list = new List<GameObject>();
        switch (targetType)
        {
            case TargetType.Attack :
                for (int i = 0; i < enemisList.Count;)
                {
                    if (enemisList[i] == null)
                    {
                        enemisList.RemoveAt(i);
                        if (enemisList.Count == 0) break;
                        continue;
                    }

                    list.Add(enemisList[i]);
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

    
    

    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            enemisList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            enemisList.Remove(other.gameObject);
        }
    }
    
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
