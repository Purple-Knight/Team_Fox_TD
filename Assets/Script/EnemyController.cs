using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    public int healthPoints = 50;

    public float moveSpeed = 10f;
    private float Speed { get => moveSpeed / 5f; } // Divided for better precision on values used in inspector;

    public PathController path;

    /// <summary>
    /// Returns Full Path progression between 0-1.
    /// </summary>
    public float GetPathProgress { get => pathCurrentTime / pathFullTime; }

    private float pathCurrentTime = 0f;
    private float pathFullTime;

    void Start()
    {
        if(path == null)
        {
            Debug.LogError($"Enemy '{name}' : No PathController reference Found");
            return;
        }

        pathFullTime = path.Distance / Speed;
    }

    void Update()
    {
        if (path == null) return;
        if (pathCurrentTime >= pathFullTime)
        {
            ReachedGoal();
            return;
        }

        pathCurrentTime += Time.deltaTime;

        transform.position = path.GetPathPosition(pathCurrentTime, pathFullTime);
    }

    public void TakeDamage(int amount)
    {
        healthPoints -= amount;
        Mathf.Max(healthPoints, 0);
        
        if(healthPoints <= 0) Destroy(gameObject);
    }

    private void ReachedGoal()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        WaveManager.Instance.OnEnemyKilled.Invoke();
    }
}
