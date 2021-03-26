

public class BotStateMachine : StateMachine
{
    public override void AddScore(int score)
    {
        _score += score;
    }
}
