using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Hi5_Interaction_Core
{
    public class Hi5InteractionManager : MonoBehaviour
    {

        public TextMesh instrText;
        private string strInfoGlove, strConnexionGlove;
        private HI5.HI5_GloveStatus gloveStatut;


		static Hi5InteractionManager _instance;
        internal  Hi5_Glove_Interaction_Hand mLeftGlove = null;
        internal Hi5_Glove_Interaction_Hand mRightGlove = null;
        internal Hi5_Glove_Interaction_Message mMessage = null;
        void Awake()
        {
            gloveStatut = HI5.HI5_Manager.GetGloveStatus();

            _instance = this;
            if (mMessage == null)
                mMessage = new Hi5_Glove_Interaction_Message();

            strInfoGlove = @"Hi5 VR GLOVES

The Hi5 gloves are developed 
by Noitom. They provide a complete 
hand and finger recognition. 
They have a vibration system 
on each wrist

The handling of an object that has 
a mesh collider is not precise, 
especially since it is small.

Interact with objects and buttons. 
A button allows access to the 
calibration scene";
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
            instrText.text = strInfoGlove + strConnexionGlove;
            if(!gloveStatut.IsLeftGloveAvailable && !gloveStatut.IsRightGloveAvailable)
            {
                strConnexionGlove=("\nWaiting for 2 gloves");
            }
            else if(gloveStatut.IsLeftGloveAvailable && gloveStatut.IsRightGloveAvailable)
            {
                strConnexionGlove = "\nGloves connected and ready. \nClick on the button to calibrate them. \n(require Vive Tracker)";
            }
            else 
            {
                strConnexionGlove="\nWaiting for 1 glove";
            }

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
            
        }

        private void SetDebugMessage(string message)
        {
            if(instrText != null)
                instrText.text = strInfoGlove + strConnexionGlove;
        }
    }

}