using System.Collections;
using UnityEngine;

public class ComputerInsultsState : BaseState
{
    public override IEnumerator EnterState(GameplayManager gameplayManager)
    {
        string insult = gameplayManager.ChooseRandom(TypeOfTurn.Insult);
        gameplayManager.UpdateDialogueUI(insult, Player.Computer);
        gameplayManager.ActivateDialogueUI(true, Player.Computer);

        yield return new WaitForSeconds(2f);

        gameplayManager.ChangeToState(gameplayManager.PlayerAnswersState);
    }
}
