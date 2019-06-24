using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HI5;

/// <summary>
/// Script used to manage board
/// </summary>
public class Hi5_BoardScript : MonoBehaviour
{
    public Text renderText;

    private PowerLevel batteryLeft, batteryRight;
    private MagneticStatus magnetizationLeft, magnetizationRight;

    private bool leftGloveAvailable, rightGloveAvailable, dongleAvailable;

    private string strRenderText;

    private HI5_GloveStatus m_Status;

    void Awake()
    {
        
        m_Status = HI5_Manager.GetGloveStatus();
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckStatus();

        Debug.Log("--------------------------------------------");
        Debug.Log("Left glove Available :" + leftGloveAvailable);
        Debug.Log("Right glove Available :" + rightGloveAvailable);
        Debug.Log("Dongle Available :" + dongleAvailable);
        if(leftGloveAvailable && rightGloveAvailable && dongleAvailable)
        {
            Debug.Log("Left battery :" + batteryLeft.ToString());
            Debug.Log("Right battery :" + batteryRight.ToString());
            Debug.Log("Left Magnetisation :" + magnetizationLeft.ToString());
            Debug.Log("Right Magnetisation :" + magnetizationRight.ToString());
        }        
        Debug.Log("--------------------------------------------");

    }

    private void CheckStatus()
    {
        leftGloveAvailable = m_Status.IsLeftGloveAvailable;
        rightGloveAvailable = m_Status.IsRightGloveAvailable;
        dongleAvailable = HI5_Manager.IsDongleAvailable();
        if(leftGloveAvailable && rightGloveAvailable && dongleAvailable)
        {
            Debug.Log("ICIIII");
            batteryLeft = m_Status.LeftPower;
            batteryRight = m_Status.RightPower;

            magnetizationLeft = m_Status.LeftMagneticStatus;
            magnetizationRight = m_Status.RightMagneticStatus;
        }

    }
}
