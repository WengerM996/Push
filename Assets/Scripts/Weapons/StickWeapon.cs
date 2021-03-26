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
    [SerializeField] private CapsuleCollider _mainStick;
    [SerializeField] private CapsuleCollider _shootingStick;
    [SerializeField] private Push _push;
    [SerializeField] private float _shotDistance;
    [SerializeField] private float _shotTime;
    
    [SerializeField] private SuperStrikeAccumulator _strikeAccumulator;

    private Coroutine _coroutine;
    private Tweener _shooting;


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
        
        _push.StateMachine = StateMachine;
    }

    public override void Use()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(Action());
        }
    }

    private IEnumerator Action()
    {
        _mainStick.isTrigger = true;
        _shootingStick.isTrigger = false;
        _push.Enabled = true;

        var (distanceModifier, speedModifier) = _strikeAccumulator.Use();
        Shot(distanceModifier, speedModifier);
        
        float counter = 0;

        while (counter < _shotTime * 2)
        {
            counter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        _shootingStick.isTrigger = true;
        _mainStick.isTrigger = false;
        _push.Enabled = false;

        _coroutine = null;
    }

    private void Shot(int distanceMultiply, int speedMultiply)
    {
        if (_shooting == null)
            _shooting = _shootingStick.gameObject.transform.DOLocalMoveX(-_shotDistance * distanceMultiply, _shotTime / speedMultiply)
                .SetLoops(2, LoopType.Yoyo).SetAutoKill(false);
        else
        {
            var newDistance = Vector3.left * (_shotDistance * distanceMultiply);
            var newDuration = _shotTime / speedMultiply;
            _shooting.ChangeEndValue(newDistance, newDuration);
            _shooting.Restart();
        }
    }

    private void OnDestroy()
    {
        _shooting.Kill();
    }
}
