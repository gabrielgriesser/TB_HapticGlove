using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class used to manage the button_Menu (Back to main menu).
/// </summary>
public class Button_Menu : MonoBehaviour
{
    #region attribute

    /// <summary> The animation to play on button "click" </summary>
    private new Animation animation;

    #endregion

    #region monobehaviour
    /// <summary>
    /// Start function
    /// </summary>
    void Start()
    {
        animation = GetComponent<Animation>();
    }

    /// <summary>
    /// Update every frame
    /// </summary>
    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            StartCoroutine(WaitToLoad());
        }
    }
    
    /// <summary>
    /// When an collider trigger the button collider. Play animation, if Senso are vibrating, do not resend vibrations.
    /// </summary>
    /// <param name="collide"></param>
    void OnTriggerEnter(Collider other)
    {
        animation.wrapMode = WrapMode.Once;
        animation.Play();      
        StartCoroutine(WaitToLoad());
    }
    #endregion
   
    #region coroutine
    IEnumerator WaitToLoad()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(0);

    }
    #endregion
}
