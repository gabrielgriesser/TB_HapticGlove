using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button3dScript : MonoBehaviour
{
    private Animation animation;
    private List<GameObject> savedGameObjects;
    private List<GameObject> interactablesGameObjects;
    // Start is called before the first frame update

    void Start()
    {
        animation = GetComponent<Animation>();

        /*GameObject bench = GameObject.Find("BenchContainer");
        savedGameObjects = new List<GameObject>();
        interactablesGameObjects = new List<GameObject>();

        foreach (Transform g in bench.transform)
        {
            savedGameObjects.Add(g.gameObject);
            interactablesGameObjects.Add(g.gameObject);
        }

        foreach(GameObject g in savedGameObjects)
        {
            Debug.Log("Name : " + g.name);
        }

        foreach (GameObject g in interactablesGameObjects)
        {
            Debug.Log("Name : " + g.name);
        }*/

    }


    void OnTriggerEnter(Collider collide)
    {
        animation.wrapMode = WrapMode.Once;
        animation.Play();

        /*Debug.Log("REPOP DES OBJETS");

        foreach(GameObject g in interactablesGameObjects)
        {
            Debug.Log("Destroy " + g.name);

            Destroy(g);
        }

        foreach(GameObject g in savedGameObjects)
        {
            Debug.Log("Instantiate " + g.name);
            Instantiate(g);
        }*/
    }
}
