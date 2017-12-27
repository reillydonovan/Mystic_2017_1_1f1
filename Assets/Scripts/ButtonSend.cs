using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Sharing;
using HoloToolkit.Sharing.Tests;

    public class ButtonSend 
    {

        int i = 0;
        public GameObject myButton;

        public void SendClick()
        {
            i++;
            CustomMessages.Instance.SendButtonClick(i.ToString());
            Debug.Log("Send Click" + i);
        }
    }
