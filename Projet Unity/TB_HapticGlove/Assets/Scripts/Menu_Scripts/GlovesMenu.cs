using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SFB;

/// <summary>
/// Manage scene loading
/// </summary>
public class GlovesMenu : MonoBehaviour
{

    private string sceneName;
    private string falconServerPath, sensoScriptPath;

    /// <summary>
    /// Load scene
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Set tracker left ID
    /// </summary>
    /// <param name="id"></param>
    public void SetTrackerLeft(int id)
    {
        DemoParameters.TrackerLeft = id;
    }

    /// <summary>
    /// Set tracker right ID
    /// </summary>
    /// <param name="id"></param>
    public void SetTrackerRight(int id)
    {
        DemoParameters.TrackerRight = id;
    }

    /// <summary>
    /// Set name of the scene to be loaded
    /// </summary>
    /// <param name="name"></param>
    public void SetSceneName(string name)
    {
        sceneName = name;
    }

    /// <summary>
    /// Open a StandaloneFileBrowser (imported from https://github.com/gkngkc/UnityStandaloneFileBrowser)
    /// Used to search FalconServer. If path is correct, server will be automatically launched at the start of the falcon scene
    /// Unfortnably, it will not be recharged correctly if we restart the falcon scene without closing the game.
    /// </summary>
    public void SearchFalconServer()
    {
        var extensions = new [] {
            new ExtensionFilter("Executables Files", "exe"),
        };
        var path = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, true);
        if(path.Length != 0)
        {
            falconServerPath = path[0];
            DemoParameters.FalconServerPath = falconServerPath;
        }
    }

    /*
    ///
    /// Not used because script is launched from Unity Folder and, therefore, does not find the Senso_UI and Senso_BLE_Server applications
    /// 
    public void SearchSensoScript()
    {
        var extensions = new [] {
            new ExtensionFilter("Scripts Files", "cmd"),
        };
        var path = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, true);
        if(path.Length != 0)
        {
            sensoScriptPath = path[0];
            DemoParameters.SensoScriptPath = sensoScriptPath;
        }
    }
    */
}
