using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Internal;
using Hi5_Interaction_Core;

public class Hi5InteractionSceneManager : SingletonBehaviour<Hi5InteractionSceneManager>
{
    void Awake()
    {
         DontDestroy();
         Application.runInBackground = true;
    }
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
			SceneManager.LoadScene("TableScene");
        }
        if (Input.GetKeyDown(KeyCode.F2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
             SceneManager.LoadScene("Calibration");
        }
        if (Input.GetKeyDown(KeyCode.F1) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            SceneManager.LoadScene("Hi5Glove_Park");
        }
    }
}