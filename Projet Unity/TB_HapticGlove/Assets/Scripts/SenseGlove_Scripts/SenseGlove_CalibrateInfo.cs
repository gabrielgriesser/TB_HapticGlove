using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage calibration with hand and give the information on the board
/// </summary>
public class SenseGlove_CalibrateInfo : MonoBehaviour
{
    #region attribute

    /// <summary> Hand connected to this object </summary>
    public SenseGlove_Object hand;

    /// <summary> Sprite renderer used to display the calibration information icon </summary>
    public SpriteRenderer spritRenderer;

    /// <summary> Sprite used to show when a glove is calibrating and when calibration is finished </summary>
    public Sprite spritIsCalibrating, spritCalibrate_done;

    /// <summary> When a calibration start, we need to know when it ends with the datas </summary>
    private SenseGlove_Data data;

    /// <summary> glove status used to change sprite </summary>
    private int gloveStatus = 0;

    #endregion

    #region monobehaviour
    /// <summary>
    /// Start method
    /// </summary>
    private void Start()
    {
        SetCalibrationSpriteRenderer(gloveStatus);
    }

    /// <summary>
    /// Called once per frame, check glove status (calibrating ? calibration finished ?)
    /// </summary>
    private void Update()
    {
        if (this.hand != null && this.hand.GloveReady && this.hand.IsConnected)
        {
            if (this.hand.IsCalibrating)
            {
                data = hand.GloveData;
                if (data.calibrationStep != data.totalCalibrationSteps)
                {
                    gloveStatus = 1;
                }
                else
                {
                    gloveStatus = 0;
                }
            }

        }
        else
            gloveStatus = -1;
        SetCalibrationSpriteRenderer(gloveStatus);
    }
    #endregion

    #region method
    /// <summary>
    /// Changes the calibration icon according to the gloveStatus
    /// </summary>
    /// <param name="gloveStatus"></param>
    private void SetCalibrationSpriteRenderer(int gloveStatus)
    {
        switch (gloveStatus)
        {
            case 1:
                spritRenderer.sprite = spritIsCalibrating;
                break;
            case 0:
                spritRenderer.sprite = spritCalibrate_done;
                break;
            case -1:
                spritRenderer.sprite = null;
                break;
        }
    }
    #endregion
}
