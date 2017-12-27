using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Sharing;
using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Sharing.Tests;

public class DropHelper : MonoBehaviour, IInputClickHandler {

	// Use this for initialization
	void Start () {
        CustomMessages.Instance.MessageHandlers[CustomMessages.TestMessageID.Drop] = OnDrop;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnDrop(NetworkInMessage msg)
    {
        DoDrop(false);
    }

    void Reset()
    {
        var rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        this.transform.position = new Vector3(0f, 0f, 2f);
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        DoDrop(true);
    }

    public void DoDrop(bool sendMessage)
    {
        GetComponent<Rigidbody>().useGravity = true;
        Invoke("Reset", 5);
       // if (sendMessage)
       //     CustomMesssages.Instance.SendCommand(CustomMessages.TestMessageID.Drop);
    }


}
