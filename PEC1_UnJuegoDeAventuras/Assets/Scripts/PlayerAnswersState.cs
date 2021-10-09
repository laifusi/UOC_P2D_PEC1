using System.Collections;
using UnityEngine;

public class PlayerAnswersState : BaseState
{
    public override IEnumerator EnterState(GameplayManager gameplayManager)
    {
        gameplayManager.PopulateUI(TypeOfTurn.Answer);
        yield return new WaitForSeconds(1f);
    }
}
