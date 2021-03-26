using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// in this state, the bot searches for and pursues a goal
/// </summary>

[RequireComponent(typeof(NavMeshAgent))]
public class ChaseState : State
{
    [SerializeField] private UnitSettings _unitSettings;
    [SerializeField] private float _speed;
    
    private NavMeshAgent _agent;
    private readonly int Run = Animator.StringToHash("Run");
    private readonly int Speed = Animator.StringToHash("Speed");

    //private StateMachine _target;
    private Coroutine _coroutine;
    private bool _targetFinded;

    public StateMachine Target { get; set; }

    protected override void OnAwake()
    {
        _agent = GetComponent<NavMeshAgent>();
        
        if (_unitSettings != null)
        {
            _speed = _unitSettings.SpeedMovement;
            Debug.LogWarning("Value of bot speed set by Scriptable Object");
        }
        else
        {
            Debug.LogError("Scriptable object not found");
        }
    }

    private void OnEnable()
    {
        
        _targetFinded = false;

        if (_coroutine == null)
            _coroutine = StartCoroutine(SelectingTarget());
    }

    private void OnDisable()
    {
        _agent.enabled = false;
        
        Animator.SetBool(Run, false);
        Animator.SetFloat(Speed, 0f);

        if (_coroutine != null)
        {
            StopCoroutine(SelectingTarget());
            _coroutine = null;
        }
    }

    private void Update()
    {
        if (_targetFinded == false) return;

        if (Target != null)
            _agent.SetDestination(Target.transform.position);
    }

    private IEnumerator SelectingTarget()
    {
        while (true)
        {
            Target = StateMachine.AllUnits[Random.Range(0, StateMachine.AllUnits.Count)];

            if (Target != StateMachine)
            {
                Animator.SetBool(Run, true);
                Animator.SetFloat(Speed, 1f);
                _agent.speed = _speed;
                _agent.enabled = true;
                _targetFinded = true;
                break;
            }
            
            yield return new WaitForEndOfFrame();
        }

        _coroutine = null;
    }
}
