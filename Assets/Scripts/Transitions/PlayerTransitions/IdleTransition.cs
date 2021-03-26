using System;
using UnityEngine;

public class IdleTransition : Transition
{
    private FloatingJoystick _floatingJoystick;
    
    private void Update()
    {
        if (_floatingJoystick == null) return;
        
        if (_floatingJoystick.Horizontal == 0f && _floatingJoystick.Vertical == 0f)
        {
            NeedTransit = true;
        }
    }

    protected override void Enable()
    {
        
    }

    private void Start()
    {
        _floatingJoystick = View.Instance.FloatingJoystick;
    }
}
