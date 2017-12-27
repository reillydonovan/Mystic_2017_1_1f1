using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobblyGrid : MonoBehaviour {

    public GameObject Block;
    public GameObject[] BlockList;
    public Material BlockMat;

    public int GridX, GridZ;
    private int GridSize, BlockIndex;
	// Use this for initialization
	void Start () {

        GridSize = GridX * GridZ;
        BlockList = new GameObject[GridSize];

        for(int x = 1; x <= GridX; x++)
        {
            for(int z = 1; z <= GridZ; z++)
            {
                GameObject BlockInstance = (GameObject)Instantiate(Block);
                BlockInstance.transform.parent = this.transform;
                BlockInstance.name = "Block " + BlockIndex;

                BlockList[BlockIndex] = BlockInstance;
                BlockList[BlockIndex].transform.position = new Vector3(x-1, 0, z-1);
                BlockIndex++;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
