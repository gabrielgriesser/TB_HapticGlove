using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// Manage particle system collision with hands
/// </summary>
public class SenseGlove_RainCollision : MonoBehaviour
{

    #region attribute

    /// <summary> The particle system (here, rain) </summary>
    public ParticleSystem RainParticleSystem;

    /// <summary> Direct link to SenseGlove object. Allow access to the vibration method </summary>
    public SenseGlove_Object senseGloveRight, senseGloveLeft;

    /// <summary> Name of the collided finger </summary>
    private string fingerName;

    /// <summary> Finger to vibrate when collided </summary>
    private bool[] fingerToBuzz;

    /// <summary> Magnitude and duration of vibration </summary>
    private int magnitude, duration;

    #endregion

    #region monobehaviour

    /// <summary>
    /// Start method
    /// </summary>
    void Start()
    {
        fingerToBuzz = new bool[] { false, false, false, false, false };
        magnitude = 100;
        duration = 50;

    }

    /// <summary>
    /// On particle collision, check if the affected object belongs to the right or left hand.
    /// To detect collision, we added a collider (with a kinematic rigidbody) to each finger.
    /// </summary>
    /// <param name="obj"></param>
    void OnParticleCollision(GameObject obj)
    {
        //if (obj.transform.IsChildOf(GameObject.Find("SenseGloveHand - Right Grab").transform)
        //|| obj.transform.IsChildOf(GameObject.Find("SenseGloveHand - Left Grab").transform))
        if (obj.transform.IsChildOf(senseGloveLeft.transform)
        || obj.transform.IsChildOf(senseGloveRight.transform))
        {
            // Get collider name (ex. ThumbParticleCollider)
            fingerName = obj.name.Replace("ParticleCollider", "");
            
            switch (fingerName)
            {
                case "palm":
                    SetAllFinger(true);
                    break;
                case "thumb":
                    fingerToBuzz[0] = true;
                    break;
                case "index":
                    fingerToBuzz[1] = true;
                    break;
                case "middle":
                    fingerToBuzz[2] = true;
                    break;
                case "ring":
                    fingerToBuzz[3] = true;
                    break;
                case "pinky":
                    fingerToBuzz[4] = true;
                    break;
            }

            if (senseGloveRight && obj.transform.IsChildOf(senseGloveRight.transform))
            {
                SendBuzzCmd(senseGloveRight);
            }
            else if (senseGloveLeft && obj.transform.IsChildOf(senseGloveLeft.transform))
            {
                SendBuzzCmd(senseGloveLeft);
            }
        }
    }
    #endregion

    #region method
    /// <summary>
    /// Sends a vibration of random magnitude  
    /// </summary>
    /// <param name="senseGlove">senseGlove to access vibration method</param>
    void SendBuzzCmd(SenseGlove_Object senseGlove)
    {
        magnitude = Random.Range(50, 100);
        senseGlove.SendBuzzCmd(fingerToBuzz, magnitude, duration);
        SetAllFinger(false);
    }

    /// <summary>
    /// Set all finger to value passed on parameter
    /// </summary>
    /// <param name="value"></param>
    void SetAllFinger(bool value)
    {
        for (int i = 0; i < fingerToBuzz.Length; i++)
            fingerToBuzz[i] = value;
    }

    #endregion
}
