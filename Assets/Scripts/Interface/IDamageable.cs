

using UnityEngine;

/// <summary>
/// in the future, this interface can be implemented by different objects at the level
/// </summary>

public interface IDamageable
{
    void ApplyHit(StateMachine attacker);
    
}
