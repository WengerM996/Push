using UnityEngine;

/// <summary>
/// here we are waiting to be hit
/// </summary>
public class BrokenTransition : Transition
{
    [SerializeField] private float _fallDistance;
    private StateMachine _stateMachine;
    private Rigidbody _rigidBody;

    private void Awake()
    {
        _stateMachine = GetComponent<StateMachine>();
        _rigidBody = GetComponent<Rigidbody>();
    }

    protected override void Enable()
    {
        _stateMachine.Damaged += OnDamaged;
    }

    private void OnDisable()
    {
        _stateMachine.Damaged -= OnDamaged;
    }

    private void OnDamaged()
    {
        NeedTransit = true;
    }
}
