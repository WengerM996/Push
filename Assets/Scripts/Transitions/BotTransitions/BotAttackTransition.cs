using UnityEngine;


/// <summary>
/// while pursuing a target, a distance check is made to attack
/// </summary>
public class BotAttackTransition : Transition
{
    [SerializeField] private ChaseState _chaseState;
    [SerializeField] private float _approachedDistance;

    private void Update()
    {
        if (_chaseState.Target == null) return;
        
        if (Vector3.Distance(_chaseState.Target.transform.position, transform.position) < _approachedDistance)
        {
            NeedTransit = true;
        }
    }


    protected override void Enable()
    {
        
    }
}
