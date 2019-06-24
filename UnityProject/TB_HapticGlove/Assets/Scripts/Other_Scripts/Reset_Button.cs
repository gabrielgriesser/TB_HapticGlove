using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script used to manage reset of all interactables objects.
/// </summary>
public class Reset_Button : MonoBehaviour
{
    #region attribute
    /// <summary> Gameobject parent of interactables </summary>
    public Transform objectsContainer;

    /// <summary> Transform of all interactables objects </summary>
    private List<Transform> interactablesTransforms;

    /// <summary> Direct link to container in order to have access to the saved positions/rotations  </summary>
    private SenseGlove_ObjectContainer senseGlove_ObjectContainer;

    /// <summary> Dictionary containing the starting position of each interactable (non breakable) object </summary>
    Dictionary<Transform, Vector3> dicPosition;
    /// <summary> Dictionary containing the starting rotation of each interactable (non breakable) object </summary>
    Dictionary<Transform, Quaternion> dicRotation;
    #endregion

    #region monobehaviour
    /// <summary>
    /// First method called
    /// Get original Position and rotation
    /// Fill in the list of interactables objects
    /// </summary>
    private void Awake()
    {
        senseGlove_ObjectContainer = objectsContainer.GetComponent<SenseGlove_ObjectContainer>();

        interactablesTransforms = new List<Transform>();
        foreach (Transform child in objectsContainer)
        {
            interactablesTransforms.Add(child);
        }

        dicPosition = senseGlove_ObjectContainer.GetDictionaryPosition();
        dicRotation = senseGlove_ObjectContainer.GetDictionaryRotation();

    }

    /// <summary>
    /// Each time this gameObject is enabled (by clicking on the associated button), reset all objects
    /// </summary>
    private void OnEnable()
    {
        ResetObjects();
    }
    #endregion

    #region method
    /// <summary>
    /// Restores the initial positions and rotations of each object.
    /// Disable this gameObject
    /// </summary>
    private void ResetObjects()
    {

        foreach (Transform t in interactablesTransforms)
        {
            t.transform.position = dicPosition[t];
            t.transform.rotation = dicRotation[t];
            if (t.gameObject.GetComponent<Rigidbody>() != null)
            {
                t.gameObject.GetComponent<Rigidbody>().Sleep();
                t.gameObject.GetComponent<Rigidbody>().WakeUp();
            }
        }
        this.gameObject.SetActive(false);
    }
    #endregion
}
