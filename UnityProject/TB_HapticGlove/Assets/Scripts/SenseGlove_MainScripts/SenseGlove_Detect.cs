using System.Collections.Generic;
using UnityEngine;

/// <summary> Turns Sense Glove_Interactables on and off manually. Used to showcase materials. </summary>
public class SenseGlove_Detect : MonoBehaviour
{

    public SenseGlove_Object senseGloveRight, senseGloveLeft;

    private SenseGlove_KeyBinds keyBinds;

    /// <summary> Text mesh to give instructions... </summary>
    //public TextMesh objectText;

    public TextMesh instrText;

    public KeyCode quitKey = KeyCode.Escape;

    /// <summary> The current index in the objectsToSwap list. </summary>
    
    //[Tooltip("The current index in the objectsToSwap list.")]
    //public int index = -1;


    // Use this for initialization
    void Awake()
    {
        
    }

    void Start()
    {
        if (this.senseGloveRight != null && this.senseGloveLeft != null)
        {
            this.keyBinds = this.senseGloveRight.gameObject.GetComponent<SenseGlove_KeyBinds>();
        }
        this.WriteInstr(this.GetInstructions());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(this.quitKey))
        {
            this.WriteInstr("Trying to exit, you CAN'T ! .. Menu here !!!");
        }

        if (this.instrText != null)
        {
            this.WriteInstr(this.GetInstructions());
        }
    }

    /// <summary> Retrieve a a string containing the instructions to operate this demo. </summary>
    /// <returns></returns>
    private string GetInstructions()
    {
        //2 gants connectés
        if ((this.senseGloveRight != null && this.senseGloveRight.GloveReady && this.senseGloveRight.IsConnected) && (this.senseGloveLeft != null && this.senseGloveLeft.GloveReady && this.senseGloveLeft.IsConnected))
        {
            string res = "Use A/S/W/D to move and the mouse wheel to move up / down the hands" + "\r\n";

            if (this.senseGloveRight != null && this.senseGloveRight.IsCalibrating)
            {
                SenseGlove_Data data = this.senseGloveRight.GloveData;

                res = "Calibrating right glove: Gathered " + data.calibrationStep + " / " + data.totalCalibrationSteps + " points.\r\n";
                
                if (this.keyBinds != null && data.calibrationStep != data.totalCalibrationSteps)
                {
                    res += this.keyBinds.cancelCalibrationKey.ToString() + " to exit calibration.";
                }

                else if (this.senseGloveLeft != null && this.senseGloveLeft.IsCalibrating)
                {
                    data = this.senseGloveLeft.GloveData;
                    res = "Calibrating left glove: Gathered " + data.calibrationStep + " / " + data.totalCalibrationSteps + " points.\r\n";

                    if (this.keyBinds != null && data.calibrationStep != data.totalCalibrationSteps)
                    {
                        res += this.keyBinds.cancelCalibrationKey.ToString() + " to exit calibration.";
                    }
                    else
                    {
                        res = "Use A/S/W/D to move and the mouse wheel to move up / down the hands" + "\r\n";
                        res += this.keyBinds.calibrateHandKey + " to start calibration.\r\n";
                        res += this.keyBinds.calibrateWristKey.ToString() + " to calibrate wrist.";
                    }
                }
                return res;
            }

            else if (this.keyBinds != null)
            {
                res += this.keyBinds.calibrateHandKey + " to start calibration.\r\n";
                return res + this.keyBinds.calibrateWristKey.ToString() + " to calibrate wrist.";
            }
            return res + "LeftShift / T(humb) to start calibration.";
        }
        else if((this.senseGloveRight == null || this.senseGloveRight.IsConnected == false) && (this.senseGloveLeft == null || this.senseGloveLeft.IsConnected == false))
        {
            return "No glove connected";
        }
        else if ((this.senseGloveRight == null || this.senseGloveRight.IsConnected == false) || (this.senseGloveLeft == null || this.senseGloveLeft.IsConnected == false))
        {
            return "Waiting to connect to a second\r\nSense Glove";
        }
        return "No glove connected";
    }

    private void WriteInstr(string msg)
    {
        if (this.instrText != null)
        {
            this.instrText.text = msg;
        }
    }



}
