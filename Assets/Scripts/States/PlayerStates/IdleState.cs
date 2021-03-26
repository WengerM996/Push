using UnityEngine;

public class IdleState : State
{
    private readonly int RunSpeedAnimation = Animator.StringToHash("Speed");
    private readonly int RunTrigger = Animator.StringToHash("Run");
    
    private void OnEnable()
    {
        Animator.SetFloat(RunSpeedAnimation, 0f);
        Animator.SetBool(RunTrigger , false);
    }

    protected override void OnAwake()
    {
        
    }
}
