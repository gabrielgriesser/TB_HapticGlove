using UnityEngine;
using System.Collections;


/// <summary>
/// Script used to manage interaction with objects (grab, ungrab, throw)
/// </summary>
public class Falcon_PickupManager : MonoBehaviour
{
    #region attribute
    /// <summary> Is the object grabbable ? </summary>
    public bool isGrabbable;

    /// <summary> The falcon who trigger this object </summary>
    private GameObject falconTriggered;

    /// <summary> Falcon position (GodPosition) </summary>
    private Vector3 falconPosition;

    /// <summary> Falcon number (0 or 1) </summary>
    private int falconNum;

    /// <summary> Is the object picked up ? </summary>
    private bool objectPickedUp;

    /// <summary> Variable used to save object position every 4 frame (i % 4 == 0)  </summary>
    private int i;

    /// <summary> Rigidpody speed, used to throw object </summary>
    private float rigidbodySpeed;

    /// <summary> Object rigidbody </summary>
    Rigidbody rb;

    /// <summary> Object position, updated while grabbed </summary>
    private Vector3 previousGrabPosition;

    #endregion

    #region monobehaviour
    
    /// <summary>
    /// Start function
    /// </summary>
    void Start()
    {
        i = 0;
        objectPickedUp = false;
        rb = this.GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Update every frame.
    /// If this object is picked up, save its position every 4 frame.
    /// Updates its position in center of falcon (GodPosition).
    /// If grabButton re-pressed, throws this object.
    /// </summary>
    void FixedUpdate()
    {
        if (objectPickedUp && falconTriggered != null)
        {
            if (i % 4 == 0)
                previousGrabPosition = transform.position;
            i++;

            FalconUnity.getGodPosition(falconNum, out falconPosition);

            transform.position = falconPosition;

            if (!falconTriggered.GetComponent<SphereManipulator>().GrabButtonPressed())
            {
                ThrowObject();
            }
        }

        rigidbodySpeed = rb.velocity.magnitude;
        if (rigidbodySpeed < 0.2f && !objectPickedUp)
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            GetComponent<FalconRigidBody>().enabled = true;
        }
    }

    /// <summary>
    /// On trigger enter, try to pick up object
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        PickupObject(other.transform);
    }

    /// <summary>
    /// If trigger stays and this object isn't yet grabbed, try to pick it up. 
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerStay(Collider other)
    {
        if (!objectPickedUp)
        {
            PickupObject(other.transform);
        }
    }

    #endregion

    #region methods
    /// <summary>
    /// If the object is catchable, the grab button has been pressed and the falcon is free, grabs the object.
    /// Disable its FalconRigidbody component to avoid a bad physics between that of the server and that of Unity.
    /// Apply mass to the falcon (to feel the weight of the object).
    /// </summary
    private void PickupObject(Transform objectToPickup)
    {
        if (isGrabbable
           && objectToPickup.parent.GetComponent<SphereManipulator>() != null
           && objectToPickup.parent.GetComponent<SphereManipulator>().GrabButtonPressed()
           && !objectToPickup.parent.GetComponent<SphereManipulator>().GetFalconIsOccuped())

        {
            GetComponent<FalconRigidBody>().enabled = false;
            falconTriggered = objectToPickup.parent.gameObject;
            falconNum = falconTriggered.GetComponent<SphereManipulator>().falcon_num;

            falconTriggered.GetComponent<SphereManipulator>().SetFalconIsOccuped(true);
            falconTriggered.GetComponent<SphereManipulator>().ApplyMassToFalcon(GetComponent<FalconRigidBody>().mass / 10f);

            objectPickedUp = true;
        }
    }

    /// <summary>
    /// Object throw process by calculating its throw vector, and throw velocity
    /// Reset attributes
    /// </summary>
    private void ThrowObject()
    {
        if (rb)
        {
            rb.isKinematic = false;

            Vector3 throwVectory = (this.transform.position - previousGrabPosition);


            float speed = (throwVectory.magnitude / Time.deltaTime);

            Vector3 throwVelocity = speed * throwVectory.normalized;


            rb.velocity = throwVelocity;
        }

        objectPickedUp = false;
        falconTriggered.GetComponent<SphereManipulator>().SetFalconIsOccuped(objectPickedUp);
        falconTriggered.GetComponent<SphereManipulator>().ApplyMassToFalcon(0);
        falconTriggered = null;
    }
    #endregion
}
