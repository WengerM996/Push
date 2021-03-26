using System;
using UnityEngine;

/// <summary>
/// framework for all states
/// </summary>

public abstract class State : MonoBehaviour
{
    [SerializeField] private Transition[] _transitions;

    protected StateMachine StateMachine { get; private set; }
    protected Rigidbody RigidBody { get; private set; }
    protected Animator Animator { get; private set; }

    private void Awake()
    {
        StateMachine = GetComponent<StateMachine>();
        OnAwake();
    }

    protected abstract void OnAwake();

    public void Enter(StateMachine stateMachine, Rigidbody rigidBody, Animator animator)
    {
        //StateMachine = stateMachine;
        RigidBody = rigidBody;
        Animator = animator;

        enabled = true;

        foreach (var transition in _transitions)
        {
            transition.enabled = true;
        }
    }

    public void Exit()
    {
        if (enabled)
        {
            foreach (var transition in _transitions)
            {
                transition.enabled = false;
            }

            enabled = false;
        }
    }

    public State GetNextState()
    {
        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
            {
                return transition.TargetState;
            }
        }

        return null;
    }
}
