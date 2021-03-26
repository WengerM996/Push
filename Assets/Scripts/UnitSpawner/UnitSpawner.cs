using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// creates units at the beginning of the level
/// </summary>
/// 
public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private PlayerStateMachine _player;
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private BotStateMachine _bot;
    [SerializeField] private int _botCount;
    [SerializeField] private List<Transform> _botSpawnPoints;
    [SerializeField] private List<StateMachine> all;
    
    public PlayerStateMachine Player { get; private set; }
    public List<StateMachine> AllUnits { get => all;
        private set => all = value;
    }

    public static event UnityAction<StateMachine> PlayerCreated;

    public event UnityAction UnitsListChanged;

    private void OnEnable()
    {
        DeadZone.Fallen += OnFallen;
    }

    private void OnDisable()
    {
        DeadZone.Fallen -= OnFallen;
    }

    private void Awake()
    {
        AllUnits = new List<StateMachine>();
        Init();
    }

    private void Init()
    {
        _botCount = Mathf.Clamp(_botCount, 0, _botSpawnPoints.Count);
        Spawn();
    }

    private void Spawn()
    {
        if (Player == null)
        {
            Player = Instantiate(_player, _playerSpawnPoint.position, Quaternion.identity, transform);
            Player.AllUnits = AllUnits;
            AllUnits.Add(Player);
            
            PlayerCreated?.Invoke(Player);
        }

        for (int i = 0; i < _botCount; i++)
        {
            var bot = Instantiate(_bot, _botSpawnPoints[i].position, Quaternion.identity, transform);
            bot.AllUnits = AllUnits;
            AllUnits.Add(bot);
        }
    }
    
    private void OnFallen(StateMachine fallen, StateMachine killer = null)
    {
        AllUnits.Remove(fallen);
        
        UnitsListChanged?.Invoke();
    }
}
