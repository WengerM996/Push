using System;
using UnityEngine;

/// <summary>
/// frame for future boosters
/// </summary>

public abstract class Booster : MonoBehaviour
{
    [SerializeField] protected float _duration;

    private MeshRenderer _meshRenderer;
    private Collider _collider;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
        
        OnAwake();
    }

    protected abstract void OnAwake();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out StateMachine stateMachine))
        {
            _collider.enabled = false;
            _meshRenderer.enabled = false;
            transform.SetParent(stateMachine.transform);
            Apply(stateMachine);
        }
    }

    protected abstract void Apply(StateMachine stateMachine);
}
