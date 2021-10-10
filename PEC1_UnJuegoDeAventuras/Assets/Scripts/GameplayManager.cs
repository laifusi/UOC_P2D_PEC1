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
    [SerializeField] private Text playerDialogueText;
    [SerializeField] private GameObject choicePanel;
    [SerializeField] private Transform choiceContent;
    [SerializeField] private GameObject choicePrefab;

    private string[] insults;
    private string[] answers;

    private string playerInsult;

    public string PlayerInsult => playerInsult;

    private void Start()
    {
        insults = FileReader.ReadFile("Insultos");
        answers = FileReader.ReadFile("Respuestas");

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

    public string ChooseRandom(TypeOfTurn typeOfTurn)
    {
        string randString = "";

        if(typeOfTurn == TypeOfTurn.Insult)
        {
            int randInt = Random.Range(0, insults.Length);
            randString = insults[randInt];
        }
        else if(typeOfTurn == TypeOfTurn.Answer)
        {
            int randInt = Random.Range(0, answers.Length);
            randString = answers[randInt];
        }

        return randString;
    }
    public void ActivateDialogueUI(bool activate, Player player)
    {
        if(player == Player.Computer)
        {
            computerDialogueText.gameObject.SetActive(activate);
        }
        else if(player == Player.Player)
        {
            playerDialogueText.gameObject.SetActive(activate);
        }
    }

    public void ShowDialogueUI(string text, Player player)
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
        if(typeOfTurn == TypeOfTurn.Insult)
        {
            for(int i = 0; i < insults.Length; i++)
            {
                int index = i;
                GameObject option = Instantiate(choicePrefab, choiceContent);
                option.GetComponent<Text>().text = insults[i];
                option.GetComponent<Button>().onClick.AddListener(() => { PlayerChoice(insults[index]); });
            }
        }
        else if(typeOfTurn == TypeOfTurn.Answer)
        {
            for (int i = 0; i < answers.Length; i++)
            {
                int index = i;
                GameObject option = Instantiate(choicePrefab, choiceContent);
                option.GetComponent<Text>().text = answers[i];
                option.GetComponent<Button>().onClick.AddListener(() => { PlayerChoice(answers[index]); });
            }
        }
    }

    private void PlayerChoice(string selectedOption)
    {
        if (currentState == PlayerInsultsState)
            StartCoroutine(PlayerInsultsState.OptionSelected(this, selectedOption));
        else if (currentState == PlayerAnswersState)
            StartCoroutine(PlayerAnswersState.OptionSelected(this, selectedOption));
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