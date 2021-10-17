using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    #region States
    private BaseState currentState;

    public readonly StartGameState StartGameState = new StartGameState();
    public readonly ComputerInsultsState ComputerInsultsState = new ComputerInsultsState();
    public readonly PlayerInsultsState PlayerInsultsState = new PlayerInsultsState();
    public readonly ComputerAnswersState ComputerAnswersState = new ComputerAnswersState();
    public readonly PlayerAnswersState PlayerAnswersState = new PlayerAnswersState();
    #endregion

    [Header("Turn UI")]
    [SerializeField] private Image turnInformationPanel;
    [SerializeField] private Text turnInformationText;

    [Header("Dialogue UI")]
    [SerializeField] private Text computerDialogueText;
    [SerializeField] private GameObject computerDialoguePanel;
    [SerializeField] private Text playerDialogueText;
    [SerializeField] private GameObject playerDialoguePanel;

    [Header("Player Choices")]
    [SerializeField] private GameObject choicePanel;
    [SerializeField] private Transform choiceContent;
    [SerializeField] private GameObject choicePrefab;

    [Header("Round Winner UI")]
    [SerializeField] private Image roundWinnerPanel;
    [SerializeField] private Text roundWinnerText;

    [Header("Fixed UI")]
    [SerializeField] private Text computerVictoriesText;
    [SerializeField] private Text playerVictoriesText;
    [SerializeField] private Text fixedTurnText;

    [Header("Sound")]
    [SerializeField] private AudioSource winLoseSoundEffect;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;

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

    /// <summary>
    /// Start method that gets the text data, initializes the score and the state machine.
    /// </summary>
    private void Start()
    {
        insults = FileReader.ReadFile(TypeOfTurn.Insult);
        answers = FileReader.ReadFile(TypeOfTurn.Answer);

        ScoreManager.ComputerRoundVictories = 0;
        ScoreManager.PlayerRoundVictories = 0;
        ScoreManager.Winner = "";

        ChangeToState(StartGameState);
    }

    /// <summary>
    /// Method that updates the currentState and changes to the specified one.
    /// </summary>
    /// <param name="state">State to switch to.</param>
    public void ChangeToState(BaseState state)
    {
        currentState = state;
        StartCoroutine(currentState.EnterState(this));
    }

    /// <summary>
    /// Method to randomize the initial player.
    /// </summary>
    /// <returns>Player chosen randomly.</returns>
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

    /// <summary>
    /// Method that chooses a random string from the specified type of strings, insults or answers.
    /// In hard mode the answers has a higher possibility of being correctly chosen.
    /// </summary>
    /// <param name="typeOfTurn">Type of computer turn, determines which array of strings we need to choose from</param>
    /// <returns>Chosen string.</returns>
    public string ChooseRandom(TypeOfTurn typeOfTurn)
    {
        string randString = "";

        if (typeOfTurn == TypeOfTurn.Insult)
        {
            int randInt = Random.Range(0, insults.Length);
            randString = insults[randInt];
            computerInsultIndex = randInt;
        }
        else if (typeOfTurn == TypeOfTurn.Answer)
        {
            if (DifficultyManager.DifficultyLevel == DifficultyLevel.Easy)
            {
                int randInt = Random.Range(0, answers.Length);
                randString = answers[randInt];
                computerAnswerIndex = randInt;
            }
            else if (DifficultyManager.DifficultyLevel == DifficultyLevel.Hard)
            {
                int correctAnswer = Random.Range(0, 2);
                if (correctAnswer == 1)
                {
                    randString = answers[playerInsultIndex];
                    computerAnswerIndex = playerInsultIndex;
                }
                else
                {
                    int randInt = Random.Range(0, answers.Length);
                    randString = answers[randInt];
                    computerAnswerIndex = randInt;
                }
            }
        }

        return randString;
    }

    #region Activate UI Methods

    /// <summary>
    /// Method to activate or deactivate the turnInformationPanel.
    /// </summary>
    /// <param name="activate">Bool that determines whether we activate or deactivate.</param>
    public void ActivateTurnUI(bool activate)
    {
        turnInformationPanel.gameObject.SetActive(activate);
    }

    /// <summary>
    /// Method to activate or deactivate the dialogue panel for an specified player.
    /// </summary>
    /// <param name="activate">Bool that determines whether we activate or deactivate.</param>
    /// <param name="player">Type of player whose dialogue panel we need to activate or deactivate.</param>
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

    /// <summary>
    /// Method to activate or deactivate the choicePanel.
    /// </summary>
    /// <param name="activate">Bool that determines whether we activate or deactivate.</param>
    public void ActivateChoicesUI(bool activate)
    {
        choicePanel.SetActive(activate);
    }

    /// <summary>
    /// Method to activate or deactivate the roundWinnerPanel.
    /// </summary>
    /// <param name="activate">Bool that determines whether we activate or deactivate.</param>
    public void ActivateRoundWinnerUI(bool activate)
    {
        roundWinnerPanel.gameObject.SetActive(activate);
    }

    #endregion

    #region Update UI Methods

    /// <summary>
    /// Method that populates the choices panel for the player to choose an insult or an answer.
    /// It also creates the buttons and assigns their listeners.
    /// </summary>
    /// <param name="typeOfTurn">Type of player turn that determines which array of strings need to be shown.</param>
    public void PopulateUI(TypeOfTurn typeOfTurn)
    {
        foreach (Transform child in choiceContent.transform)
        {
            Destroy(child.gameObject);
        }

        if (typeOfTurn == TypeOfTurn.Insult)
        {
            for (int i = 0; i < insults.Length; i++)
            {
                int index = i;
                GameObject option = Instantiate(choicePrefab, choiceContent);
                option.GetComponent<Text>().text = insults[i];
                option.GetComponent<Button>().onClick.AddListener(() => { PlayerChoice(insults[index], index); });
            }
        }
        else if (typeOfTurn == TypeOfTurn.Answer)
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

    /// <summary>
    /// Method to update the turn information.
    /// </summary>
    /// <param name="player">Player who has the turn.</param>
    public void UpdateTurnUIInfo(Player player)
    {
        turnInformationText.text = "El turno es del ";
        if (player == Player.Computer)
        {
            turnInformationPanel.color = Color.red;
            turnInformationText.text += ComputerString;
        }
        else if (player == Player.Player)
        {
            turnInformationPanel.color = Color.blue;
            turnInformationText.text += PlayerString;
        }
    }

    /// <summary>
    /// Method to update the turn information always showing on the screen.
    /// </summary>
    /// <param name="player">Player who has the turn.</param>
    /// <param name="typeOfTurn">Type of turn.</param>
    public void UpdateFixedTurnInfo(Player player, TypeOfTurn typeOfTurn)
    {
        if (player == Player.Computer)
        {
            fixedTurnText.text = "Ordenador";
            fixedTurnText.color = Color.red;
        }
        else if (player == Player.Player)
        {
            fixedTurnText.text = "Jugador";
            fixedTurnText.color = Color.blue;
        }

        fixedTurnText.text += " ";

        if (typeOfTurn == TypeOfTurn.Insult)
        {
            fixedTurnText.text += "Insulta";
        }
        else if (typeOfTurn == TypeOfTurn.Answer)
        {
            fixedTurnText.text += "Contesta";
        }
    }

    /// <summary>
    /// Method to update the dialogue text.
    /// </summary>
    /// <param name="text">Chosen string.</param>
    /// <param name="player">Player who chose it.</param>
    public void UpdateDialogueUI(string text, Player player)
    {
        if (player == Player.Computer)
        {
            computerDialogueText.text = text;
        }
        else if (player == Player.Player)
        {
            playerDialogueText.text = text;
        }
    }

    /// <summary>
    /// Method to update the round winner information.
    /// </summary>
    public void UpdateRoundWinnerUI()
    {
        roundWinnerText.text = "El ganador de la ronda es el " + lastWinner;
        if (lastWinner == PlayerString)
            roundWinnerPanel.color = Color.blue;
        else
            roundWinnerPanel.color = Color.red;
        computerVictoriesText.text = computerVictories.ToString();
        playerVictoriesText.text = playerVictories.ToString();
    }

    #endregion

    /// <summary>
    /// Method called when the player chooses an insult or an answer.
    /// It starts the corresponding state's coroutine.
    /// </summary>
    /// <param name="selectedOption">String chosen.</param>
    /// <param name="index">Index of the chosen string.</param>
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

    #region Winner Checks

    /// <summary>
    /// Method to check who won the round.
    /// </summary>
    /// <returns>Player that won the round.</returns>
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
                winLoseSoundEffect.PlayOneShot(loseSound);
            }
            else
            {
                playerVictories++;
                lastWinner = PlayerString;
                roundWinner = Player.Player;
                winLoseSoundEffect.PlayOneShot(winSound);
            }
        }
        else if(currentState == PlayerAnswersState)
        {
            if(computerInsultIndex == playerAnswerIndex)
            {
                playerVictories++;
                lastWinner = PlayerString;
                roundWinner = Player.Player;
                winLoseSoundEffect.PlayOneShot(winSound);
            }
            else
            {
                computerVictories++;
                lastWinner = ComputerString;
                roundWinner = Player.Computer;
                winLoseSoundEffect.PlayOneShot(loseSound);
            }
        }

        return roundWinner;
    }

    /// <summary>
    /// Method to check whether the game ended or not and who won it.
    /// If the game ended, it updates the score.
    /// </summary>
    /// <returns>Bool that defines whether the game ended or not.</returns>
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

    #endregion
}

public enum Player
{
    Computer, Player
}

public enum TypeOfTurn
{
    Insult, Answer
}