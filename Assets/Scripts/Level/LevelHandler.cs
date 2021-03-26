using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// level handler, starts the last level not passed
/// </summary>

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private List<Level> _levels;

    private Level _currentLevel;

    public static event UnityAction LevelCreated;

    private void OnEnable()
    {
        LevelForm.PlayClicked += OnPlayClick;
        GameOverForm.ResetClicked += OnResetClicked;
        DeadZone.PlayerFallen += DestroyLevel;
        WinChecker.Winner += DestroyLevel;
    }

    private void OnDisable()
    {
        LevelForm.PlayClicked -= OnPlayClick;
        GameOverForm.ResetClicked -= OnResetClicked;
        DeadZone.PlayerFallen -= DestroyLevel;
        WinChecker.Winner -= DestroyLevel;
    }

    private void OnPlayClick()
    {
        Launch();
    }

    private void Launch()
    {
        foreach (var level in _levels)
        {
            if (level.Complete == false)
            {
                _currentLevel = Instantiate(level);
                LevelCreated?.Invoke();
                break;
            }
        }
    }

    private void DestroyLevel()
    {
        if (_currentLevel != null)
            Destroy(_currentLevel.gameObject);
    }

    private void OnResetClicked()
    {
        DestroyLevel();
        Launch();
    }
}
