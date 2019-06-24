using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage connection with falcons and give the information on the board
/// </summary>
public class Falcon_ConnectionInfo : MonoBehaviour
{
    #region attribute
    /// <summary> Falcon connected to this object </summary>
    //public FalconMain firstFalcon, secondFalcon;

    /// <summary> Sprite renderer used to display the connection information (battery icon) </summary>
    public SpriteRenderer firstFalconSpritRenderer, secondFalconSpritRenderer;

    /// <summary> Sprite used to show when falcon is connected (icon battery full) or disconnected (icon battery empty) </summary>
    public Sprite spritConnected, spritDisconnected;

    /// <summary> falcon status used to change sprite </summary>
    private int firstFalconStatus, secondFalconStatus;
    #endregion 

    #region monobehaviour
    /// <summary>
    /// Start method
    /// </summary>
    private void Start()
    {
        firstFalconStatus = secondFalconStatus = -1;
        SetConnectedSpriteRenderer();
    }

    /// <summary>
    /// Called once per frame, check falcon status (connected ?)
    /// </summary>
    private void Update()
    {
        if (FalconUnity.getNumFalcons() == 1)
        {
            firstFalconStatus = 1;
        }
        else if (FalconUnity.getNumFalcons() == 2)
        {
            firstFalconStatus = 1;
            secondFalconStatus = 1;
        }

        SetConnectedSpriteRenderer();
    }
    #endregion

    #region method
    /// <summary>
    /// Changes the connection icon according to the first and second falcon status
    /// </summary>
    private void SetConnectedSpriteRenderer()
    {
        switch (firstFalconStatus)
        {
            case 1:
                firstFalconSpritRenderer.sprite = spritConnected;
                break;
            case -1:
                firstFalconSpritRenderer.sprite = spritDisconnected;
                break;
        }
        switch (secondFalconStatus)
        {
            case 1:
                secondFalconSpritRenderer.sprite = spritConnected;
                break;
            case -1:
                secondFalconSpritRenderer.sprite = spritDisconnected;
                break;
        }
    }

    #endregion
}
