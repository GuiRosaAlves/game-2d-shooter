using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance { get; private set; }

    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Enemy[] _enemyPrefabs;
    [SerializeField] private float _spawnRate = 2f;
    [SerializeField] private int _spawnQuantity = 2;
    [SerializeField] private int _maxEnemies = 10;
    [SerializeField] private PlayerShip _targetPlayer;

    private List<Enemy> _spawnedEnemies = new List<Enemy>();
    private float _timer;
    public event System.Action<Enemy> EnemySpawned;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        instance = this;

        if (PlayerPrefs.HasKey(ProjectConstants.PlayerPrefs.SpawnRate))
        {
            _spawnRate = PlayerPrefs.GetFloat(ProjectConstants.PlayerPrefs.SpawnRate);
        }

        Debug.Log("TESTE!!! " + _spawnRate);
        EnemySpawned += OnEnemySpawned;
        _targetPlayer.PlayerShipDestroyed += OnPlayerShipDestroyed;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _spawnRate)
        {
            SpawnEnemy();
        }
    }
    private void SpawnEnemy()
    {
        _timer = 0f;

        for (int i = 0; i < _spawnQuantity; i++)
        {
            if (_spawnedEnemies.Count >= _maxEnemies)
            {
                break;
            }
            var randomEnemyIndex = Random.Range(0, _enemyPrefabs.Length);
            var randomSpawnPointIndex = Random.Range(0, _spawnPoints.Length);
            var newEnemy = Instantiate(_enemyPrefabs[randomEnemyIndex],
                _spawnPoints[randomSpawnPointIndex].position, Quaternion.identity);
            Debug.Log(newEnemy.transform.position);
            EnemySpawned?.Invoke(newEnemy);
        }
    }
    private void OnEnemySpawned(Enemy newEnemy)
    {
        _spawnedEnemies.Add(newEnemy);
        newEnemy.EnemyShipDestroyed += OnEnemyShipDestroyed;
        newEnemy.Target = _targetPlayer.transform;
        Debug.Log("Teste");
    }

    private void OnEnemyShipDestroyed(Enemy enemy)
    {
        _spawnedEnemies.Remove(enemy);
    }
    private void OnPlayerShipDestroyed()
    {
        Debug.Log("Enemy AI Stopped!");
        if (_spawnedEnemies == null)
        {
            return;
        }
        foreach (var enemy in _spawnedEnemies)
        {
            enemy.Target = null;
        }
    }
}
