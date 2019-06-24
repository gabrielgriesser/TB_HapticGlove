using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage connection with hand and give the information on the board
/// </summary>
public class SenseGlove_ConnectionInfo : MonoBehaviour
{
    #region attribute
    /// <summary> Hand connected to this object </summary>
    public SenseGlove_Object hand;

    /// <summary> Sprite renderer used to display the connection information icon </summary>
    public SpriteRenderer spritRenderer;

    /// <summary> Sprite used to show when a glove is connected and when a glove is unlinked </summary>
    public Sprite spritConnected, spritUnlinked;

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
        if (this.hand != null && this.hand.GloveReady && this.hand.IsConnected)
            gloveStatus = 1;
        
        else if (this.hand != null && (this.hand.GloveReady == false && this.hand.IsConnected))
            gloveStatus = 0;

        else
            gloveStatus = -1;

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
                spritRenderer.sprite = spritConnected;
                break;
            case 0:
                spritRenderer.sprite = spritUnlinked;
                break;
            case -1:
                spritRenderer.sprite = null;
                break;
        }
    }
    #endregion
}
