using System.Collections;
using UnityEngine;

public class PlayerAnswersState : BaseState
{
    public override IEnumerator EnterState(GameplayManager gameplayManager)
    {
        gameplayManager.ActivateChoicesUI(true);
        gameplayManager.PopulateUI(TypeOfTurn.Answer);

        yield return new WaitForSeconds(0f);
    }

    public IEnumerator OptionSelected(GameplayManager gameplayManager, string selectedText)
    {
        gameplayManager.ActivateDialogueUI(false, Player.Computer);
        gameplayManager.ActivateChoicesUI(false);
        gameplayManager.ActivateDialogueUI(true, Player.Player);
        gameplayManager.ShowDialogueUI(selectedText, Player.Player);

        yield return new WaitForSeconds(2f);
    }
}
