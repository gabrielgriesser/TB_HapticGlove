using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to manage the button_1 (Explosion with vibrations).
/// </summary>
public class Falcon_Button1 : MonoBehaviour
{
    #region attribute

    /// <summary> The object related to the effect triggered by the button trigger </summary>
    public GameObject effect;

    /// <summary> The animation to play on button "click" </summary>
    private new Animation animation;

    /// <summary> Particle system of the GameOject effect </summary>
    private ParticleSystem ps;

    /// <summary> Check this boolean before launch trigger action (to avoid 2 trigger actions at once) </summary>
    private bool isTriggered;

    /// <summary> "strength" of the vibrations </summary>
    private int strength;


    #endregion

    #region monobehaviour

    /// <summary>
    /// Start function
    /// </summary>
    void Start()
    {
        strength = 500;

        // Get particle effect to reduce its simulation time
        GetChildParticleSystem(effect, true);

        effect.SetActive(false);
        animation = GetComponent<Animation>();
    }

    /// <summary>
    /// When an collider trigger the button collider. Play animation and send vibrations.
    /// </summary>
    /// <param name="collide"></param>
    void OnTriggerEnter(Collider other)
    {
        if (!isTriggered)
        {
            isTriggered = true;
            animation.wrapMode = WrapMode.Once;
            animation.Play();

            effect.SetActive(true);


            StartCoroutine(SendBuzzGradually(FalconUnity.getNumFalcons()));
        }

    }
    #endregion

    #region coroutine
    /// <summary>
    /// Apply gradually force to each falcons.
    /// To make an "proportion to the explosion" effect, we use a for loop to reduce gradually the magnitude
    /// </summary>
    /// <returns></returns>
    IEnumerator SendBuzzGradually(int numFalcon)
    {

        FalconUnity.applyForce(0, Random.insideUnitSphere * strength, 0.005f);
        if (numFalcon == 2)
        {
            FalconUnity.applyForce(1, Random.insideUnitSphere * strength, 0.005f);
        }

        yield return new WaitForSeconds(0.005f);


        for (float i = strength; i > 0; i -= 2f)
        {
            FalconUnity.applyForce(0, Random.insideUnitSphere * i, 0.005f);
            if (numFalcon == 2)
                FalconUnity.applyForce(1, Random.insideUnitSphere * i, 0.005f);

            yield return new WaitForSeconds(0.005f);
        }

        if (effect.activeSelf == true && isTriggered)
        {
            effect.SetActive(false);
            isTriggered = false;
        }
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

                main.simulationSpeed = 0.15f;
            }
            if (recursive)
            {
                GetChildParticleSystem(child.gameObject, true);
            }
        }
    }
    #endregion
}
