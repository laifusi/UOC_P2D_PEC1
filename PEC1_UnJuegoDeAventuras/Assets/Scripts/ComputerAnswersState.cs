using System.Collections;
using UnityEngine;

public class ComputerAnswersState : BaseState
{
    public override IEnumerator EnterState(GameplayManager gameplayManager)
    {
        string answer = gameplayManager.ChooseRandom(TypeOfTurn.Answer);
        gameplayManager.ShowDialogueUI(answer, Player.Computer);
        gameplayManager.ActivateDialogueUI(true, Player.Computer);

        yield return new WaitForSeconds(1f);
    }
}
