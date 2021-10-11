using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenManager : MonoBehaviour
{
    [SerializeField] private Text winnerText;
    [SerializeField] private Text computerVictories;
    [SerializeField] private Text playerVictories;

    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        winnerText.text = ScoreManager.Winner;
        computerVictories.text = ScoreManager.ComputerRoundVictories.ToString();
        playerVictories.text = ScoreManager.PlayerRoundVictories.ToString();
    }
}
