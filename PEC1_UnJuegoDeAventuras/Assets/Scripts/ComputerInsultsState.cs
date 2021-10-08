public class ComputerInsultsState : BaseState
{
    public override void EnterState(GameplayManager gameplayManager)
    {
        gameplayManager.ChooseRandom(TypeOfTurn.Insult);
    }
}
