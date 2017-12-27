// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Sharing;
using HoloToolkit.Sharing.Tests;

    /// <summary>
    /// Broadcasts the head transform of the local user to other users in the session,
    /// and adds and updates the head transforms of remote users.
    /// Head transforms are sent and received in the local coordinate space of the GameObject this component is on.
    /// </summary>

    public class RemoteObjectManager : MonoBehaviour
    { 
        public GameObject RemoteObject;
   
        private void Start()
        {
            CustomMessages.Instance.MessageHandlers[CustomMessages.TestMessageID.ObjectTransform] = UpdateObjectTransform;
        }

        private void Update()
        {
            // Grab the current head transform and broadcast it to all the other users in the session
            Transform objectTransform = RemoteObject.transform;//CameraCache.Main.transform;
            
            // Transform the object position and rotation from world space into local space
            Vector3 objectPosition = transform.InverseTransformPoint(objectTransform.position);
            Quaternion objectRotation = Quaternion.Inverse(transform.rotation) * objectTransform.rotation;
           
            CustomMessages.Instance.SendObjectTransform(objectPosition, objectRotation);
        }

        private void UpdateObjectTransform(NetworkInMessage msg)
        {
            // Parse the message
            long userID = msg.ReadInt64();

            Vector3 objectPos = CustomMessages.Instance.ReadVector3(msg);
            Quaternion objectRot = CustomMessages.Instance.ReadQuaternion(msg);
        
          //  RemoteObject.transform.parent = gameObject.transform;
            RemoteObject.transform.localPosition = objectPos;
            RemoteObject.transform.localRotation = objectRot;
        }

    }

