using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Class used to manage the button_2 (Calibration).
/// </summary>
public class Senso_ButtonCalibration : MonoBehaviour
{
    #region attribute
    /// <summary> The effect to show when button is triggered </summary> 
    public GameObject effectToShow;
    /// <summary> Used to play animation when trigger enter and exit </summary>
    private new Animation animation;

    private bool isTrigger;
    #endregion

    #region monobehaviour
    void Start()
    {
        this.SetEffectObject(false);
        this.animation = GetComponent<Animation>();
        isTrigger = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isTrigger)
        {
            if (this.effectToShow.activeSelf == false)
                this.SetEffectObject(true);
            else
                this.SetEffectObject(false);
            isTrigger = true;
        }

        animation.wrapMode = WrapMode.Once;
        animation.Play("ButtonDown");
    }

    void OnTriggerExit(Collider other)
    {
        animation.wrapMode = WrapMode.Once;
        animation.Play("ButtonUp");
        isTrigger = false;
    }
    #endregion

    #region method
    /// <summary>
    /// Enable/disable the "effectToShow" Gameobject
    /// </summary>
    /// <param name="active"></param>
    public void SetEffectObject(bool active)
    {
        if (this.effectToShow != null)
        {
            this.effectToShow.gameObject.SetActive(active);
        }
    }
    #endregion
}
