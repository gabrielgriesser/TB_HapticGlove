using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SenseGloveCs.Kinematics;
using SenseGloveCs;

/// <summary> Script used to manage button 1 (calibration) gameObject effect </summary>
public class SenseGlove_Button_Calibration : MonoBehaviour
{
    #region attribute
    /// <summary> Which hands to calibrate </summary>
    public GameObject[] hand;

    /// <summary> Object script from the hands above </summary>
    private SenseGlove_Object senseGloveLeft, senseGloveRight;

    /// <summary> VirtualHand script from the hands above </summary>
    private SenseGlove_VirtualHand virtualHandLeft, virtualHandRight;

    /// <summary> to not launch a second calibration until the first one is finished </summary>
    public bool IsCalibrating { get; set; }

    /// <summary> Which variable to calibrate. </summary>
    public CalibrateVariable variableToCalibrate = CalibrateVariable.FingerVariables;

    /// <summary> How to collect the snapshots required to calibrate the chosen variable. </summary>
    public CollectionMethod collectionMethod = CollectionMethod.SemiAutomatic;

    //public CalibrationType calibrationType = CalibrationType.SemiAutomatic;
    #endregion

    #region monobehaviour
    /// <summary>
    /// Awake method. Get components of each hand
    /// </summary>
    void Awake()
    {
       
        virtualHandLeft = hand[1].GetComponent<SenseGlove_VirtualHand>();
        senseGloveLeft = hand[1].GetComponent<SenseGlove_Object>();

        virtualHandRight = hand[0].GetComponent<SenseGlove_VirtualHand>();
        senseGloveRight = hand[0].GetComponent<SenseGlove_Object>();
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

        if (senseGloveRight.IsConnected && senseGloveLeft.IsConnected)
        {
            IsCalibrating = true;
            //Debug.Log("Calibrate right and left wrist");
            //yield return new WaitForSeconds(2);
            //virtualHandRight.CalibrateWrist();
            //virtualHandLeft.CalibrateWrist();

            Debug.Log("Calibrate right and left finger");
            yield return new WaitForSeconds(2);
            senseGloveRight.StartCalibration(variableToCalibrate, collectionMethod);
            senseGloveLeft.StartCalibration(variableToCalibrate, collectionMethod);
        }
        else
        {
            if (senseGloveLeft.IsConnected)
            {
                IsCalibrating = true;
                //Debug.Log("Calibrate left wrist");
                //yield return new WaitForSeconds(2);
                //virtualHandLeft.CalibrateWrist();

                Debug.Log("Calibrate left finger");
                yield return new WaitForSeconds(2);
                senseGloveLeft.StartCalibration(variableToCalibrate, collectionMethod);
            }
            else if(senseGloveRight.IsConnected)
            {
                IsCalibrating = true;
                //Debug.Log("Calibrate Right wrist");
                //yield return new WaitForSeconds(2);
                //virtualHandRight.CalibrateWrist();

                Debug.Log("Calibrate Right finger");
                yield return new WaitForSeconds(2);
                senseGloveRight.StartCalibration(variableToCalibrate, collectionMethod);
            }

        }
        yield return new WaitForSeconds(1);
        IsCalibrating = false;
        this.gameObject.SetActive(false);
    }
    #endregion
}