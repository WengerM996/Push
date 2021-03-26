using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New General Settings", menuName = "Preferences/General Settings")]
public class GeneralSettings : ScriptableObject
{
    [SerializeField] private int _costUnit;
    [SerializeField] private int _costCrystal;
    [SerializeField] private float _distanceBetweenCrystals;

    public int CostUnit => _costUnit;
    public int CostCrystal => _costCrystal;
    public float DistanceBetweenCrystals => _distanceBetweenCrystals;
    
}
