using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to manage the button_1 (Vibration to trigger finger).
/// </summary>
public class Senso_Button1 : MonoBehaviour
{
    #region attribute
    /// <summary> The animation to play on button "click" </summary>
    private new Animation animation;

    /// <summary> The sensoManager related to the project </summary>
    public GameObject SensoManager;

    /// <summary> The finger who trigger button </summary>
    private Senso.EFingerType finger;

    /// <summary> The hand who has the finger </summary>
    private Senso.EPositionType hand;

    /// <summary> if isRunning, do not send vibrations because the Senso are unstable  </summary>
    private bool isVibrating;

    #endregion

    #region monobehaviour
    // Start is called before the first frame updates
    void Start()
    {
        animation = GetComponent<Animation>();
    }

    /// <summary>
    /// When an collider trigger the button collider
    /// </summary>
    /// <param name="collide"></param>
    void OnTriggerEnter(Collider other)
    {
        animation.wrapMode = WrapMode.Once;
        animation.Play("ButtonDown");

        switch (other.gameObject.name)
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
        if (SensoManager.GetComponent<SensoHandsController>().IsRight(other.gameObject))
            hand = Senso.EPositionType.RightHand;
        else
            hand = Senso.EPositionType.LeftHand;

        if (isVibrating == false)
            StartCoroutine(SendBuzzGradually());
    }

    /// <summary>
    /// On trigger exit, play second animation
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit(Collider other)
    {
        animation.wrapMode = WrapMode.Once;
        animation.Play("ButtonUp");
    }
    #endregion

    #region coroutine
    /// <summary>
    /// Send gradually buzz command to each finger from strength 0 to 10
    /// </summary>
    /// <returns></returns>
    IEnumerator SendBuzzGradually()
    {
        isVibrating = true;

        yield return new WaitForSeconds(0.5f);    

        for (float i = 0; i <= 10; i += 0.2f)
        {
            SensoManager.GetComponent<SensoHandsController>().SendVibro(hand, finger, 1000, Convert.ToByte(i));
            //SensoManager.GetComponent<SensoHandsController>().SendVibroToEachFinger(Senso.EPositionType.RightHand, 1000, Convert.ToByte(i));
            //SensoManager.GetComponent<SensoHandsController>().SendVibroToEachFinger(Senso.EPositionType.LeftHand, 1000, Convert.ToByte(i));
            yield return new WaitForSeconds(0.1f);
        }

        isVibrating = false;
    }
    #endregion
}
