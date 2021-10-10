using System.Collections;
using UnityEngine;

public class PlayerInsultsState : BaseState
{
    public override IEnumerator EnterState(GameplayManager gameplayManager)
    {
        gameplayManager.ActivateChoicesUI(true);
        gameplayManager.PopulateUI(TypeOfTurn.Insult);
        yield return new WaitForSeconds(0f);
    }

    public IEnumerator OptionSelected(GameplayManager gameplayManager, string selectedText)
    {
        gameplayManager.ActivateChoicesUI(false);
        gameplayManager.ActivateDialogueUI(true, Player.Player);
        gameplayManager.ShowDialogueUI(selectedText, Player.Player);

        yield return new WaitForSeconds(2f);

        gameplayManager.ActivateDialogueUI(false, Player.Player);
        gameplayManager.ChangeToState(gameplayManager.ComputerAnswersState);
    }
}
