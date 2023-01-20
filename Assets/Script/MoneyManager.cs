using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get { return instance; } }
    private static MoneyManager instance;

    public GameObject coinText;
    TextMeshProUGUI coinCounter;

    private int coinAmount = 0;
    public bool CanBuy(int amount) => coinAmount >= amount ? true : false; 

    private void Start()
    {
        instance = this;
        coinCounter = coinText.GetComponent<TextMeshProUGUI>();
        UpdateCoin(500 ,0);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.C)) UpdateCoin(100, 0);
        //if (Input.GetKeyDown(KeyCode.V)) UpdateCoin(0, 100);
    }


    public void UpdateCoin(int add, int substract)
    {
        coinAmount = coinAmount + add - substract;
        coinCounter.text = coinAmount.ToString();
    }
}
