using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// the main spawner who controls the other crystal spawners in the level
/// </summary>

public class CrystalGenerator : MonoBehaviour
{
    [SerializeField] private Crystal _crystalTemplate;
    [SerializeField] private List<CrystalSpawner> _spawners;
    [SerializeField] private int _totalCrystals;
    [SerializeField] private float _distanceZ;
    [SerializeField] private float _distanceX;

    private void Awake()
    {
        Generate();
    }

    private void Generate()
    {
        var crystalsPerSpawner = _totalCrystals / _spawners.Count;
        
        foreach (var spawner in _spawners)
        {
            spawner.CrystalTemplate = _crystalTemplate;
            spawner.Count = crystalsPerSpawner;
            spawner.DistanceX = _distanceX;
            spawner.DistanceZ = _distanceZ;
            spawner.Spawn();
        }
    }
}
