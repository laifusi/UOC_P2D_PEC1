public class ComputerAnswersState : BaseState
{
    public override void EnterState(GameplayManager gameplayManager)
    {
        gameplayManager.ChooseRandom(TypeOfTurn.Answer);
    }
}
