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

    /// <summary>
    /// Method that updates the end screen UI to show the winner of the game, the rounds won by the player and the rounds won by the computer.
    /// </summary>
    private void UpdateUI()
    {
        winnerText.text = ScoreManager.Winner;
        computerVictories.text = ScoreManager.ComputerRoundVictories.ToString();
        playerVictories.text = ScoreManager.PlayerRoundVictories.ToString();
    }
}
