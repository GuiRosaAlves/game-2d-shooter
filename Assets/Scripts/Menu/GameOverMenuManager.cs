using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    private void Awake()
    {
        var score = PlayerPrefs.GetInt(ProjectConstants.PlayerPrefs.PlayerScore, 0);
        _scoreText.text = $"Sua Pontuacao: {score}";
    }

    public void OnPressPlayAgain()
    {
        SceneManager.LoadScene(ProjectConstants.GameScene);
    }

    public void OnPressMainMenu()
    {
        SceneManager.LoadScene(ProjectConstants.MainMenuScene);
    }
}
