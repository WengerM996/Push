using System;
using UnityEngine;

/// <summary>
/// local crystal spawner. spawns crystals at a certain distance from each other
/// </summary>

public class CrystalSpawner : MonoBehaviour
{
    [SerializeField] private GeneralSettings _generalSettings;
    [SerializeField] private Crystal _crystalTemplate;
    [SerializeField] private float _distanceZ;
    [SerializeField] private float _distanceX;
    [SerializeField] private int _linesCount;
    [SerializeField] private int _elementsInLine;
    [SerializeField] private bool _customSettings;
    [SerializeField] private int _count;

    public int Count
    {
        get => _count;
        set
        {
            if (_customSettings == false)
                _count = value;
        }
    }

    public float DistanceX
    {
        get => _distanceX;
        set
        {
            if (_customSettings == false)
                _distanceX = value;
        }
    }
    
    public float DistanceZ
    {
        get => _distanceZ;
        set
        {
            if (_customSettings == false)
                _distanceZ = value;
        }
    }

    public Crystal CrystalTemplate
    {
        get => _crystalTemplate;
        set
        {
            if (_customSettings == false)
                _crystalTemplate = value;
        }
    }

    private void Awake()
    {
        if (_generalSettings != null)
        {
            _distanceX = _generalSettings.DistanceBetweenCrystals;
            _distanceZ = _generalSettings.DistanceBetweenCrystals;
            Debug.LogWarning("Values of distance between crystals set by Scriptable Object");
        }
        else
        {
            Debug.LogError("Scriptable object not found");
        }
    }

    public void Spawn()
    {
        Build();
    }

    private void Build()
    {
        _elementsInLine = _count / _linesCount;
        
        var startPosition = transform.position;
        var currentPosition = startPosition;
        
        for (int i = 0; i < _linesCount; i++)
        {

            for (int j = 0; j < _elementsInLine; j++)
            {
                var crystal = Instantiate(_crystalTemplate, currentPosition, Quaternion.identity, transform);
                
                currentPosition += transform.forward * _distanceZ;
                //currentPosition.z += _distanceZ;
            }

            currentPosition += transform.right * _distanceX;
            currentPosition.z = startPosition.z;

            //currentPosition.x += _distanceX;
            //currentPosition.z = startPosition.z;
        }
        
    }
}
