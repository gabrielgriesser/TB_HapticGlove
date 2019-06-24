using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falcon_PickupManager : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGER WITH : " + other.gameObject.name);
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("TRIGGER EXIT WITH : " + other.gameObject.name);
    }
}
