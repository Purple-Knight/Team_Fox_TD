using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void OnLoseHealth()
    {
        currentHealth--;
        if(currentHealth <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }
}
