using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// Rain particle collision manager
/// </summary>
public class Falcon_RainCollision : MonoBehaviour
{
    #region attribute
    private ParticleSystem rainParticleSystem;
    #endregion

    #region monobehaviour

    /// <summary>
    /// Start function
    /// </summary>
    private void Start()
    {
        rainParticleSystem = GetComponent<ParticleSystem>();

    }

    /// <summary>
    /// On particle collision, if the collided object is a falcon (name = "Tip"), apply a force down
    /// </summary>
    /// <param name="obj"></param>
    void OnParticleCollision(GameObject obj)
    {
        if (obj.name == "Tip")
        {
            //Debug.Log("Rain touched falcon N°" + obj.transform.parent.GetComponent<SphereManipulator>().falcon_num);
            FalconUnity.applyForce(obj.transform.parent.GetComponent<SphereManipulator>().falcon_num, new Vector3(0, -4f, 0), 0.1f);
        }
    }
    #endregion
}

