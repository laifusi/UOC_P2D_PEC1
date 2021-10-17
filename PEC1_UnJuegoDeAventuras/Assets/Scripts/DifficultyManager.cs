using UnityEngine;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyLevel DifficultyLevel;

    [SerializeField] Button[] difficultyButtons;

    private void Start()
    {
        UpdateButtonInteractivity();
    }

    /// <summary>
    /// Method that changes the difficulty level.
    /// </summary>
    /// <param name="difficultyLevel">Number correponding to the new level of difficulty.</param>
    public void ChangeDifficulty(int difficultyLevel)
    {
        if(difficultyLevel == 0)
        {
            DifficultyLevel = DifficultyLevel.Easy;
        }
        else if(difficultyLevel == 1)
        {
            DifficultyLevel = DifficultyLevel.Hard;
        }

        UpdateButtonInteractivity();
    }

    /// <summary>
    /// Method that updates the button interactivity for each of the difficulty level buttons.
    /// It makes the chosen level not be interactable.
    /// </summary>
    private void UpdateButtonInteractivity()
    {
        for (int i = 0; i < difficultyButtons.Length; i++)
        {
            if (i == (int)DifficultyLevel)
            {
                difficultyButtons[i].interactable = false;
            }
            else
            {
                difficultyButtons[i].interactable = true;
            }
        }
    }
}

public enum DifficultyLevel
{
    Easy, Hard
}
