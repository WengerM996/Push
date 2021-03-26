using UnityEngine;

/// <summary>
/// framework for all transitions
/// </summary>
public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    public State TargetState => _targetState;
    public bool NeedTransit { get; protected set; }
    
    
    private void OnEnable()
    {
        NeedTransit = false;
        Enable();
    }

    protected abstract void Enable();
}
