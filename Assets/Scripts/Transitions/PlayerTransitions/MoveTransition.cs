using System;
using UnityEngine;

public class MoveTransition : Transition
{
    private FloatingJoystick _floatingJoystick;

    public void Update()
    {
        if (_floatingJoystick.Horizontal > 0f || _floatingJoystick.Vertical > 0f)
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
