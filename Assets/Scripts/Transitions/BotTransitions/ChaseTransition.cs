using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// here we are waiting for the fall animation to end to go to the next state
/// </summary>
/// 
public class ChaseTransition : Transition
{
    private Animator _animator;
    private Coroutine _coroutine;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    protected override void Enable()
    {
        if (_coroutine == null)
            _coroutine = StartCoroutine(Waiter());
    }

    private IEnumerator Waiter()
    {
        var time = _animator.GetCurrentAnimatorStateInfo(0).length + _animator.GetNextAnimatorStateInfo(0).length;

        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        _coroutine = null;
        NeedTransit = true;
        
    }
}
