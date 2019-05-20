using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button3dScript : MonoBehaviour
{
    private Animation animation;
    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animation>();
    }


    void OnTriggerEnter(Collider collide)
    {
        // Debug.Log("Triggered !");
        animation.wrapMode = WrapMode.Once;
        animation.Play();
    }
}
