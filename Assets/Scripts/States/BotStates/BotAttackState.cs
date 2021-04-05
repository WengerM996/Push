using System.Collections;
using UnityEngine;

/// <summary>
/// being in this state, the bot attacks using weapons
/// </summary>

public class BotAttackState : State
{
    [SerializeField] private StickSettings _stickSettings;
    [SerializeField] private ChaseState _chaseState;
    [SerializeField] private Weapon _weaponTemplate;
    [SerializeField] private Vector3 _weaponRotation;
    [SerializeField] private Transform _weaponPosition;
    [SerializeField] private float _speedRotation; 

    private Weapon _weapon;
    private Coroutine _coroutine;

    protected override void OnAwake()
    {
        if (_stickSettings != null)
        {
            _weaponTemplate = _stickSettings.WeaponTemplate;
            Debug.LogWarning("Value of Weapon Template for Bots set by Scriptable Object");
        }
        else
        {
            Debug.LogError("Scriptable object not found");
        }
        
        //CreateWeapon();
    }
    
    private void CreateWeapon()
    {
        if (_weapon != null) return;
        
        _weapon = Instantiate(_weaponTemplate, _weaponPosition.position, Quaternion.Euler(_weaponRotation), transform);
        StateMachine.CurrentWeapon = _weapon;
    }

    private void OnEnable()
    {
        return;
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(Action());
        }
    }

    private IEnumerator Action()
    {
        while (enabled)
        {
            if (_chaseState.Target == null) break;
            
            StateMachine.LookAtXZ(transform, _chaseState.Target.transform.position, _speedRotation);
            _weapon.Use();
            yield return new WaitForEndOfFrame();
        }

        _coroutine = null;
    }
}
