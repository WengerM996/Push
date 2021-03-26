using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// level settings, as well as its state. More parameters can be added in the future
/// </summary>

public class Level : MonoBehaviour
{
    [SerializeField] private bool _complete;

    public bool Complete
    {
        get => _complete;
        private set => _complete = value;
    }
}
