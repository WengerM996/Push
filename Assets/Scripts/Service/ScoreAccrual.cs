using System;
using UnityEngine;


/// <summary>
/// gives points for picking up crystals and for throwing off a unit
/// </summary>
public class ScoreAccrual : MonoBehaviour
{
    [SerializeField] private GeneralSettings _generalSettings;
    [SerializeField] private int _costUnit;
    [SerializeField] private int _costCrystal;
    private void OnEnable()
    {
        Crystal.Picked += OnCrystalPicked;
        DeadZone.Fallen += OnFallen;
    }

    private void OnDisable()
    {
        Crystal.Picked -= OnCrystalPicked;
        DeadZone.Fallen -= OnFallen;
    }

    private void Awake()
    {
        if (_generalSettings != null)
        {
            _costUnit = _generalSettings.CostUnit;
            _costCrystal = _generalSettings.CostCrystal;
            Debug.LogWarning("Values of crystals and units set by Scriptable Object");
        }
        else
        {
            Debug.LogError("Scriptable object not found");
        }
    }

    private void OnCrystalPicked(int score, StateMachine stateMachine)
    {
        score = (score == 0) ? _costCrystal : score;
        stateMachine.AddScore(score);
    }

    private void OnFallen(StateMachine fallen, StateMachine killer)
    {
        if (killer != null)
        {
            killer.AddKill();
            killer.AddScore(_costUnit);
        }
    }
}
