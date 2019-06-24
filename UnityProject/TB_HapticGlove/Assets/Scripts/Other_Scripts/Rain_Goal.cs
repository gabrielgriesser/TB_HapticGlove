using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain_Goal : MonoBehaviour
{
    public GameObject EffectToShow;
    
    // Start is called before the first frame update
    void Start()
    {
        if(EffectToShow != null)
            EffectToShow.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) 
    {        
        if(EffectToShow != null)
        {      
            switch(EffectToShow.activeSelf)
            {
                case true:
                    EffectToShow.SetActive(false);
                    break;
                case false:
                    EffectToShow.SetActive(true);
                    break;
            }
            /*if(!EffectToShow.activeSelf && triggerObjectName == "" || !EffectToShow.activeSelf && other.name != triggerObjectName)
            {
                Debug.Log("Rain");      
                EffectToShow.SetActive(true);
                triggerObjectName = other.name;
                //return;
            }
            else if(EffectToShow.activeSelf && other.name != triggerObjectName)
            {
                Debug.Log("Not Rain");
                EffectToShow.SetActive(false);
                triggerObjectName = other.name;
            }*/
        }
        
    }
}
