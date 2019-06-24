using System;
using UnityEngine;

/// <summary>
/// Class representing the material of an object. The material is defined by a strength and a vibration duration
/// </summary>
public class Senso_Material : MonoBehaviour
{

    #region attribute

    [Header("Haptic feedback")]
    /// <summary> If checked, send a vibration when the object is touched </summary>
    public bool hapticFeedback;

    /// <summary> Strength of vibration </summary>
    [Range(0, 10)]
    public int strength;

    /// <summary> Duration of vibration </summary>
    [Range(0, 1000)]
    public ushort duration;

    [Header("Grabbable")]
    /// <summary> The object is grabbable ? </summary>
    public bool isGrabbable;

    /// <summary> Distance between thumb and index finger at which the object is considered to be picked up </summary>
    [Range(0, 0.2f)]
    public float distPinch;

    /// <summary> Transform to which the object will be attached if it is picked up  </summary>
    private Transform grabAnchor;

    /// <summary> Is the index/thumb colliding ? </summary>
    private bool indexColliding, thumbColliding;

    /// <summary> Respective index and thumb transform </summary>
    private Transform index, thumb;

    /// <summary> Is the object picked up ? </summary>
    private bool isObjectPickedUp;

    /// <summary> The grabbedObject </summary>
    private GameObject grabbedObject;

    /// <summary> Position of grabbedObject, updated while grabbed </summary>
    private Vector3 previousGrabPosition;

    /// <summary> Direct link to SensoHandExample. Used to check if an object is already attached to the hand </summary>
    private SensoHandExample sensoHand;

    #endregion

    #region getter
    public byte GetStrength()
    {
        return Convert.ToByte(strength);
    }

    public ushort GetDuration()
    {
        return duration;
    }
    #endregion

    #region monobehaviour

    /// <summary>
    /// Start method
    /// </summary>
    void Start()
    {
        isObjectPickedUp = false;
        indexColliding = false;
        thumbColliding = false;
    }

    /// <summary>
    /// Updated every frame
    /// As long as the object is caught, we update its position
    /// If the distance between the thumb and index finger exceeds distPinch, the object is released
    /// </summary>
    void Update()
    {
        if (isObjectPickedUp && grabbedObject != null)
        {
            previousGrabPosition = this.transform.position;

            SetObjectPose();

            if (Vector3.Distance(thumb.transform.position, index.transform.position) > distPinch)
            {
                ThrowObject();
            }
        }
    }

    /// <summary>
    /// On trigger enter, we check which finger triggered
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (isGrabbable && !isObjectPickedUp && !FindParentWithTag(other.gameObject, "SensoGlove").GetComponent<SensoHandExample>().GetHandIsOccuped())
        {
            sensoHand = FindParentWithTag(other.gameObject, "SensoGlove").GetComponent<SensoHandExample>();
            grabAnchor = sensoHand.grabAnchor;

            //Thumb colliding
            if (ObjectNameContains(other.gameObject, "thumb"))
            {
                thumbColliding = true;
                thumb = other.transform;
            }
            //Index colliding
            if (ObjectNameContains(other.gameObject, "index"))
            {
                indexColliding = true;
                index = other.transform;
            }
        }

        PickupObject();
    }

    #endregion

    #region method

    /// <summary>
    /// If the thumb and index finger are in contact with the object 
    /// AND the distance between the two fingers is smaller than distPinch, the object is picked up
    /// Set hand is occuped to true, prevents 2 objects from being picked up at the same time
    /// </summary>
    private void PickupObject()
    {
        if (thumbColliding && indexColliding && Vector3.Distance(thumb.transform.position, index.transform.position) <= distPinch)
        {
            grabbedObject = this.gameObject;
            isObjectPickedUp = true;

            sensoHand.SetHandIsOccuped(isObjectPickedUp);
        }
    }

    /// <summary>
    /// Object throw process by calculating its throw vector, and throw velocity
    /// Reset all attributes
    /// </summary>
    private void ThrowObject()
    {

        Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();

        if (rb)
        {
            Vector3 throwVectory = (grabbedObject.transform.position - previousGrabPosition);

            float speed = (throwVectory.magnitude / Time.deltaTime);

            Vector3 throwVelocity = speed * throwVectory.normalized;

            rb.velocity = throwVelocity;
        }

        isObjectPickedUp = false;
        sensoHand.SetHandIsOccuped(isObjectPickedUp);

        thumbColliding = false;
        indexColliding = false;

        thumb = null;
        index = null;

        grabbedObject = null;
    }

    /// <summary>
    /// Set object position to center of thumb and index
    /// </summary>
    public void SetObjectPose()
    {

        if (this.GetComponent<Rigidbody>())
        {
            this.GetComponent<Rigidbody>().isKinematic = false;
        }

        if (grabbedObject.name.Contains("Soccer"))
        {
            this.transform.position = Vector3.Lerp(index.transform.position, thumb.transform.position, 0.5f) + new Vector3(0, 0, 0.22f);
            this.transform.rotation = grabAnchor.transform.rotation;
        }
        else
        {
            this.transform.position = Vector3.Lerp(index.transform.position, thumb.transform.position, 0.5f);
            this.transform.rotation = grabAnchor.transform.rotation;
        }


    }

    /// <summary>
    /// Returns if the name of a gameObject contains the string str
    /// </summary>
    /// <param name="go"></param>
    /// <param name="str"></param>
    /// <returns></returns>
    private bool ObjectNameContains(GameObject go, string str)
    {
        return go.name.Contains(str);
    }

    /// <summary>
    /// Get the first GameObject parent with tag
    /// </summary>
    /// <param name="childObject">GameObject</param>
    /// <param name="tag">tag</param>
    /// <returns></returns>
    public GameObject FindParentWithTag(GameObject childObject, string tag)
    {
        Transform t = childObject.transform;
        while (t.parent != null)
        {
            if (t.parent.tag == tag)
            {
                return t.parent.gameObject;
            }
            t = t.parent.transform;
        }
        return null; // Could not find a parent with given tag.
    }
    #endregion

}
