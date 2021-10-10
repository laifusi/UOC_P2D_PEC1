using System.Collections;
using UnityEngine;

public class PlayerInsultsState : BaseState
{
    public override IEnumerator EnterState(GameplayManager gameplayManager)
    {
        gameplayManager.PopulateUI(TypeOfTurn.Insult);
        gameplayManager.ActivateChoicesUI(true);
        yield return new WaitForSeconds(0f);
    }

    public IEnumerator OptionSelected(GameplayManager gameplayManager, string selectedText)
    {
        gameplayManager.ActivateChoicesUI(false);
        gameplayManager.UpdateDialogueUI(selectedText, Player.Player);
        gameplayManager.ActivateDialogueUI(true, Player.Player);

        yield return new WaitForSeconds(2f);

        gameplayManager.ActivateDialogueUI(false, Player.Player);
        gameplayManager.ChangeToState(gameplayManager.ComputerAnswersState);
    }
}
