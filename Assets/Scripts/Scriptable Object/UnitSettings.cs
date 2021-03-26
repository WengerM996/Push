using UnityEngine;

[CreateAssetMenu(fileName = "New Unit Settings", menuName = "Preferences/Unit Settings")]
public class UnitSettings : ScriptableObject
{
    [SerializeField] private float _speedMovement;
    [SerializeField] private float _forcePush;

    public float SpeedMovement => _speedMovement;

    public float ForcePush => _forcePush;
}
