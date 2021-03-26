
using System;
using UnityEngine;
using UnityEngine.Events;

public class WinChecker : MonoBehaviour
{
    [SerializeField] private UnitSpawner _unitSpawner;

    public static event UnityAction Winner;

    private void OnEnable()
    {
        _unitSpawner.UnitsListChanged += OnUnitsListChanged;
    }

    private void OnDisable()
    {
        _unitSpawner.UnitsListChanged -= OnUnitsListChanged;
    }

    private void OnUnitsListChanged()
    {
        if (_unitSpawner.AllUnits.Count > 1) return;

        if (_unitSpawner.AllUnits[0] as PlayerStateMachine != null)
        {
            Winner?.Invoke();
        }
       
    }
}
