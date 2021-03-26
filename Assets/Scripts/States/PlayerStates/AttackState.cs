using System;
using System.Collections;
using UnityEngine;

public class AttackState : State
{
    [SerializeField] private StickSettings _stickSettings;
    [SerializeField] private Weapon _weaponTemplate;
    [SerializeField] private Vector3 _weaponRotation;
    [SerializeField] private Transform _weaponPosition;

    private Weapon _weapon;

    protected override void OnAwake()
    {
        if (_stickSettings != null)
        {
            _weaponTemplate = _stickSettings.WeaponTemplate;
            Debug.LogWarning("Value of Weapon Template for Player set by Scriptable Object");
        }
        else
        {
            Debug.LogError("Scriptable object not found");
        }
        
        CreateWeapon();
    }
    
    private void CreateWeapon()
    {
        if (_weapon != null) return;
        
        _weapon = Instantiate(_weaponTemplate, _weaponPosition.position, Quaternion.Euler(_weaponRotation), transform);
        
        StateMachine.CurrentWeapon = _weapon;
    }

    private void OnEnable()
    {
        _weapon.Use();
    }
}
