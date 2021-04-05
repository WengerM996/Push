using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// here is the calculation of shooting with a stick
/// </summary>
/// 
public class StickWeapon : Weapon
{
    [SerializeField] private GameObject _stick;
    [SerializeField] private GameObject _shootingStick;
    [SerializeField] private Push _push;
    [SerializeField] private float _shotDistance;
    [SerializeField] private float _shotTime;
    
    [SerializeField] private SuperStrikeAccumulator _strikeAccumulator;
    
    private Tweener _shootingTweener;
    private bool _using;

    private Vector3[] _path;
    private Rigidbody _rbShotStick;
    private Collider _stickCollider;
    private Collider _shotStickCollider;

    protected override void OnAwake()
    {
        if (_stickSettings != null)
        {
            _shotDistance = _stickSettings.StartDistance;
            _shotTime = _stickSettings.ShotTime;
            Debug.LogWarning("Value of Weapon Start Shot Distance set by Scriptable Object");
        }
        else
        {
            Debug.LogError("Scriptable object not found");
        }

        _path = new[]
        {
            Vector3.left * _shotDistance, Vector3.zero
        };

        _rbShotStick = _shootingStick.GetComponent<Rigidbody>();

        _stickCollider = _stick.GetComponent<Collider>();
        _shotStickCollider = _shootingStick.GetComponent<Collider>();

        _push.StateMachine = StateMachine;
    }
    

    public override void Use()
    {
        if (_using) return;
        _using = true;
        
        _rbShotStick.isKinematic = false;
        _rbShotStick.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        if (_shootingTweener == null)
        {
            _shootingTweener = _rbShotStick.DOLocalPath(_path, _shotTime * _path.Length)
                .SetRelative()
                .OnComplete(OnTweenerComplete)
                .SetAutoKill(false);
        }
        else
        {
            _shootingTweener.Restart();
        }
    }

    private void OnTweenerComplete()
    {
        _rbShotStick.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        _rbShotStick.isKinematic = true;
        _using = false;
    }

    private void Update()
    {
        _shootingStick.transform.rotation = _stick.transform.rotation;
    }

    private void OnDestroy()
    {
        _shootingTweener.Kill();
    }
}
