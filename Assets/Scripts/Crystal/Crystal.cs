using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// collectable bonus increasing points
/// </summary>

public class Crystal : MonoBehaviour
{
    [SerializeField] private int _score;
    [SerializeField] private bool _customSettings;
    
    public static event UnityAction<int, StateMachine> Picked;
    
    public int Score
    {
        get => _score;
        set
        {
            if (_customSettings == false)
                _score = value;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out StateMachine stateMachine))
        {
            var score = (_customSettings) ? _score : 0;
            Picked?.Invoke(score, stateMachine);
            Destroy(gameObject);
        }
    }
}
