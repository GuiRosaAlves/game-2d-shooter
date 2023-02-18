using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSessionManager : MonoBehaviour
{
    public static GameSessionManager instance { get; private set; }

    [SerializeField] private PlayerShip _playerShip;
    [Tooltip("Duration in seconds")] [SerializeField] private float _sessionDuration = 180f;

    private float _timer;
    public event System.Action GameOverEvent;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        instance = this;

        if (!PlayerPrefs.HasKey(ProjectConstants.PlayerPrefs.GameDuration))
        {
            return;
        }

        _sessionDuration = PlayerPrefs.GetFloat(ProjectConstants.PlayerPrefs.GameDuration);
    }

    private void Start()
    {
        if (!_playerShip)
        {
            return;
        }
        _playerShip.PlayerShipDestroyed += OnGameOver;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _sessionDuration)
        {
            OnGameOver();
        }
    }

    private void OnGameOver()
    {
        Debug.Log("GAME OVER!");

        GameOverEvent?.Invoke();
    }
}
