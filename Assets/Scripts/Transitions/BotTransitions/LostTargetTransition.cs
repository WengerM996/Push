using UnityEngine;

/// <summary>
/// during the attack, a check is made for a distance so that if you need to switch to chase mode
/// </summary>
public class LostTargetTransition : Transition
{
    [SerializeField] private ChaseState _chaseState;
    [SerializeField] private float _minimumLostDistance;

    private void Update()
    {
        if (_chaseState.Target == null)
        {
            NeedTransit = true;
            return;
        }
        
        if (Vector3.Distance(_chaseState.Target.transform.position, transform.position) > _minimumLostDistance)
        {
            NeedTransit = true;
        }
    }

    protected override void Enable()
    {
        
    }
}
