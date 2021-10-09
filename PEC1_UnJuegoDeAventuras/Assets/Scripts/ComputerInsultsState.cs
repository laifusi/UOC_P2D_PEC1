using System.Collections;
using UnityEngine;

public class ComputerInsultsState : BaseState
{
    public override IEnumerator EnterState(GameplayManager gameplayManager)
    {
        gameplayManager.ChooseRandom(TypeOfTurn.Insult);
        yield return new WaitForSeconds(1f);
    }
}
