using System.Collections.Generic;
using UnityEngine;

/// <summary> Turns Sense Glove_Interactables on and off manually. Used to showcase materials. </summary>
public class SenseGlove_Detect : MonoBehaviour
{

    public SenseGlove_Object senseGlove;

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
        if (this.senseGlove != null)
        {
            this.keyBinds = this.senseGlove.gameObject.GetComponent<SenseGlove_KeyBinds>();
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
        if (this.senseGlove != null && this.senseGlove.GloveReady)
        {
            string res = "Use A/S/W/D to move and the mouse wheel to up/down hands" + "\r\n";

            if (this.senseGlove != null && this.senseGlove.IsCalibrating)
            {
                SenseGlove_Data data = this.senseGlove.GloveData;
                res += "Calibrating: Gathered " + data.calibrationStep + " / " + data.totalCalibrationSteps + " points.\r\n";
                if (this.keyBinds != null)
                {
                    res += this.keyBinds.cancelCalibrationKey.ToString() + " to cancel.";
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
        return "Waiting to connect to a\r\nSense Glove";
    }

    private void WriteInstr(string msg)
    {
        if (this.instrText != null)
        {
            this.instrText.text = msg;
        }
    }



}
