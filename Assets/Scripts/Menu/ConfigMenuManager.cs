using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConfigMenuManager : MonoBehaviour
{
    [SerializeField] private Slider _gameDurationSlider;
    [SerializeField] private TextMeshProUGUI _gameDurationLabel;
    [SerializeField] private Slider _spawnRateSlider;
    [SerializeField] private TextMeshProUGUI _spawnRateLabel;

    private float _gameDuration = 60f;
    private float _enemySpawnRate = 10f;
    private const float SecondsToMinutes = 60f;

    public void Start()
    {
        if (PlayerPrefs.HasKey(ProjectConstants.PlayerPrefs.GameDuration))
        {
            _gameDuration = PlayerPrefs.GetFloat(ProjectConstants.PlayerPrefs.GameDuration);
        }
        if (PlayerPrefs.HasKey(ProjectConstants.PlayerPrefs.SpawnRate))
        {
            _enemySpawnRate = PlayerPrefs.GetFloat(ProjectConstants.PlayerPrefs.SpawnRate);
        }
    }

    public void Update()
    {
        if (_gameDurationSlider)
        {
            _gameDurationSlider.value = (int)_gameDuration / SecondsToMinutes;

        }
        if (_spawnRateSlider)
        {
            _spawnRateSlider.value = _enemySpawnRate;
        }
        if (_gameDurationLabel)
        {
            var newText = $"Duração da partida: {_gameDuration / SecondsToMinutes} {(_gameDuration / SecondsToMinutes <= 1 ? "Minuto" : "Minutos")}";
            _gameDurationLabel.SetText(newText);
        }
        if (_spawnRateLabel)
        {
            var newText = $"Tempo de spawn dos inimigos: {_enemySpawnRate} Segundos";
            _spawnRateLabel.SetText(newText);
        }
    }

    public void OnPressBack()
    {
        SceneManager.LoadScene(ProjectConstants.MainMenuScene);
    }

    public void ChangeGameDuration(float amount)
    {
        if (!_gameDurationSlider)
        {
            return;
        }

        _gameDuration += amount;
        _gameDuration = Mathf.Clamp(_gameDuration, _gameDurationSlider.minValue * SecondsToMinutes, _gameDurationSlider.maxValue * SecondsToMinutes);
        PlayerPrefs.SetFloat(ProjectConstants.PlayerPrefs.GameDuration, _gameDuration);
    }
    public void ChangeSpawnRate(float amount)
    {
        if (!_spawnRateSlider)
        {
            return;
        }
        _enemySpawnRate += amount;
        _enemySpawnRate = Mathf.Clamp(_enemySpawnRate, _spawnRateSlider.minValue, _spawnRateSlider.maxValue);
        PlayerPrefs.SetFloat(ProjectConstants.PlayerPrefs.SpawnRate, _enemySpawnRate);
    }
}
