using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// basic state machine framework for all units
/// </summary>

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public abstract class StateMachine : MonoBehaviour, IDamageable
{
    [SerializeField] private string _name;
    [SerializeField] private State _firstState;
    [SerializeField] private ScoreView _scoreViewTemplate;

    private State _currentState;
    private Rigidbody _rigidBody;
    private Animator _animator;
    protected int _score;
    private ScoreView _scoreView;
    private Coroutine _coroutine;
    private float _markTime = 3f;
    private int _countKills;

    public List<StateMachine> AllUnits { get; set; }
    public Weapon CurrentWeapon { get; set; }
    public StateMachine LastTouch { get; set; } 

    public event UnityAction Damaged;

    public event UnityAction KillsCountUp;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _score = 0;
        View.Instance.ScoreView.text = "Player: " + _score;
        _currentState = _firstState;
        _currentState.Enter(this, _rigidBody, _animator);
    }

    private void Update()
    {
        if (_currentState == null) return;

        var nextState = _currentState.GetNextState();
        if (nextState != null)
        {
            Transit(nextState);
        }
    }

    private void Transit(State nextState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }

        _currentState = nextState;

        if (_currentState != null)
        {
            _currentState.Enter(this, _rigidBody, _animator);
        }
    }

    public void ApplyHit(StateMachine attacker)
    {
        Debug.LogWarning(name + " Attack By " );
        
        if (_coroutine == null)
            _coroutine = StartCoroutine(Mark(attacker));
        
        //_navMeshAgent.enabled = false;
        LookAtXZ(transform, attacker.transform.position);
        Damaged?.Invoke();
    }

    private IEnumerator Mark(StateMachine attacker)
    {
        LastTouch = attacker;
        var time = 0f;
        
        while (time < _markTime)
        {
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        LastTouch = null;
        _coroutine = null;
    }

    public void AddKill()
    {
        _countKills++;
        KillsCountUp?.Invoke();
    }

    public abstract void AddScore(int score);
    // {
    //     
    //
    //     // if (_scoreView == null)
    //     // {
    //     //     _scoreView = View.AddScoreView(_scoreViewTemplate);
    //     // }
    //     //
    //     // _scoreView.View.text = _name + " " + _score;
    // }

    public static void LookAtXZ(Transform transform, Vector3 point, float speed)
    {
        var direction = (point - transform.position).normalized;
        direction.y = 0f;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), speed);
    }
    
    public static void LookAtXZ(Transform transform, Vector3 point)
    {
        var direction = (point - transform.position).normalized;
        direction.y = 0f;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
