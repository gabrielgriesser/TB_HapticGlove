using System.Collections.Generic;
using UnityEngine;

public class SensoHandsController : SensoBaseController
{
    #region attribute
    // Variables for hands objects
    public Senso.Hand[] Hands;
    public TextMesh instructionText;
    private int m_rightHandInd = -1;
    private int m_leftHandInd = -1;
    System.Diagnostics.Process[] srvName, uiName;
    private string sensoInfoText, userInfoText, userSrvText, userUIText, sensoBatteryText;
    #endregion

    #region monobehaviour
    // Initialization
    void Start()
    {
        sensoInfoText = @"Senso gloves

The Senso are developed by 
Senso Device Inc. 
They are wireless gloves used
for Virtual Reality. Senso Glove enables 
precise hands & fingers tracking with 
haptic feedback effect for every 
single finger 

Pinch with thumb and index finger 
to pick up an object in hand.

Connection to and calibration of the 
gloves is done through the 
SENSO_BLE_SERVER and SENSO_UI 
applications (see documentation on 
senso.me/docs/)" + "\n";


        if (Hands != null && Hands.Length > 0)
        {
            for (int i = 0; i < Hands.Length; ++i)
            {
                if (m_rightHandInd == -1 && Hands[i].HandType == Senso.EPositionType.RightHand)
                {
                    m_rightHandInd = i;
                    Hands[i].SetHandsController(this);
                }
                else if (m_leftHandInd == -1 && Hands[i].HandType == Senso.EPositionType.LeftHand)
                {
                    m_leftHandInd = i;
                    Hands[i].SetHandsController(this);
                }
            }
        }
        base.Start();

    }

    // Every frame
    void Update()
    {
        base.Update();
        if (sensoThread != null)
        {
            UpdateInfoText();

            var datas = sensoThread.UpdateData();
            if (datas != null)
            {

                bool rightUpdated = false, leftUpdated = false;
                while (datas.Count > 0)
                {
                    var parsedData = datas.Pop();
                    if (parsedData.type.Equals("position"))
                    {
                        if ((m_rightHandInd != -1 && !rightUpdated) || (m_leftHandInd != -1 && !leftUpdated))
                        {
                            var handData = JsonUtility.FromJson<Senso.HandDataFull>(parsedData.packet);

                            if (handData.data.handType == Senso.EPositionType.RightHand && m_rightHandInd != -1 && !rightUpdated)
                            {
                                // GABRIEL MODIF
                                handData.data.handPosition = Hands[0].transform.localPosition;
                                SetHandPose(ref handData, m_rightHandInd);

                                Hands[0].SetBattery(handData.data.battery);

                                rightUpdated = true;
                            }
                            if (handData.data.handType == Senso.EPositionType.LeftHand && m_leftHandInd != -1 && !leftUpdated)
                            {
                                // GABRIEL MODIF
                                handData.data.handPosition = Hands[1].transform.localPosition;
                                SetHandPose(ref handData, m_leftHandInd);

                                Hands[1].SetBattery(handData.data.battery);

                                leftUpdated = true;
                            }

                            if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
                            {
                                Hands[0].transform.localPosition += new Vector3(0, 0.015f, 0);
                                Hands[1].transform.localPosition += new Vector3(0, 0.015f, 0);

                            }
                            else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
                            {
                                Hands[0].transform.localPosition -= new Vector3(0, 0.015f, 0);
                                Hands[1].transform.localPosition -= new Vector3(0, 0.015f, 0);
                            }
                        }

                    }

                }
            }
        }

    }
    #endregion

    #region methods
    /// <summary>
    /// Send vibration to Senso
    /// </summary>
    /// <param name="handType"></param>
    /// <param name="finger"></param>
    /// <param name="duration"></param>
    /// <param name="strength"></param>
    public void SendVibro(Senso.EPositionType handType, Senso.EFingerType finger, ushort duration, byte strength)
    {
        sensoThread.VibrateFinger(handType, finger, duration, strength);
    }

    /// <summary>
    /// Send vibration to each finger of the given hand (handType)
    /// </summary>
    /// <param name="handType"></param>
    /// <param name="duration"></param>
    /// <param name="strength"></param>
    public void SendVibroToEachFinger(Senso.EPositionType handType, ushort duration, byte strength)
    {
        sensoThread.VibrateEachFinger(handType, duration, strength);
    }

    /// <summary>
    /// Returns true if the gameObject obj is a child of the right glove
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public bool IsRight(GameObject obj)
    {
        Transform t = obj.transform;
        while (t.parent != null)
        {
            if (t.parent.name.Contains("Right"))
            {
                return true;
            }
            t = t.parent.transform;
        }
        return false; // Could not find a parent
    }

    private void SetHandPose(ref Senso.HandDataFull handData, int ind)
    {
        if (Hands[ind].MacAddress == null)
        {
            Hands[ind].SetMacAddress(handData.src);
        }
        Hands[ind].SetSensoPose(handData.data);
    }

    /// <summary>
    /// Receive collision from a finger.
    /// This method is called from the script Senso_HapticFeedback
    /// </summary>
    /// <param name="hand"></param>
    /// <param name="finger"></param>
    /// <param name="duration"></param>
    /// <param name="strength"></param>
    public void ReceiveCollision(Senso.EPositionType hand, Senso.EFingerType finger, ushort duration, byte strength)
    {
        SendVibro(hand, finger, duration, strength);
    }

    /// <summary>
    /// Update info text depending on the connection status of the gloves.
    /// </summary>
    public void UpdateInfoText()
    {
        switch (sensoThread.State)
        {
            case Senso.NetworkState.SENSO_CONNECTING:
                if (SensoAppsRunning())
                {
                    userSrvText = "";
                    userUIText = "";
                    userInfoText = "Try to connect to Senso server";
                }
                break;

            case Senso.NetworkState.SENSO_CONNECTED:
                userInfoText = "Senso(s) connected";

                if (Hands[0].BatteryLevel != -1 || Hands[1].BatteryLevel != -1)
                {
                    sensoBatteryText = "\nRight glove battery : " + Hands[0].BatteryLevel + "\nLeft glove batery : " + Hands[1].BatteryLevel;
                    userInfoText += " and ready";
                }
                else
                {
                    sensoBatteryText = "\nPlease calibrate connected glove(s)";
                }
                
                userInfoText += sensoBatteryText;
                break;

            case Senso.NetworkState.SENSO_DISCONNECTED:
                userInfoText = "Unable to connect to Senso server";
                break;
        }
        instructionText.text = sensoInfoText + "\n" + userSrvText + "\n" + userUIText + "\n" + userInfoText;
    }

    /// <summary>
    /// Check if SENSO_BLE_SERVER and SENSO_UI are running
    /// We cannot configure these applications ourselves for you. 
    /// Please read the documentation on https://senso.me/docs/ for more help.
    /// </summary>
    /// <returns></returns>
    public bool SensoAppsRunning()
    {
        srvName = System.Diagnostics.Process.GetProcessesByName("SENSO_BLE_SERVER");
        uiName = System.Diagnostics.Process.GetProcessesByName("SENSO_UI");
        userInfoText = "";

        if (srvName.Length == 0)
        {
            userSrvText = "Launch and configure \nSENSO_BLE_SERVER.exe once per glove\nOr launch run.cmd for both gloves.";
        }
        if (uiName.Length == 0)
        {
            userUIText = "Launch and configure \nSENSO_UI.exe once per glove \nOr just launch run.cmd for both gloves.";
        }

        if (srvName.Length != 0 && uiName.Length != 0)
            return true;
        else
        {
            Hands[0].SetBattery(-1);
            Hands[1].SetBattery(-1);
            return false;
        }

    }

    #endregion
}
