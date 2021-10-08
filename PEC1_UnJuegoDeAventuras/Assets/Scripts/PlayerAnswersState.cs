public class PlayerAnswersState : BaseState
{
    public override void EnterState(GameplayManager gameplayManager)
    {
        gameplayManager.PopulateUI(TypeOfTurn.Answer);
    }
}
