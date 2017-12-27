using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Sharing;
using HoloToolkit.Sharing.Tests;

public class HLogic3 : MonoBehaviour {


    void Start()
    {
        CustomMessages.Instance.MessageHandlers[CustomMessages.TestMessageID.RemoteHeadsetPosition] =
                OnHeadTransformMessageReceived;

    }


    void OnHeadTransformMessageReceived(NetworkInMessage msg)
    {
        long userID = msg.ReadInt64();
        Vector3 headPos = CustomMessages.Instance.ReadVector3(msg);
        Vector3 headForward = CustomMessages.Instance.ReadVector3(msg);

        this.transform.localPosition = headPos;
        this.transform.forward = headForward;
    }
}
