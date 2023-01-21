using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public static LifeManager Instance { get { return instance; } }
    private static LifeManager instance;

    public GameObject lifeText;
    TextMeshProUGUI lifeCounter;

    private void Start()
    {
        instance = this;
        lifeCounter = lifeText.GetComponent<TextMeshProUGUI>();
    }


    public void UpdateLife(int currentLife)
    {
        lifeCounter.text = currentLife.ToString();
    }
}
