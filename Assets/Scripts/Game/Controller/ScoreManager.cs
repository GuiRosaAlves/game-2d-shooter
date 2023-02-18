using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance { get; private set; }

    [SerializeField] private int _score = 0;
    [SerializeField] private PlayerShip _playerShip;
    [SerializeField] private TextMeshProUGUI _scoreText;

    public event System.Action<int> ScoredPoints;
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        instance = this;
    }
    private void Start()
    {
        EnemyManager.instance.EnemySpawned += OnEnemySpawn;
        ScoredPoints += OnScorePoints;
    }
    private void OnEnemySpawn(Enemy enemy)
    {
        enemy.EnemyShipDestroyed += OnEnemyShipDestroyed;
    }
    private void OnEnemyShipDestroyed(Enemy enemy)
    {
        ScoredPoints?.Invoke(enemy.ScorePoints);
    }

    private void OnScorePoints(int scorePoints)
    {
        if (!_scoreText)
        {
            return;
        }
        _score += scorePoints;
        _scoreText.text = $"Score: {_score}";
    }
}
