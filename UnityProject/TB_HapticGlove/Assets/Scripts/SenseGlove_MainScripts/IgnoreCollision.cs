using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    public List<GameObject> objects;
    public GameObject leftGlove, rightGlove;

    // Start is called before the first frame update

    void Start()
    {
        foreach(Transform child in leftGlove.GetComponentsInChildren<Transform>())
        {
            if(child.gameObject.GetComponent<Collider>() != null && child.gameObject.GetComponent<Collider>().name.Contains("ParticleCollider"))
            {
                foreach(GameObject g in objects)
                {
                    if(g.GetComponent<Collider>() != null)
                    {
                        Physics.IgnoreCollision(g.GetComponent<Collider>(), child.GetComponent<Collider>());
                    }
                }
            }

        }

        foreach (Transform child in rightGlove.GetComponentsInChildren<Transform>())
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


    // Update is called once per frame
    void Update()
    {
        
    }
}
