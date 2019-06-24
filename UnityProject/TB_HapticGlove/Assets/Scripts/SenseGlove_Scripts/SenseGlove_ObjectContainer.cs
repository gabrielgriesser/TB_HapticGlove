using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Script that contains property (position and rotation) of all interactables (non breakable) objects.
/// </summary>
public class SenseGlove_ObjectContainer : MonoBehaviour
{
    #region attribute

    /// <summary> Dictionary containing the starting position of each interactable (non breakable) object </summary>
    Dictionary<Transform, Vector3> dicPosition;

    /// <summary> Dictionary containing the starting rotation of each interactable (non breakable) object </summary>
    Dictionary<Transform, Quaternion> dicRotation;

    #endregion

    #region monobehaviour
    /// <summary>
    /// Start method, instanciate dictionary with start position and rotation of each object
    /// </summary>
    private void Start()
    {
        dicPosition = new Dictionary<Transform, Vector3>();
        dicRotation = new Dictionary<Transform, Quaternion>();


        foreach (Transform child in this.transform)
        {

            dicPosition.Add(child.transform, child.transform.position);
            dicRotation.Add(child.transform, child.transform.rotation);

        }
    }
    #endregion

    #region getter
    /// <summary>
    /// Getter for positions
    /// </summary>
    /// <returns></returns>
    public Dictionary<Transform, Vector3> GetDictionaryPosition()
    {
        return dicPosition;
    }

    /// <summary>
    /// Getter for rotations
    /// </summary>
    /// <returns></returns>
    public Dictionary<Transform, Quaternion> GetDictionaryRotation()
    {
        return dicRotation;
    }
    #endregion
}
