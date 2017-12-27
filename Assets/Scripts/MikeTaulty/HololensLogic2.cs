using HoloToolkit.Sharing;
using HoloToolkit.Sharing.Tests;
using UnityEngine;

public class HoloLensLogic2 : MonoBehaviour
{

 //   public GameObject headsetPrefab;
 

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