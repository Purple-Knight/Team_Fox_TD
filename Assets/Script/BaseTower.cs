using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float coolDownBtwShot;
    private float actualCoolDownBtwShot;
    [SerializeField] private float attackPower;
    [SerializeField] private List<GameObject> enemisList = new List<GameObject>();
    
    private CircleCollider2D rangeCollider2D;

    protected void Start()
    {
        rangeCollider2D = gameObject.AddComponent<CircleCollider2D>();
        rangeCollider2D.radius = radius;
        rangeCollider2D.offset = Vector2.zero;
        rangeCollider2D.isTrigger = true;
    }
    
    protected void SetTarget()
    {
        
    }

    protected virtual void Shoot()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
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
}
