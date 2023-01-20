using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public GameObject coinText;
    TextMeshProUGUI coinCounter;

    private int coinAmount = 0;

    private void Start()
    {
        coinCounter = coinText.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) UpdateCoin(100, 0);
        if (Input.GetKeyDown(KeyCode.V)) UpdateCoin(0, 100);
    }


    public void UpdateCoin(int add, int substract)
    {
        coinAmount = coinAmount + add - substract;
        coinCounter.text = coinAmount.ToString();
    }
}
