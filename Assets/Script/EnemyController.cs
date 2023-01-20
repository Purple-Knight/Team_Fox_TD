using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    public int healthPoints = 50;

    public float moveSpeed = 10f;
    private float Speed { get => moveSpeed / 5f; } // Divided for better precision on values used in inspector;

    public PathController path;

    private float pathCurrentTime = 0f;
    private float pathFullTime;

    MoneyManager moneyManager;

    void Start()
    {
        if(path == null)
        {
            Debug.LogError($"Enemy '{name}' : No PathController reference Found");
            return;
        }

        pathFullTime = path.Distance / Speed;

        moneyManager = GameObject.FindGameObjectWithTag("Money").GetComponent<MoneyManager>();
    }

    void Update()
    {
        if (path == null) return;
        if (pathCurrentTime >= pathFullTime) return;

        pathCurrentTime += Time.deltaTime;

        transform.position = path.GetPathPosition(pathCurrentTime, pathFullTime);
    }

    public void TakeDamage(int amount)
    {
        healthPoints -= amount;
        Mathf.Max(healthPoints, 0);
        
        if(healthPoints <= 0) OnKilled();
    }

    private void OnKilled()
    {
        moneyManager.UpdateCoin(100, 0);
        Destroy(gameObject);
        //WaveManager.Instance.OnEnemyKilled.Invoke();
    }


    /*private void OnDestroy()
    {
        WaveManager.Instance.OnEnemyKilled.Invoke();
    }*/
}
