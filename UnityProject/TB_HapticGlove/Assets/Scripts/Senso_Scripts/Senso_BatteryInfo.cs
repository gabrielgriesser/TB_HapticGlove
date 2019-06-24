using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage connection with hand and give the information on the board
/// </summary>
public class Senso_BatteryInfo : MonoBehaviour
{
    #region attribute
    /// <summary> Hand connected to this object </summary>
    public Senso.Hand hand;

    /// <summary> Sprite renderer used to display the battery information icon </summary>
    public SpriteRenderer spritRenderer;

    /// <summary> Sprite used to show when a battery is full, half or empty </summary>
    public Sprite spritFull, spritHalf, spritEmpty;

    /// <summary> glove status used to change sprite </summary>
    private int gloveStatus = -1;
    #endregion 

    #region monobehaviour
    /// <summary>
    /// Start method
    /// </summary>
    private void Start()
    {
        SetConnectedSpriteRenderer(gloveStatus);
    }

    /// <summary>
    /// Called once per frame, check glove status (connected and ready ?)
    /// </summary>
    private void Update()
    {
        if (hand != null)
        {
            if (hand.BatteryLevel > 66)
            {
                gloveStatus = 1;
            }
            else if (hand.BatteryLevel > 33 && hand.BatteryLevel < 66)
            {
                gloveStatus = 2;
            }
            else if (hand.BatteryLevel < 33 && hand.BatteryLevel > 0)
            {
                gloveStatus = 3;
            }
            else if (hand.BatteryLevel < 0)
            {
                gloveStatus = -1;
            }
        }

        SetConnectedSpriteRenderer(gloveStatus);
    }
    #endregion

    #region method
    /// <summary>
    /// Changes the connection icon according to the gloveStatus
    /// </summary>
    /// <param name="gloveStatus"></param>
    private void SetConnectedSpriteRenderer(int gloveStatus)
    {
        switch (gloveStatus)
        {
            case 1:
                spritRenderer.sprite = spritFull;
                break;
            case 2:
                spritRenderer.sprite = spritHalf;
                break;
            case 3:
                spritRenderer.sprite = spritEmpty;
                break;
            case -1:
                spritRenderer.sprite = null;
                break;
        }
    }
    #endregion
}
