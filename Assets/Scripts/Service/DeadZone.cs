using UnityEngine;
using UnityEngine.Events;

public class DeadZone : MonoBehaviour
{
    public static event UnityAction<StateMachine, StateMachine> Fallen;
    public static event UnityAction PlayerFallen; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerStateMachine player))
        {
            PlayerFallen?.Invoke();
        }
        
        if (other.TryGetComponent(out StateMachine stateMachine))
        {
            Fallen?.Invoke(stateMachine, stateMachine.LastTouch);
            
            Destroy(other.gameObject);
        }
    }
}
