using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class used to load calibration scene when button is trigger 
/// </summary>
public class Hi5_Button_Calibration : MonoBehaviour
{
    #region attribute
    /// <summary> Used to play animation when trigger enter and exit </summary>
    private new Animation animation;

    private bool isTrigger;
    #endregion

    #region monobehaviour
    
    /// <summary>
    /// Start method
    /// </summary>
    void Start()
    {
		if(GetComponent<Animation>() != null)
        	this.animation = GetComponent<Animation>();
        isTrigger = false;
    }

    /// <summary>
    /// On trigger
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if(!isTrigger)
        {
            isTrigger = true;
            animation.wrapMode = WrapMode.Once;
            animation.Play("ButtonDown");
        }
    }

    /// <summary>
    /// When trigger exit, load scene
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit(Collider other)
    {
        if(isTrigger)
        {
            animation.wrapMode = WrapMode.Once;
            animation.Play("ButtonUp");
            isTrigger = false;
            StartCoroutine(loadScene());
        }
    }
    #endregion

    #region coroutine
    /// <summary>
    /// Load calibration scene
    /// </summary>
    /// <returns></returns>
    private IEnumerator loadScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Calibration");
    }
    #endregion
}
