using System;
using UnityEngine;


/// <summary>
/// the camera follows the player
/// </summary>

public class CameraTracker : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offsetPosition;
    [SerializeField] private Vector3 _rotation;
    [SerializeField] private float _speed;

    private void OnEnable()
    {
        UnitSpawner.PlayerCreated += OnPlayerCreated;
    }

    private void OnDisable()
    {
        UnitSpawner.PlayerCreated -= OnPlayerCreated;
    }

    private void OnPlayerCreated(StateMachine stateMachine)
    {
        _target = stateMachine.transform;
    }

    private void Update()
    {
        if (_target == null) return;
        
        var newPosition = _target.position + _offsetPosition;

        transform.position = Vector3.Lerp(transform.position, newPosition, _speed);
        transform.rotation = Quaternion.Euler(_rotation);
    }
}
