using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// Rain particle collision manager
/// </summary>
public class Senso_RainCollision : MonoBehaviour
{
    #region attribute

    /// <summary> senso Manager </summary>
    public GameObject sensoManager;

    /// <summary> Particle system </summary>
    private ParticleSystem rainParticleSystem;

    /// <summary> The finger triggered </summary>
    private Senso.EFingerType finger;

    /// <summary> The hand to which the finger belongs  </summary>
    private Senso.EPositionType hand;

    /// <summary> The duration of vibration  </summary>
    private ushort duration;
    
    /// <summary> The strenght of vibration  </summary>
    private byte strength;
    #endregion

    #region monobehaviour

    /// <summary>
    /// Start function
    /// </summary>
    private void Start()
    {
        rainParticleSystem = GetComponent<ParticleSystem>();

        strength = 10;
        duration = 250;

        sensoManager.GetComponent<SensoHandsController>().Hands[0].SetTemperature(5);
        sensoManager.GetComponent<SensoHandsController>().Hands[1].SetTemperature(5);
    }

    /// <summary>
    /// On particle collision, if the collided object is a finger (contains "end"), send a vibration (of random strength) on the affected finger.
    /// </summary>
    /// <param name="obj"></param>
    void OnParticleCollision(GameObject obj)
    {

        if (obj.name.Contains("end"))
        {
            if (sensoManager.GetComponent<SensoHandsController>().IsRight(obj))
                hand = Senso.EPositionType.RightHand;
            else
                hand = Senso.EPositionType.LeftHand;

            switch (obj.name.Replace("_particleCollider", ""))
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

            //Debug.Log("Collision between rain and " + hand.ToString() + ":" + finger.ToString());

            strength = System.Convert.ToByte(Random.Range(1, 10));

            sensoManager.GetComponent<SensoHandsController>().SendVibro(hand, finger, duration, strength);
        }
    }
    #endregion
}

