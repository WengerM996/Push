

using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public override void AddScore(int score)
    {
        _score += score;
        var bestScore = PlayerPrefs.GetInt("Best", 0);
        if (_score > bestScore)
            PlayerPrefs.SetInt("Best", _score);

        View.Instance.ScoreView.text = "Player: " + _score;
    }
}
