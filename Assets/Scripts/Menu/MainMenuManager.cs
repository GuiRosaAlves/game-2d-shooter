using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MainMenuManager : MonoBehaviour
{
    public void OnPressPlayGame()
    {
        SceneManager.LoadScene(ProjectConstants.GameScene);
    }
    public void OnPressConfig()
    {
        SceneManager.LoadScene(ProjectConstants.ConfigScene);
    }
}
