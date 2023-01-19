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
    
    [SerializeField] private float attackPower;
    
    
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
                for (int i = 0; i < enemisList.Count; i++)
                {
                    list.Add(enemisList[i]);
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

        for (int i = 0; i < list.Count; i++)
        {
            Debug.Log("Attack " + list[i].name);
        }
        
        actualCoolDownBtwShot = coolDownBtwShot;
    }

    

    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            enemisList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
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
