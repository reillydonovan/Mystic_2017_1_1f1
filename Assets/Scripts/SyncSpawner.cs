using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Sharing.Spawning;

public class SyncSpawner : Singleton<SyncSpawner>
{

    public GameObject syncPrefab;
    public PrefabSpawnManager spawnManager;
	// Use this for initialization

    public void SyncSpawnPrefab()
    {
        SyncSpawnedObject spawnedObject = new SyncSpawnedObject();
        Vector3 position = syncPrefab.gameObject.transform.position;
        spawnManager.Spawn(spawnedObject, position, syncPrefab.gameObject.transform.rotation, null, "syncPrefab", true);
            
    }
}
