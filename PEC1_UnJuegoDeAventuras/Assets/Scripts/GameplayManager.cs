using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    private BaseState currentState;

    public readonly StartGameState StartGameState = new StartGameState();
    public readonly ComputerInsultsState ComputerInsultsState = new ComputerInsultsState();
    public readonly PlayerInsultsState PlayerInsultsState = new PlayerInsultsState();
    public readonly ComputerAnswersState ComputerAnswersState = new ComputerAnswersState();
    public readonly PlayerAnswersState PlayerAnswersState = new PlayerAnswersState();

    [SerializeField] private Image turnInformationPanel;
    [SerializeField] private Text turnInformationText;

    private void Start()
    {
        ChangeToState(StartGameState);
    }

    public void ChangeToState(BaseState state)
    {
        currentState = state;
        StartCoroutine(currentState.EnterState(this));
    }

    public Player RandomizeTurn()
    {
        Player player;
        int randomInt = Random.Range(0, 2);

        if (randomInt == 0)
            player = Player.Computer;
        else
            player = Player.Player;

        return player;
    }

    public void UpdateTurnUIInfo(Player player)
    {
        turnInformationText.text = "El turno es del ";
        if(player == Player.Computer)
        {
            turnInformationPanel.color = Color.red;
            turnInformationText.text += "ORDENADOR";
        }
        else if(player == Player.Player)
        {
            turnInformationPanel.color = Color.blue;
            turnInformationText.text += "JUGADOR";
        }
    }

    public void ActivateTurnUI(bool activate)
    {
        turnInformationPanel.gameObject.SetActive(activate);
    }

    public void ChooseRandom(TypeOfTurn typeOfTurn)
    {

    }

    public void PopulateUI(TypeOfTurn typeOfTurn)
    {
        
    }
}

public enum Player
{
    Computer, Player
}

public enum TypeOfTurn
{
    Insult, Answer
}