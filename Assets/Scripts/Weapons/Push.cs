using System;
using UnityEngine;

/// <summary>
/// push effect. attached to the shooting stick
/// </summary>
/// 
public class Push : MonoBehaviour
{
    [SerializeField] private UnitSettings _unitSettings;
    [SerializeField] private float _force;
    
    public StateMachine StateMachine { get; set; }
    
    //public bool Enabled { get; set; }

    private void Awake()
    {
        if (_unitSettings != null)
        {
            _force = _unitSettings.ForcePush;
            Debug.LogWarning("Value of force push set by Scriptable Object");
        }
        else
        {
            Debug.LogError("Scriptable object not found");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.TryGetComponent(out StateMachine stateMachine))
        {
            if (stateMachine == StateMachine) return;
            
            Debug.LogWarning("Collision with " + stateMachine.name);
            stateMachine.ApplyHit(StateMachine);
        }
    }
}
