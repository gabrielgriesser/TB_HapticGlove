using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script used to manage collision 
/// </summary>
/*public class SenseGlove_IgnoreCollision : MonoBehaviour
{
    public List<GameObject> objects;
    public GameObject leftGlove, rightGlove;

    // Start is called before the first frame update

    void Start()
    {
        IgnoreCollisionBetweenGloveAndObjects(rightGlove);
        IgnoreCollisionBetweenGloveAndObjects(leftGlove);
    }

    private void IgnoreCollisionBetweenGloveAndObjects(GameObject glove)
    {
        foreach (Transform child in glove.GetComponentsInChildren<Transform>())
        {
            if (child.gameObject.GetComponent<Collider>() != null && child.gameObject.GetComponent<Collider>().name.Contains("ParticleCollider"))
            {
                foreach (GameObject g in objects)
                {
                    if (g.GetComponent<Collider>() != null)
                    {
                        Physics.IgnoreCollision(g.GetComponent<Collider>(), child.GetComponent<Collider>());
                    }
                }
            }
        }
    }
}*/
