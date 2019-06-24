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
            //Debug.Log("LEFTTT" + obj.name);
            HI5_Manager.EnableLeftVibration(duration);
        }
        else
        {
            //Debug.Log("RIGHTTT" + obj.name);
            HI5_Manager.EnableRightVibration(duration);
        }
        /*
        if(this.transform.IsChildOf(GameObject.Find("HI5_Left_Human_Collider").transform))
        {
            Debug.Log("LEFTTT" + obj.name);
            HI5_Manager.EnableLeftVibration(duration);
        }
        else if(this.transform.IsChildOf(GameObject.Find("HI5_Right_Human_Collider").transform))
        {
            Debug.Log("RIGHTTT" + obj.name);
            HI5_Manager.EnableRightVibration(duration);
        }
        */
    }
    #endregion

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
}

