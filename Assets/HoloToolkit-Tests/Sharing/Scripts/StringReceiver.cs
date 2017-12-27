using System;
using System.Collections.Generic;
using HoloToolkit.Unity;
using UnityEngine;
using HoloToolkit.Sharing.Tests;
using HoloToolkit.Sharing;
    public class StringReceiver 
    {
    

        // public GameObject ReceivingString; //Yes it's a button, I'll just get the text component in his Text child.

        void Start()
        {
            CustomMessages.Instance.MessageHandlers[CustomMessages.TestMessageID.Clicked] = OnMessageReceived;
        }

        void OnMessageReceived(NetworkInMessage msg)
        {
            msg.ReadInt64();
            string i = msg.ReadString();
            //  ReceivingString.text = i;
            Debug.Log("Receiving at " + i.ToString());
        }
    }
