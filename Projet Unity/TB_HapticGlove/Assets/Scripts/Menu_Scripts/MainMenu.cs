using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manage game exit
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Exit game
    /// </summary>
    /// <param name="sceneName"></param>
    public void QuitGame(string sceneName)
    {
        Application.Quit();
    }
}
