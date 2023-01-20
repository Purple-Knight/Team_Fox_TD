using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public static PlayerHealthManager Instance { get { return instance; } }
    private static PlayerHealthManager instance;

    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    private void Start()
    {
        if (PlayerHealthManager.Instance != null)
        {
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
        }
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
