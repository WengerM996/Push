using UnityEngine;

/// <summary>
/// frame for all weapons
/// </summary>
/// 
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected StickSettings _stickSettings;
    //protected Animation Animation { get; private set; }
    public StateMachine StateMachine { get; private set; }
    public Vector3 StartSize { get; private set; }
    public Vector3 MaxSize { get; private set; }

    private void Awake()
    {
        if (_stickSettings != null)
        {
            StartSize = _stickSettings.StartSize;
            MaxSize = _stickSettings.MaxSize;
            Debug.LogWarning("Value of Weapon Start Scale set by Scriptable Object");
        }
        else
        {
            Debug.LogError("Scriptable object not found");
        }
        
        StateMachine = GetComponentInParent<StateMachine>();
        StartSize = transform.localScale;
        OnAwake();
    }

    protected abstract void OnAwake();

    public abstract void Use();
}
