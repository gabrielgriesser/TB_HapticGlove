using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Collide manager for each finger
/// </summary>
public class Senso_HapticFeedback : MonoBehaviour
{
    #region attribute

    /// <summary> The sensoManager related to the project </summary>
    public GameObject SensoManager;
    /// <summary> The finger triggered </summary>
    private Senso.EFingerType finger;
    /// <summary> The hand to which the finger belongs </summary> 
    private Senso.EPositionType hand;

    /// <summary> The duration of vibration </summary> 
    private ushort duration;
    /// <summary> The strength of vibration </summary> 
    private byte strength;
    #endregion

    #region monobehaviour
    void Start()
    {
        duration = 500;
        strength = 255;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Senso_Material>() && other.GetComponent<Senso_Material>().hapticFeedback)
            SendHapticFeedback(other);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Senso_Material>() && other.GetComponent<Senso_Material>().hapticFeedback)
            SendHapticFeedback(other);
    }
    #endregion

    #region haptic feedback

    /// <summary>
    /// When a collision between object and finger is detected, send a vibration to the concerned fingers according to the properties of the touched object.
    /// </summary>
    /// <param name="other"></param>
    public void SendHapticFeedback(Collider other)
    {
        if (other.name.Contains("end"))
        {
            Physics.IgnoreCollision(this.GetComponent<Collider>(), other);
        }

        else
        {
            if (SensoManager.GetComponent<SensoHandsController>().IsRight(this.gameObject))
                hand = Senso.EPositionType.RightHand;
            else
                hand = Senso.EPositionType.LeftHand;

            switch (this.gameObject.name)
            {
                case "Bone.004_thumb_end":
                    finger = Senso.EFingerType.Thumb;
                    break;
                case "Bone.008_index_end":
                    finger = Senso.EFingerType.Index;
                    break;
                case "Bone.017_middle_end":
                    finger = Senso.EFingerType.Middle;
                    break;
                case "Bone.015_third_end":
                    finger = Senso.EFingerType.Third;
                    break;
                case "Bone.020_little_end":
                    finger = Senso.EFingerType.Little;
                    break;
            }

            // Get touched object properties
            if (other.GetComponent<Senso_Material>() != null)
            {
                duration = other.GetComponent<Senso_Material>().GetDuration();
                strength = other.GetComponent<Senso_Material>().GetStrength();
            }

            SensoManager.GetComponent<SensoHandsController>().ReceiveCollision(hand, finger, duration, strength);
        }
    }
    #endregion
}
