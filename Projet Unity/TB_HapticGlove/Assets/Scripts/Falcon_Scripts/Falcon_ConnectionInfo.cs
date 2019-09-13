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

    /// <summary> Textmesh used to show information about falcons </summary>
    public TextMesh infoText;

    /// <summary> falcon status used to change sprite </summary>
    private int firstFalconStatus, secondFalconStatus;

    /// <summary> String that will be displayed on the board (on TextMesh) </summary>
    private string strInfoText, strInfoFalcon;
    #endregion 

    #region monobehaviour
    /// <summary>
    /// Start method
    /// </summary>
    private void Start()
    {
        strInfoText = @"The Falcon, from Novint, is a haptic device 
connected to USB. 

It allows you to feel the force feedback, 
and therefore the texture, the resistance 
of the objects and their weight.

The connection is made through a server : 
FalconServer. It starts automatically
when the scene is loaded.
If the falcons are badly instantiated, 
please reload the application
because sometimes the server starts badly.

To take an object in hand, click on the button 
in the middle of the falcon. To release 
an object, click on the same button.
";

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
            strInfoFalcon = "\n\n1 falcon connected";

        }
        else if (FalconUnity.getNumFalcons() == 2)
        {
            firstFalconStatus = 1;
            secondFalconStatus = 1;
            strInfoFalcon = "\n\n2 falcons connected";
        }
        infoText.text = strInfoText + strInfoFalcon;

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
