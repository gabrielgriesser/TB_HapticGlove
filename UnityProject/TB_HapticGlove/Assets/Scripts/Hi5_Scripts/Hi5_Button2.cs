using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hi5_Button2 : MonoBehaviour
{
    #region attribute
    /// <summary> Used to play animation when trigger enter and exit </summary>
    private new Animation animation;

    private bool isTrigger;
    #endregion

    #region monobehaviour
    void Start()
    {
		if(GetComponent<Animation>() != null)
        	this.animation = GetComponent<Animation>();
        isTrigger = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(!isTrigger)
        {
            isTrigger = true;
            animation.wrapMode = WrapMode.Once;
            animation.Play("ButtonDown");
        }
    }

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
    private IEnumerator loadScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Calibration");
    }
    #endregion
}
