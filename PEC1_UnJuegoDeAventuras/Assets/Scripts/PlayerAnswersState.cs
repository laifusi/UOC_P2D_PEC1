using System.Collections;
using UnityEngine;

public class PlayerAnswersState : BaseState
{
    public override IEnumerator EnterState(GameplayManager gameplayManager)
    {
        gameplayManager.PopulateUI(TypeOfTurn.Answer);
        gameplayManager.ActivateChoicesUI(true);

        yield return new WaitForSeconds(0f);
    }

    public IEnumerator OptionSelected(GameplayManager gameplayManager, string selectedText)
    {
        gameplayManager.ActivateDialogueUI(false, Player.Computer);
        gameplayManager.ActivateChoicesUI(false);
        gameplayManager.UpdateDialogueUI(selectedText, Player.Player);
        gameplayManager.ActivateDialogueUI(true, Player.Player);

        yield return new WaitForSeconds(2f);

        Player winner = gameplayManager.CheckRoundWinner();
        gameplayManager.UpdateRoundWinnerUI();
        gameplayManager.ActivateRoundWinnerUI(true);
        gameplayManager.ActivateDialogueUI(false, Player.Player);

        yield return new WaitForSeconds(2f);

        gameplayManager.ActivateRoundWinnerUI(false);
        bool ended = gameplayManager.CheckGameWinner();

        if (!ended)
        {
            if (winner == Player.Computer)
            {
                gameplayManager.ChangeToState(gameplayManager.ComputerInsultsState);
            }
            else if (winner == Player.Player)
            {
                gameplayManager.ChangeToState(gameplayManager.PlayerInsultsState);
            }
        }
    }
}
