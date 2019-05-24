//
// Rain Maker (c) 2016 Digital Ruby, LLC
// http://www.digitalruby.com
//

using UnityEngine;
using System.Collections.Generic;

public class RainCollision : MonoBehaviour
{
    public ParticleSystem RainParticleSystem;

    public SenseGlove_Object senseGloveRight, senseGloveLeft;

    private string fingerName;

    private bool[] fingerToBuzz;

    private int magnitude, duration;

    private void Start()
    {
        fingerToBuzz = new bool[] { false, false, false, false, false };
        magnitude = 100;
        duration = 50;
    }

    private void Update()
    {

    }

    private void OnParticleCollision(GameObject obj)
    {
        if (obj.transform.IsChildOf(GameObject.Find("SenseGloveHands").transform))
        {
            fingerName = obj.name.Replace("ParticleCollider", "");
            Debug.Log(fingerName);

            switch (fingerName)
            {
                case "palm":
                    SetAllFinger(true);
                    break;
                case "thumb":
                    fingerToBuzz[0] = true;
                    break;
                case "index":
                    fingerToBuzz[1] = true;
                    break;
                case "middle":
                    fingerToBuzz[2] = true;
                    break;
                case "ring":
                    fingerToBuzz[3] = true;
                    break;
                case "pinky":
                    fingerToBuzz[4] = true;
                    break;
            }

            if (obj.transform.IsChildOf(senseGloveRight.transform))
            {
                SenddBuzzCmd(senseGloveRight);
                SetAllFinger(false);
            }
            else if (obj.transform.IsChildOf(senseGloveLeft.transform))
            {
                SenddBuzzCmd(senseGloveLeft);
                SetAllFinger(false);
            }
        }
    }

    private void SenddBuzzCmd(SenseGlove_Object senseGlove)
    {
        senseGlove.SendBuzzCmd(fingerToBuzz, magnitude, duration);
    }

    private void SetAllFinger(bool value)
    {
        for (int i = 0; i < fingerToBuzz.Length; i++)
            fingerToBuzz[i] = value;
    }
}
