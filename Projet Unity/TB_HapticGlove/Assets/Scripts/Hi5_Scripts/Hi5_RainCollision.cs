using UnityEngine;
using System.Collections.Generic;
using HI5;

/// <summary>
/// Rain particle collision manager
/// </summary>
public class Hi5_RainCollision : MonoBehaviour
{
    #region attribute
    //public ParticleSystem ps;

    private int duration;

    #endregion

    #region monobehaviour
    /// <summary>
    /// Start method
    /// </summary>
    private void Start()
    {     
        duration = 500;
    }

    /// <summary>
    /// On particle collision, if the collided object is child of left/right hand collider, send vibration
    /// </summary>
    /// <param name="obj"></param>
    void OnParticleCollision(GameObject obj)
    {
        if(FindParentContainsName(this.gameObject, "Left_Human_Collider"))
        {
            HI5_Manager.EnableLeftVibration(duration);
        }
        else
        {
            HI5_Manager.EnableRightVibration(duration);
        }
    }
    #endregion

    #region method
    /// <summary>
    /// Find first parent that contains given name
    /// </summary>
    /// <param name="childObject"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static GameObject FindParentContainsName(GameObject childObject, string name)
    {
        Transform t = childObject.transform;
        while (t.parent != null)
        {
            if (t.parent.name.Contains(name))
            {
                return t.parent.gameObject;
            }
            t = t.parent.transform;
        }
        return null; // Could not find a parent with given name.
    }
    #endregion
}

