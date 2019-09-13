using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to manage the button_Reset (Reset of all interactables objects).
/// </summary>
public class Senso_Button_Reset : MonoBehaviour
{
    #region attribute
    /// <summary> GameObject to play </summary>
    public GameObject effectToShow;
    #endregion

    #region monobehaviour
    /// <summary>
    /// On trigger enter, active gameObject
    /// </summary>
    void OnTriggerEnter()
    {
        if (effectToShow && !effectToShow.activeSelf)
        {
            effectToShow.SetActive(true);
        }
    }
    #endregion
}
