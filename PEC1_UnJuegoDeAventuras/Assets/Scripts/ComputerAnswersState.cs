using System.Collections;
using UnityEngine;

public class ComputerAnswersState : BaseState
{
    public override IEnumerator EnterState(GameplayManager gameplayManager)
    {
        string answer = gameplayManager.ChooseRandom(TypeOfTurn.Answer);
        gameplayManager.UpdateDialogueUI(answer, Player.Computer);
        gameplayManager.ActivateDialogueUI(true, Player.Computer);

        yield return new WaitForSeconds(2f);

        Player winner = gameplayManager.CheckRoundWinner();
        gameplayManager.UpdateRoundWinnerUI();
        gameplayManager.ActivateRoundWinnerUI(true);
        gameplayManager.ActivateDialogueUI(false, Player.Computer);

        yield return new WaitForSeconds(2f);

        gameplayManager.ActivateRoundWinnerUI(false);
        bool ended = gameplayManager.CheckGameWinner();

        if(!ended)
        {
            if(winner == Player.Computer)
            {
                gameplayManager.ChangeToState(gameplayManager.ComputerInsultsState);
            }
            else if(winner == Player.Player)
            {
                gameplayManager.ChangeToState(gameplayManager.PlayerInsultsState);
            }
        }
    }
}
