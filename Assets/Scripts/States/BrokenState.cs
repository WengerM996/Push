using UnityEngine;
using UnityEngine.AI;


/// <summary>
/// in this state, the unit falls, then gets up
/// </summary>
/// 
public class BrokenState : State
{
    private NavMeshAgent _navMeshAgent;
    
    private readonly int Run = Animator.StringToHash("Run");
    private readonly int Speed = Animator.StringToHash("Speed");
    private readonly int Fall = Animator.StringToHash("Fall");

    private void OnEnable()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        Broke();
    }

    private void Broke()
    {
        _navMeshAgent.enabled = false;
        Animator.SetBool(Run, false);
        Animator.SetFloat(Speed, 0f);
        Animator.SetTrigger(Fall);
    }

    protected override void OnAwake()
    {
        
    }
}
