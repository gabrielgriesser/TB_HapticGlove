using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hi5_Button1 : MonoBehaviour
{
    #region attribute

    /// <summary> The animation to play on button "click" </summary>
    private new Animation animation;

    /// <summary> The object related to the effect triggered by the button trigger </summary>
    public GameObject effect;

    /// <summary> Particle system of the GameOject effect </summary>
    private ParticleSystem ps;
    #endregion

    #region monobehaviour
    void Start()
    {
        // Get particle effect to reduce its simulation time
        ps = effect.GetComponent<ParticleSystem>();
        var main = ps.main;
        main.simulationSpeed = 0.2f;

        effect.SetActive(false);
        animation = GetComponent<Animation>();
    }

    /// <summary>
    /// When an collider trigger the button collider. Play animation, if Senso are vibrating, do not resend vibrations.
    /// </summary>
    /// <param name="collide"></param>
    void OnTriggerEnter(Collider other)
    {
        animation.wrapMode = WrapMode.Once;
        animation.Play();

        if(!effect.activeSelf)
        {
            effect.SetActive(true);       
            StartCoroutine(SendBuzzGradually());
        }
        HI5.HI5_Manager.EnableBothGlovesVibration(2000, 2000);
        Debug.Log("Vibration");

    }
    #endregion

    #region coroutine
    /// <summary>
    /// Send gradually buzz command to each finger.
    /// To make an "proportion to the explosion" effect, we use a for loop to reduce gradually the magnitude
    /// </summary>
    /// <returns></returns>
    IEnumerator SendBuzzGradually()
    {
        yield return new WaitForSeconds(3f);

        if (effect.activeSelf == true)
        {
            effect.SetActive(false);
        }
    }
    #endregion
}
