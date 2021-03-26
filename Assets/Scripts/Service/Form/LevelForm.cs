using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelForm : MonoBehaviour
{
    public static event UnityAction PlayClicked;

    public void OnPlayClick()
    {
        PlayClicked?.Invoke();
        Destroy(gameObject);
    }
}
