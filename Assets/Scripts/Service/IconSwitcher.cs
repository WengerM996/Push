using UnityEngine;

/// <summary>
/// switches windows when needed
/// </summary>

public class IconSwitcher : MonoBehaviour
{
    [SerializeField] private GameOverForm _gameOverFormTemplate;
    [SerializeField] private LevelForm _levelFormTemplate;

    private GameOverForm _gameOverForm;
    private LevelForm _levelForm;

    private void OnEnable()
    {
        DeadZone.PlayerFallen += OnPlayerFallen;
        WinChecker.Winner += OnPlayerWin;
    }

    private void OnDisable()
    {
        DeadZone.PlayerFallen -= OnPlayerFallen;
        WinChecker.Winner -= OnPlayerWin;
    }

    private void Awake()
    {
        if (_levelForm == null)
            _levelForm = Instantiate(_levelFormTemplate);
    }

    private void OnPlayerFallen()
    {
        if (_gameOverForm == null)
            _gameOverForm = Instantiate(_gameOverFormTemplate);
    }

    private void OnPlayerWin()
    {
        if (_gameOverForm == null)
            _gameOverForm = Instantiate(_gameOverFormTemplate);
    }
}
