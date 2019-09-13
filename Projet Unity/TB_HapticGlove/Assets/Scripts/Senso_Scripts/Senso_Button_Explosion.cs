using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to manage the button_Explosion (Explosion with vibrations).
/// </summary>
public class Senso_Button_Explosion : MonoBehaviour
{
    #region attribute

    
    /// <summary> The sensoManager related to the project </summary>
    public GameObject SensoManager;

    /// <summary> The object related to the effect triggered by the button trigger </summary>
    public GameObject effect;

    /// <summary> The animation to play on button "click" </summary>
    private new Animation animation;

    /// <summary> The magnitude of the haptic feedback </summary>
    private Byte strength = 10;

    /// <summary> The duration of a haptic feedback pulse. </summary>
    private ushort duration = 1000;

    /// <summary> if isRunning, do not send vibrations because the Senso are unstable  </summary>
    private bool isVibrating;

    /// <summary> Particle system of the GameOject effect </summary>
    private ParticleSystem ps;

    /// <summary> Check this boolean before launch trigger action (to avoid 2 trigger actions at once) </summary>
    private bool isTriggered;

    #endregion

    #region monobehaviour
    void Start()
    {
        // Get particle effect to reduce its simulation time
        GetChildParticleSystem(effect, true);

        effect.SetActive(false);
        animation = GetComponent<Animation>();
        isVibrating = false;
    }

    /// <summary>
    /// When an collider trigger the button collider. Play animation, if Senso are vibrating, do not resend vibrations.
    /// </summary>
    void OnTriggerEnter()
    {
        if (!isTriggered)
        {
            isTriggered = true;
            animation.wrapMode = WrapMode.Once;
            animation.Play();

            effect.SetActive(true);

            if(!isVibrating)
                StartCoroutine(SendBuzzGradually());
        }

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
        isVibrating = true;
        SensoManager.GetComponent<SensoHandsController>().SendVibroToEachFinger(Senso.EPositionType.RightHand, duration, strength);
        SensoManager.GetComponent<SensoHandsController>().SendVibroToEachFinger(Senso.EPositionType.LeftHand, duration, strength);
        yield return new WaitForSeconds(0.25f);


        for (float i = 10; i > 0; i -= 0.5f)
        {
            SensoManager.GetComponent<SensoHandsController>().SendVibroToEachFinger(Senso.EPositionType.RightHand, 800, Convert.ToByte(i));
            SensoManager.GetComponent<SensoHandsController>().SendVibroToEachFinger(Senso.EPositionType.LeftHand, 800, Convert.ToByte(i));

            yield return new WaitForSeconds(0.175f);
        }

        if (effect.activeSelf == true && isTriggered)
        {
            effect.SetActive(false);
            isTriggered = false;
        }
        isVibrating = false;
    }
    #endregion

    #region method

    /// <summary>
    /// Get all particles effects to reduce their simulations times
    /// Used to coordinate the time of the explosion with the vibration time
    /// </summary>
    /// <param name="go"></param>
    /// <param name="recursive"></param>
    public void GetChildParticleSystem(GameObject go, bool recursive)
    {
        foreach (Transform child in go.transform)
        {
            if (child.GetComponent<ParticleSystem>())
            {
                ps = child.GetComponent<ParticleSystem>();
                var main = ps.main;
                main.simulationSpeed = 0.35f;
            }
            if (recursive)
            {
                GetChildParticleSystem(child.gameObject, true);
            }
        }
    }

    #endregion
}
