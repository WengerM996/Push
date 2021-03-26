using System;
using UnityEngine;

/// <summary>
/// after a certain number of discarded units, the stick can use a super shot
/// </summary>
public class SuperStrikeAccumulator : MonoBehaviour
{
    [SerializeField] private StickSettings _stickSettings;
    [SerializeField] private int _requiredKills;
    [SerializeField] private bool _available;
    [SerializeField] private int _distanceMultiply;
    [SerializeField] private int _speedMultiply;
    [SerializeField] private Weapon _weapon;

    private int _currentKills;

    private void Awake()
    {
        if (_stickSettings != null)
        {
            _distanceMultiply = _stickSettings.DistanceMultiply;
            _speedMultiply = _stickSettings.SpeedTimeMultiply;
            _requiredKills = _stickSettings.UnitsForSuperStrike;
            Debug.LogWarning("Values of Booster distance, speed and kills required set by Scriptable Object");
        }
        else
        {
            Debug.LogError("Scriptable object not found");
        }
    }

    private void OnEnable()
    {
        _weapon.StateMachine.KillsCountUp += OnKillsCountUp;
    }

    private void OnDisable()
    {
        _weapon.StateMachine.KillsCountUp -= OnKillsCountUp;
    }

    private void OnKillsCountUp()
    {
        _currentKills++;
        
        if (_currentKills >= _requiredKills)
            _available = true;
    }

    public (int, int) Use()
    {
        if (_available)
        {
            _available = false;
            _currentKills = 0;
            
            return (_distanceMultiply, _speedMultiply);
        }

        return (1, 1);
    }
}
