using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SenseGloveCs.Kinematics;

/// <summary> Script used to manage button 2 (calibration) gameObject effect </summary>
public class SenseGlove_Button2 : MonoBehaviour
{
    #region attribute
    /// <summary> Which hands to calibrate </summary>
    public GameObject[] hand;

    /// <summary> Object script from the hands above </summary>
    private SenseGlove_Object objectLeftHand, objectRightHand;

    /// <summary> VirtualHand script from the hands above </summary>
    private SenseGlove_VirtualHand virtualLeftHand, virtualRightHand;

    /// <summary> to not launch a second calibration until the first one is finished </summary>
    public bool isCalibrating { get; set; }

    /// <summary> Which variable to calibrate. </summary>
    public CalibrateVariable variableToCalibrate = CalibrateVariable.FingerVariables;

    /// <summary> How to collect the snapshots required to calibrate the chosen variable. </summary>
    public CollectionMethod collectionMethod = CollectionMethod.SemiAutomatic;
    #endregion

    #region monobehaviour
    /// <summary>
    /// Awake method. Get components of each hand
    /// </summary>
    void Awake()
    {
        virtualLeftHand = hand[1].GetComponent<SenseGlove_VirtualHand>();
        objectLeftHand = hand[1].GetComponent<SenseGlove_Object>();

        virtualRightHand = hand[0].GetComponent<SenseGlove_VirtualHand>();
        objectRightHand = hand[0].GetComponent<SenseGlove_Object>();
    }

    /// <summary>
    /// When enabled (on via SenseGlove_Trigger script), launch calibration
    /// </summary>
    private void OnEnable()
    {
        StartCoroutine(StartCalibration());
    }
    #endregion

    #region coroutine
    /// <summary>
    /// Using a coroutine to start the calibration of the wrist and then (after 2sec) the fingers.
    /// </summary>
    /// <returns></returns>
    IEnumerator StartCalibration()
    {

        if (objectRightHand.IsConnected && objectLeftHand.IsConnected)
        {
            isCalibrating = true;
            Debug.Log("Calibrate right and left wrist");
            yield return new WaitForSeconds(2);
            virtualRightHand.CalibrateWrist();
            virtualLeftHand.CalibrateWrist();

            Debug.Log("Calibrate right and left finger");
            yield return new WaitForSeconds(2);
            objectRightHand.StartCalibration(variableToCalibrate, collectionMethod);
            objectLeftHand.StartCalibration(variableToCalibrate, collectionMethod);
        }
        else
        {
            if (objectLeftHand.IsConnected)
            {
                isCalibrating = true;
                Debug.Log("Calibrate left wrist");
                yield return new WaitForSeconds(2);
                virtualLeftHand.CalibrateWrist();

                Debug.Log("Calibrate left finger");
                yield return new WaitForSeconds(2);
                objectLeftHand.StartCalibration(variableToCalibrate, collectionMethod);
            }
            else
            {
                isCalibrating = true;
                Debug.Log("Calibrate Right wrist");
                yield return new WaitForSeconds(2);
                virtualRightHand.CalibrateWrist();

                Debug.Log("Calibrate Right finger");
                yield return new WaitForSeconds(2);
                objectRightHand.StartCalibration(variableToCalibrate, collectionMethod);
            }

        }
        yield return new WaitForSeconds(1);
        isCalibrating = false;
        this.gameObject.SetActive(false);
    }
    #endregion
}