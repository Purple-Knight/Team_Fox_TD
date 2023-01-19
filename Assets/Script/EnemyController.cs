using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    public int healthPoints = 50;

    public float moveSpeed = 10f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void TakeDamage() => TakeDamage(15);

    public void TakeDamage(int amount)
    {
        healthPoints -= amount;
        Mathf.Max(healthPoints, 0);
    }
}
