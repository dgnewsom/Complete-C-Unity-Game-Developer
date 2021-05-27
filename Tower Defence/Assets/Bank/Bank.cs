using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;
    [SerializeField] int currentBalance;
    GoldDisplay goldDisplay;

    public int CurrentBalance { get => currentBalance;}

    void Start()
    {
        currentBalance = startingBalance;
        goldDisplay = FindObjectOfType<GoldDisplay>();
        goldDisplay.UpdateDisplay(CurrentBalance);
    }

    public void Withdraw(int amountToWithdraw)
    {
        currentBalance -= Mathf.Abs(amountToWithdraw);
        if(currentBalance < 0)
        {
            FindObjectOfType<GameOverScript>().DisplayGameOverScreen(true);
        }
        goldDisplay.UpdateDisplay(currentBalance);
    }

    public void Deposit(int amountToDeposit)
    {
        currentBalance += Mathf.Abs(amountToDeposit);
        goldDisplay.UpdateDisplay(currentBalance);
    }

    
}
