using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/// <summary> Manage SenseGlove actions </summary>
public class SenseGlove_Detect : MonoBehaviour
{
    #region attribute

    /// <summary> The two senseGloves </summary>
    public SenseGlove_Object senseGloveRight, senseGloveLeft;

    /// <summary> KeyBinds used to manage senseGlove with keyboard (Calibration, wrist, model, etc) </summary>
    private SenseGlove_KeyBinds keyBinds;

    /// <summary> Text mesh to give instructions (placed on the board) </summary>    
    public TextMesh instrText;

    /// <summary> Text about senseGlove and instructions for their use. </summary>
    private string senseGloveInfoText, userInfoText;

    /// <summary> When a glove is calibrating, this variable will contain the glove datas </summary>
    private SenseGlove_Data data;

    /// <summary> Tracker left and right </summary>
    private int iTrackerLeft, iTrackerRight;

    #endregion

    #region monobehaviour
    /// <summary>
    /// Initialization
    /// </summary>
    void Awake()
    {
        iTrackerLeft = 0;
        iTrackerRight = 0;
    }

    /// <summary>
    /// Start method. Get keybinds and write informations about SenseGloves
    /// </summary>
    void Start()
    {
        if (this.senseGloveRight != null && this.senseGloveLeft != null)
        {
            this.keyBinds = this.senseGloveRight.gameObject.GetComponent<SenseGlove_KeyBinds>();
        }
        senseGloveInfoText =
        @"SenseGlove

The SenseGlove are developed by 
Sense Glove firm. 
They provide a complete hand and finger 
recognition with a force feedback system 
(brake) and a tactile feedback system 
(vibration) on each finger." + "\n\n" +
@"To throw an object, open your hand 
completely. No fingers should be 
in contact with the object 
at the end of the movement" + "\n\n";

        this.WriteInstr(this.GetInstructions());
    }

    /// <summary>
    /// Once per frame, get instructions to write them on the board
    /// </summary>
    void Update()
    {
        if (this.instrText != null)
        {
            this.WriteInstr(this.GetInstructions());
        }
    }

    #endregion 

    #region methods

    /// <summary> Write string on the board containing the instructions to operate this demo.</summary>
    /// <returns></returns>
    private string GetInstructions()
    {
        //2 gloves connected
        if ((this.senseGloveRight != null && this.senseGloveRight.GloveReady && this.senseGloveRight.IsConnected)
        && (this.senseGloveLeft != null && this.senseGloveLeft.GloveReady && this.senseGloveLeft.IsConnected))
        {
            //1 glove calibrating (right first)
            if ((this.senseGloveRight != null && this.senseGloveRight.IsCalibrating)
            && (this.senseGloveLeft != null && this.senseGloveLeft.IsCalibrating))
            {
                // Right hand calibrating
                if (this.senseGloveRight != null || this.senseGloveRight.IsCalibrating)
                {
                    data = this.senseGloveRight.GloveData;

                    userInfoText = @"Calibrating right glove: 
Gathered " + data.calibrationStep + " / " + data.totalCalibrationSteps + " points.\r\n";

                    if (data.calibrationStep != data.totalCalibrationSteps)
                    {
                        userInfoText += "Waiting for a snapshot . . .";
                    }
                    // When right hand calibration finished, do left hand
                    else
                    {
                        // Left hand calibrating
                        if (this.senseGloveLeft != null && this.senseGloveLeft.IsCalibrating)
                        {

                            data = this.senseGloveLeft.GloveData;
                            userInfoText += @"Calibrating left glove: 
Gathered " + data.calibrationStep + " / " + data.totalCalibrationSteps + " points.\r\n";

                            if (data.calibrationStep != data.totalCalibrationSteps)
                            {
                                userInfoText += "Waiting for a snapshot . . .";
                            }
                            else
                            {

                                userInfoText = @"Interact with objects and buttons. 
Hand and wrist calibration 
are launched via a button 
on the table." + "\n";
                            }
                        }
                    }
                    return userInfoText;
                }
                return userInfoText;

            }
        }

        //At least 1 glove connected
        if ((this.senseGloveRight != null && this.senseGloveRight.GloveReady && this.senseGloveRight.IsConnected) || (this.senseGloveLeft != null && this.senseGloveLeft.GloveReady && this.senseGloveLeft.IsConnected))
        {
            // if one hand is calibrating
            if ((this.senseGloveRight != null && this.senseGloveRight.IsCalibrating) || (this.senseGloveLeft != null && this.senseGloveLeft.IsCalibrating))
            {
                // Right hand calibrating
                if (this.senseGloveRight != null && this.senseGloveRight.IsCalibrating)
                {
                    data = this.senseGloveRight.GloveData;

                    userInfoText = @"Calibrating right glove: 
Gathered " + data.calibrationStep + " / " + data.totalCalibrationSteps + " points.\r\n";

                    //if (this.keyBinds != null && data.calibrationStep != data.totalCalibrationSteps)
                    if (data.calibrationStep != data.totalCalibrationSteps)
                    {
                        userInfoText += "Repress the button to exit calibration.";
                    }
                    else
                    {
                        userInfoText = @"Interact with objects and buttons. 
Hand and wrist calibration 
are launched via a button on the table." + "\n";
                    }
                }

                // Left hand calibrating
                if (this.senseGloveLeft != null && this.senseGloveLeft.IsCalibrating)
                {

                    data = this.senseGloveLeft.GloveData;
                    userInfoText = @"Calibrating left glove: 
Gathered " + data.calibrationStep + " / " + data.totalCalibrationSteps + " points.\r\n";

                    //if (this.keyBinds != null && data.calibrationStep != data.totalCalibrationSteps)
                    if (data.calibrationStep != data.totalCalibrationSteps)
                    {
                        userInfoText += "Repress the button to exit calibration.";
                    }
                    else
                    {

                        userInfoText = @"Interact with objects and buttons. 
Hand and wrist calibration 
are launched via a button on the table." + "\n";
                    }
                }
                return userInfoText;

            }
            else if (this.keyBinds != null)
            {
                userInfoText = @"Interact with objects and buttons. 
Hand and wrist calibration 
is launched via a button on the table." + "\n";

                return userInfoText;
            }
            return userInfoText;
        }

        //No glove connected
        else if ((this.senseGloveRight == null || this.senseGloveRight.IsConnected == false) && (this.senseGloveLeft == null || this.senseGloveLeft.IsConnected == false))
        {
            return "No glove connected";
        }

        //One glove connected and ready, but not calibrating.
        else if ((this.senseGloveRight == null || this.senseGloveRight.IsConnected == false) || (this.senseGloveLeft == null || this.senseGloveLeft.IsConnected == false))
        {
            userInfoText = @"Waiting to connect to a second 
Sense Glove. 
Interact with objects and buttons." + "\n" +
            @"Hand and wrist calibration 
are launched via a button 
on the table." + "\n";
            return userInfoText;
        }

        return "No glove connected";
    }

    /// <summary>
    /// Send text to the board
    /// </summary>
    /// <param name="msg"></param>
    private void WriteInstr(string msg)
    {
        if (this.instrText != null)
        {
            this.instrText.text = senseGloveInfoText + msg;
        }
    }

    #endregion
}
