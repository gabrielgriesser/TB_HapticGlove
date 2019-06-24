using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to manage the button_2 (Reset of all interactables objects).
/// </summary>
public class Senso_Button2 : MonoBehaviour
{
    /// <summary> GameObject to play </summary>
    public GameObject effectToShow;

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
}
