public class PlayerInsultsState : BaseState
{
    public override void EnterState(GameplayManager gameplayManager)
    {
        gameplayManager.PopulateUI(TypeOfTurn.Insult);
    }
}
