using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class Falcon_Control : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Process.GetProcessesByName("FalconServer").Length > 1)
        {
            UnityEngine.Debug.Log("Server started");
        }
        else
        {
            try
            {
                Process.Start("FalconServer");
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log("Erreur " + e);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
