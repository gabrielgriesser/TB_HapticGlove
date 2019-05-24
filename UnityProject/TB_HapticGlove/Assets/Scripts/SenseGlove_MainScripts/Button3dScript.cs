using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button3dScript : MonoBehaviour
{
    private new Animation animation;
    public SenseGlove_Object gloveRight, gloveLeft;

    /// <summary> The object related to the effect triggered by the button trigger </summary>
    public GameObject effect;

    /// <summary> The magnitude of the haptic Feedback </summary>
    private int hapticForce = 10;

    /// <summary> The duration of a haptic feedback pulse. </summary>
    private int hapticDuration = 1000; //don't show if looping.

    /// <summary> Which fingers to apply the Haptic feedback to </summary>
    private bool[] whichFingers = new bool[5] { true, true, true, true, true };

    /// <summary> Particle system of the GameOject effect </summary>
    private ParticleSystem ps;
    
    // Start is called before the first frame updates
    void Start()
    {
        // Get particle effect to reduce its simulation time
        ps = effect.GetComponent<ParticleSystem>();
        var main = ps.main;
        main.simulationSpeed = 0.5f;

        effect.SetActive(false);
        animation = GetComponent<Animation>();

    }

    /// <summary>
    /// When an object trigger the button collider
    /// </summary>
    /// <param name="collide"></param>
    void OnTriggerEnter(Collider collide)
    {
        animation.wrapMode = WrapMode.Once;
        animation.Play();

        effect.SetActive(true);

        StartCoroutine(SendBuzzGradually());
        
    }

    /// <summary>
    /// Send gradually buzz command to each finger.
    /// To make an "proportion to the explosion" effect, we use a for loop to reduce gradually the magnitude
    /// </summary>
    /// <returns></returns>
    IEnumerator SendBuzzGradually()
    {
        this.gloveLeft.SendBuzzCmd(whichFingers, 100, 400);
        this.gloveRight.SendBuzzCmd(whichFingers, 100, 400);
        yield return new WaitForSeconds(0.4f);

        for (int i = 80; i >= 0; i--)
        {
            this.gloveLeft.SendBuzzCmd(whichFingers, i, 20);
            this.gloveRight.SendBuzzCmd(whichFingers, i, 20);
            yield return new WaitForSeconds(0.02f);
        }

        if(effect.activeSelf == true)
        {
            effect.SetActive(false);
        }
    }
}
