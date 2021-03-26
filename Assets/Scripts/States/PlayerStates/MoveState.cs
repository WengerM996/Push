using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class MoveState : State
{
    [SerializeField] private UnitSettings _unitSettings;
    [SerializeField] private float _speed;
    
    private FloatingJoystick _floatingJoystick;

    private readonly int RunSpeedAnimation = Animator.StringToHash("Speed");
    private readonly int RunTrigger = Animator.StringToHash("Run");

    private void OnEnable()
    {
        Animator.SetBool(RunTrigger, true);
    }
    
    protected override void OnAwake()
    {
        if (_unitSettings != null)
        {
            _speed = _unitSettings.SpeedMovement;
            Debug.LogWarning("Value of player speed set by Scriptable Object");
        }
        else
        {
            Debug.LogError("Scriptable object not found");
        }
    }

    private void Start()
    {
        _floatingJoystick = View.Instance.FloatingJoystick;
    }

    public void FixedUpdate()
    {
        if (_floatingJoystick == null) return;
        
        var horizontal = Math.Abs(_floatingJoystick.Horizontal);
        var vertical = Math.Abs(_floatingJoystick.Vertical);
        
        var animationSpeed = (horizontal > vertical)
            ? horizontal
            : vertical;

        Animator.SetFloat(RunSpeedAnimation, animationSpeed);
        
        Vector3 direction = Vector3.forward * _floatingJoystick.Vertical + Vector3.right * _floatingJoystick.Horizontal;
        RigidBody.MovePosition(transform.position + direction * (_speed * Time.fixedDeltaTime));
        RigidBody.MoveRotation(Quaternion.LookRotation(direction));
    }
}
