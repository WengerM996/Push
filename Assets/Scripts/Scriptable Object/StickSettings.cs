using UnityEngine;

[CreateAssetMenu(fileName = "New Stick Settings", menuName = "Preferences/Stick Settings")]
public class StickSettings : ScriptableObject
{
    [SerializeField] private Weapon _weaponTemplate;
    [SerializeField] private Vector3 _startSize;
    [SerializeField] private Vector3 _sizeModifier;
    [SerializeField] private Vector3 _maxSize;
    [SerializeField] private float _startDistance;
    [SerializeField] private float _shotTime;
    [SerializeField] private int _speedTimeMultiply;
    [SerializeField] private int _distanceMultiply;
    [SerializeField] private int _unitsForSuperStrike;

    public Weapon WeaponTemplate => _weaponTemplate;

    public Vector3 StartSize => _startSize;

    public Vector3 SizeModifier => _sizeModifier;

    public Vector3 MaxSize => _maxSize;

    public float StartDistance => _startDistance;

    public float ShotTime => _shotTime;

    public int SpeedTimeMultiply => _speedTimeMultiply;

    public int DistanceMultiply => _distanceMultiply;

    public int UnitsForSuperStrike => _unitsForSuperStrike;
}
