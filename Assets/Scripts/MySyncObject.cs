using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Sharing;
using HoloToolkit.Sharing.SyncModel;
using HoloToolkit.Sharing.Spawning;

public class MySyncObject : SyncObject
{
    [SyncData]
    public SyncTransform OtherSyncObject;

    [SyncData]
    public SyncFloat FloatValue;
}