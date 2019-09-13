using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manage the goal_2 (rain activated when triggered)
/// </summary>
public class Goal_Rain : MonoBehaviour
{
    #region attribute
    /// <summary> Effect rain </summary>
    public GameObject effectToShow;

    /// <summary> is Hi5 scene </summary>
    private bool isHi5Scene;
    
    /// <summary> last trigger object name (for Hi5 scene) </summary>
    string lastTriggerObjectName;
    #endregion

    #region monobehaviour
    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        
        if(currentScene.name.Contains("Hi5"))
        {
            isHi5Scene = true;
            lastTriggerObjectName = "";
        }

        if(effectToShow != null)
            effectToShow.SetActive(false);
    }

    /// <summary>
    /// On trigger enter, show rain.
    /// If Hi5 scene, need to ignore collision with object inside trigger object (they need to have same name)
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other) 
    {        
        if(effectToShow != null)
        {      
            if(!isHi5Scene)
            {
                effectToShow.SetActive(!effectToShow.activeSelf);
            }
            else
            {
                if(lastTriggerObjectName == other.name)
                {
                    Physics.IgnoreCollision(other, this.GetComponent<BoxCollider>());
                }
                else
                {
                    effectToShow.SetActive(!effectToShow.activeSelf);
                    lastTriggerObjectName = other.name;
                }
            }
        }
        
    }

    #endregion
}
