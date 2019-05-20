using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterObject : MonoBehaviour
{
    /// <summary> One of the GameObjects to swap. </summary>
    [Tooltip("One of the GameObjects to swap. Both objects must be assigned for this script to work.")]
    public GameObject objectA, objectB;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(objectA.GetComponent<Collider>().isTrigger)
        {
            SetParent(objectA.transform, objectB.transform);
        }
    }

    /// <summary> Set the new parent of another transform, keepign the same local position- and rotation. </summary>
    /// <param name="obj"></param>
    /// <param name="newParent"></param>
    protected void SetParent(Transform obj, Transform newParent)
    {
        if (obj != null && !GameObject.ReferenceEquals(obj.parent, newParent))
        {
            Quaternion localRot = obj.localRotation;
            Vector3 localPos = obj.localPosition;
            obj.parent = newParent;
            obj.localPosition = localPos;
            obj.localRotation = localRot;
        }
    }

    /// <summary> Swap ObjectA and ObjectB themselves. </summary>
    protected void SwapObjects()
    {
        Transform A_parent = objectA.transform.parent;
        SetParent(objectA.transform, objectB.transform.parent);
        SetParent(objectB.transform, A_parent);
    }
}
