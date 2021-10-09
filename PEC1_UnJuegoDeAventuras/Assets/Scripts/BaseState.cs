using System.Collections;

public abstract class BaseState
{
    public abstract IEnumerator EnterState(GameplayManager gameplayManager);
}
