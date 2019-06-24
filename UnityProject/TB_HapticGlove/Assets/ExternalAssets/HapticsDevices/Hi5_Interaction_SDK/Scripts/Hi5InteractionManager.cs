using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Hi5_Interaction_Core
{
    public class Hi5InteractionManager : MonoBehaviour
    {

        public TextMesh instrText;

		static Hi5InteractionManager _instance;
        internal  Hi5_Glove_Interaction_Hand mLeftGlove = null;
        internal Hi5_Glove_Interaction_Hand mRightGlove = null;
        internal Hi5_Glove_Interaction_Message mMessage = null;
        void Awake()
        {
            _instance = this;
            if (mMessage == null)
                mMessage = new Hi5_Glove_Interaction_Message();
        }

		public static Hi5InteractionManager Instance
        {
            get
            {
                return _instance;
            }
        }

        public Hi5_Glove_Interaction_Message GetMessage()
        {
            if(mMessage == null)
                mMessage = new Hi5_Glove_Interaction_Message();
            return mMessage;
        }

        private void Update()
        {
            if (mLeftGlove == null)
            {
                Hi5_Glove_Interaction_Hand[] temps = gameObject.GetComponentsInChildren<Hi5_Glove_Interaction_Hand>();
                if (temps != null && temps.Length > 0)
                {
                    for (int i = 0; i < temps.Length; i++)
                    {
                        if (temps[i].m_IsLeftHand)
                            mLeftGlove = temps[i];
                    }
                }
            }
            if (mRightGlove == null)
            {
                Hi5_Glove_Interaction_Hand[] temps = gameObject.GetComponentsInChildren<Hi5_Glove_Interaction_Hand>();
                if (temps != null && temps.Length > 0)
                {
                    for (int i = 0; i < temps.Length; i++)
                    {
                        if (!temps[i].m_IsLeftHand)
                            mRightGlove = temps[i];
                    }
                }
            }
            if(mLeftGlove == null && mRightGlove == null)
            {
                SetDebugMessage("Waiting for 2 gloves");
            }
            else if(mLeftGlove == null || mRightGlove == null)
            {
                SetDebugMessage("Waiting for 1 gloves");
            }
            else
            {
                SetDebugMessage("Gloves connected and ready.\nPress F2 to calibrate (require Vive Tracker)");
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
            {
                
                //GameObject.Find("Hi5GloveHands").transform.position += new Vector3(0, 0.015f, 0);

            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
            {
               //GameObject.Find("Hi5GloveHands").transform.position -= new Vector3(0, 0.015f, 0);

            }
        }

        private void SetDebugMessage(string message)
        {
            //Debug.Log(message);
            if(instrText != null)
                instrText.text = message;
        }
    }

}