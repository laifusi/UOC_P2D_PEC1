using System.Collections;
using UnityEngine;

public class ComputerAnswersState : BaseState
{
    public override IEnumerator EnterState(GameplayManager gameplayManager)
    {
        gameplayManager.ChooseRandom(TypeOfTurn.Answer);
        yield return new WaitForSeconds(1f);
    }
}
