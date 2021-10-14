using System.Collections;
using System.Collections.Generic;
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
