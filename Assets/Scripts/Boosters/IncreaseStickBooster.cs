using System.Collections;
using UnityEngine;
    
/// <summary>
/// booster increasing the size of the stick for a certain time
/// </summary>

public class IncreaseStickBooster : Booster
{
    [SerializeField] private StickSettings _stickSettings;
    [SerializeField] private Vector3 _modifier;
    private Coroutine _coroutine;

    protected override void OnAwake()
    {
        if (_stickSettings != null)
        {
            _modifier = _stickSettings.SizeModifier;
            Debug.LogWarning("Value of Weapon Increase Size set by Scriptable Object");
        }
        else
        {
            Debug.LogError("Scriptable object not found");
        }
    }

    protected override void Apply(StateMachine stateMachine)
    {
        if (_coroutine == null)
            _coroutine = StartCoroutine(Action(stateMachine));
    }

    private IEnumerator Action(StateMachine stateMachine)
    {

        var currentSize = stateMachine.CurrentWeapon.transform.localScale;
        var maxSize = stateMachine.CurrentWeapon.MaxSize;
        var newSize = currentSize + _modifier;

        stateMachine.CurrentWeapon.transform.localScale = EqualMax(newSize, maxSize);
        
        var time = 0f;

        while (time < _duration)
        {
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        stateMachine.CurrentWeapon.transform.localScale = stateMachine.CurrentWeapon.StartSize;
        _coroutine = null;
        
        Destroy(gameObject);
    }

    private Vector3 EqualMax(Vector3 newSize, Vector3 maxSize)
    {
        if (newSize.x <= maxSize.x &&
            newSize.y <= maxSize.y &&
            newSize.z <= maxSize.z)
        {
            return newSize;
        }

        return maxSize;
    }
}
