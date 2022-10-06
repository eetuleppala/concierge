using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProgressTracker : MonoBehaviour
{
    private int moneyBank = 10;
    private int reputationBank = 1;

    public TMP_Text moneyText;
    public TMP_Text repText;

    private void Update()
    {
        moneyText.text = "€: " + moneyBank;
        repText.text = "Reputation: " + reputationBank;
    }

    public void UpdateMoney(int money)
    {
        Debug.Log("Adding money");
        moneyBank += money;
    }

    public void UpdateReputation(int reputation)
    {
        reputationBank += reputation;
    }
}
