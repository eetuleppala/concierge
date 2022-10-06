using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProgressTracker : MonoBehaviour
{
    public int moneyBank;
    private int reputationBank;

    public TMP_Text moneyText;
    public TMP_Text repText;

    private void Start()
    {
        moneyBank = 15;
        reputationBank = 1;
    }

    public void Update()
    {
        moneyText.text = "€: " + this.moneyBank;
        repText.text = "Reputation: " + reputationBank;
    }

    public void UpdateMoney(int money)
    {
        Debug.Log("Adding " + money + "€");
        moneyBank = moneyBank + money;
        Debug.Log(moneyBank);
    }

    public void UpdateReputation(int reputation)
    {
        reputationBank += reputation;
    }
}
