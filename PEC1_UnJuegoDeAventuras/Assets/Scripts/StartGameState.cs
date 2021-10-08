public class StartGameState : BaseState
{
    public override void EnterState(GameplayManager gameplayManager)
    {
        gameplayManager.RandomizeTurn();
    }
}
