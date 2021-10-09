using System.Collections;
using UnityEngine;

public class StartGameState : BaseState
{
    public override IEnumerator EnterState(GameplayManager gameplayManager)
    {
        Player player = gameplayManager.RandomizeTurn();
        gameplayManager.UpdateTurnUIInfo(player);
        gameplayManager.ActivateTurnUI(true);
        
        yield return new WaitForSeconds(2f);

        gameplayManager.ActivateTurnUI(false);
        if (player == Player.Computer)
        {
            gameplayManager.ChangeToState(gameplayManager.ComputerInsultsState);
        }
        else if(player == Player.Player)
        {
            gameplayManager.ChangeToState(gameplayManager.PlayerInsultsState);
        }
    }
}
