using System.Collections;
using UnityEngine;

public class PlayerInsultsState : BaseState
{
    public override IEnumerator EnterState(GameplayManager gameplayManager)
    {
        gameplayManager.PopulateUI(TypeOfTurn.Insult);
        yield return new WaitForSeconds(1f);
    }
}
