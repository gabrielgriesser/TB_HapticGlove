using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to manage the button_3 (Explosion with vibrations).
/// </summary>
public class SenseGlove_Button_Explosion : MonoBehaviour
{

    #region attribute

    /// <summary> Animation to play when button is triggered </summary>
    private new Animation animation;

    /// <summary> A direct link to glove right and left </summary>
    public SenseGlove_Object senseGloveRight, senseGloveLeft;

    /// <summary> The object related to the effect triggered by the button trigger </summary>
    public GameObject effect;

    /// <summary> The magnitude of the haptic Feedback </summary>
    private int hapticForce = 100;

    /// <summary> The duration of a haptic feedback pulse. </summary>
    private int hapticDuration = 800; //don't show if looping.

    /// <summary> Which fingers to apply the Haptic feedback to </summary>
    private readonly bool[] whichFingers = new bool[5] { true, true, true, true, true };

    /// <summary> Particle system of the GameOject effect </summary>
    private ParticleSystem ps;

    /// <summary> Check this boolean before launch trigger action (to avoid 2 trigger actions at once) </summary>
    private bool isTriggered;

    #endregion

    #region monobehaviour
    /// <summary>
    /// Start method
    /// </summary>
    void Start()
    {

        // Get particle effect to reduce its simulation time
        GetChildParticleSystem(effect, true);

        effect.SetActive(false);
        animation = GetComponent<Animation>();
        isTriggered = false;
    }

    /// <summary>
    /// When an object trigger the button collider
    /// </summary>
    /// <param name="collide"></param>
    void OnTriggerEnter(Collider collide)
    {
        if (!isTriggered)
        {
            isTriggered = true;
            hapticForce = 100;
            hapticDuration = 700;

            animation.wrapMode = WrapMode.Once;
            animation.Play();

            effect.SetActive(true);
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
        this.senseGloveLeft.SendBuzzCmd(whichFingers, hapticForce, hapticDuration);
        this.senseGloveRight.SendBuzzCmd(whichFingers, hapticForce, hapticDuration);
        yield return new WaitForSeconds(0.7f);

        hapticDuration = 40;

        for (hapticForce = 80; hapticForce >= 0; hapticForce--)
        {
            this.senseGloveLeft.SendBuzzCmd(whichFingers, hapticForce, hapticDuration);
            this.senseGloveRight.SendBuzzCmd(whichFingers, hapticForce, hapticDuration);
            yield return new WaitForSeconds(0.04f);
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
