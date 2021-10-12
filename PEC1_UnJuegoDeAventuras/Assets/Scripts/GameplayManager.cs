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
    [SerializeField] private Text computerDialogueText;
    [SerializeField] private GameObject computerDialoguePanel;
    [SerializeField] private Text playerDialogueText;
    [SerializeField] private GameObject playerDialoguePanel;
    [SerializeField] private GameObject choicePanel;
    [SerializeField] private Transform choiceContent;
    [SerializeField] private GameObject choicePrefab;
    [SerializeField] private Image roundWinnerPanel;
    [SerializeField] private Text roundWinnerText;
    [SerializeField] private Text computerVictoriesText;
    [SerializeField] private Text playerVictoriesText;

    private string[] insults;
    private string[] answers;

    private int computerInsultIndex;
    private int computerAnswerIndex;
    private int playerInsultIndex;
    private int playerAnswerIndex;

    private int computerVictories;
    private int playerVictories;
    private string lastWinner;

    private const string PlayerString = "JUGADOR";
    private const string ComputerString = "ORDENADOR";

    private void Start()
    {
        insults = FileReader.ReadFile("Insultos");
        answers = FileReader.ReadFile("Respuestas");

        ScoreManager.ComputerRoundVictories = 0;
        ScoreManager.PlayerRoundVictories = 0;
        ScoreManager.Winner = "";

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
            turnInformationText.text += ComputerString;
        }
        else if(player == Player.Player)
        {
            turnInformationPanel.color = Color.blue;
            turnInformationText.text += PlayerString;
        }
    }

    public void ActivateTurnUI(bool activate)
    {
        turnInformationPanel.gameObject.SetActive(activate);
    }

    public string ChooseRandom(TypeOfTurn typeOfTurn)
    {
        string randString = "";

        if(typeOfTurn == TypeOfTurn.Insult)
        {
            int randInt = Random.Range(0, insults.Length);
            randString = insults[randInt];
            computerInsultIndex = randInt;
        }
        else if(typeOfTurn == TypeOfTurn.Answer)
        {
            int randInt = Random.Range(0, answers.Length);
            randString = answers[randInt];
            computerAnswerIndex = randInt;
        }

        return randString;
    }
    public void ActivateDialogueUI(bool activate, Player player)
    {
        if(player == Player.Computer)
        {
            computerDialoguePanel.SetActive(activate);
        }
        else if(player == Player.Player)
        {
            playerDialoguePanel.SetActive(activate);
        }
    }

    public void UpdateDialogueUI(string text, Player player)
    {
        if(player == Player.Computer)
        {
            computerDialogueText.text = text;
        }
        else if(player == Player.Player)
        {
            playerDialogueText.text = text;
        }
    }

    public void ActivateChoicesUI(bool activate)
    {
        choicePanel.SetActive(activate);
    }

    public void PopulateUI(TypeOfTurn typeOfTurn)
    {
        foreach(Transform child in choiceContent.transform)
        {
            Destroy(child.gameObject);
        }

        if(typeOfTurn == TypeOfTurn.Insult)
        {
            for(int i = 0; i < insults.Length; i++)
            {
                int index = i;
                GameObject option = Instantiate(choicePrefab, choiceContent);
                option.GetComponent<Text>().text = insults[i];
                option.GetComponent<Button>().onClick.AddListener(() => { PlayerChoice(insults[index], index); });
            }
        }
        else if(typeOfTurn == TypeOfTurn.Answer)
        {
            for (int i = 0; i < answers.Length; i++)
            {
                int index = i;
                GameObject option = Instantiate(choicePrefab, choiceContent);
                option.GetComponent<Text>().text = answers[i];
                option.GetComponent<Button>().onClick.AddListener(() => { PlayerChoice(answers[index], index); });
            }
        }
    }

    private void PlayerChoice(string selectedOption, int index)
    {
        if (currentState == PlayerInsultsState)
        {
            playerInsultIndex = index;
            StartCoroutine(PlayerInsultsState.OptionSelected(this, selectedOption));
        }
        else if (currentState == PlayerAnswersState)
        {
            playerAnswerIndex = index;
            StartCoroutine(PlayerAnswersState.OptionSelected(this, selectedOption));
        }
    }

    public Player CheckRoundWinner()
    {
        Player roundWinner = Player.Computer;

        if(currentState == ComputerAnswersState)
        {
            if(playerInsultIndex == computerAnswerIndex)
            {
                computerVictories++;
                lastWinner = ComputerString;
                roundWinner = Player.Computer;
            }
            else
            {
                playerVictories++;
                lastWinner = PlayerString;
                roundWinner = Player.Player;
            }
        }
        else if(currentState == PlayerAnswersState)
        {
            if(computerInsultIndex == playerAnswerIndex)
            {
                playerVictories++;
                lastWinner = PlayerString;
                roundWinner = Player.Player;
            }
            else
            {
                computerVictories++;
                lastWinner = ComputerString;
                roundWinner = Player.Computer;
            }
        }

        return roundWinner;
    }

    public void ActivateRoundWinnerUI(bool activate)
    {
        roundWinnerPanel.gameObject.SetActive(activate);
    }

    public void UpdateRoundWinnerUI()
    {
        roundWinnerText.text = "El ganador de la ronda es el " + lastWinner + "\n Es su turno.";
        if (lastWinner == PlayerString)
            roundWinnerPanel.color = Color.blue;
        else
            roundWinnerPanel.color = Color.red;
        computerVictoriesText.text = computerVictories.ToString();
        playerVictoriesText.text = playerVictories.ToString();
    }

    public bool CheckGameWinner()
    {
        if (computerVictories >= 3 || playerVictories >= 3)
        {
            ScoreManager.ComputerRoundVictories = computerVictories;
            ScoreManager.PlayerRoundVictories = playerVictories;
            ScoreManager.Winner = lastWinner;
            MenuManager.EndGame();
            return true;
        }
        else
        {
            return false;
        }
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