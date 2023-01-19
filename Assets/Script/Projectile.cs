using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject target;
    private Rigidbody2D rb;
    
    private Vector2 dir;
    private bool go;
    
    [SerializeField] private float speed;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetTarget(GameObject _target)
    {
        target = _target;
        go = true;
        
    }

    private void Update()
    {
        if (target != null) dir = (target.transform.position - transform.position).normalized * speed;

        rb.velocity = new Vector2 (dir.x, dir.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (target != null && other.gameObject == target)
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (target == null && other.tag == "Player")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
