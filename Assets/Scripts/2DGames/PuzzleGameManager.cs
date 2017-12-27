using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleGameManager : MonoBehaviour {

    public Image piece;
    public Image placeHolder;
    float phWidth, phHeight;

	// Use this for initialization
	void Start () {
        CreatePlaceHolders();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreatePlaceHolders()
    {
        phWidth = 100;
        phHeight = 100;

        float nbRows, nbColumns;
        nbRows = 5;
        nbColumns = 5;

        for(int i = 0; i < 25; i++)
        {
            Vector3 centerPosition = new Vector3();
            centerPosition = GameObject.Find("RightSide").transform.position;

            float row, column;

            row = i % 5;
            column = i / 5;

            Vector3 phPosition = new Vector3(centerPosition.x + phWidth * (row - nbRows / 2),
                centerPosition.y - phHeight * (column - nbColumns / 2), centerPosition.z);

            Image ph = (Image)(Instantiate(placeHolder, phPosition, Quaternion.identity));

            ph.tag = "" + (i + 1);
            ph.name = "PH" + (i + 1);

            ph.transform.SetParent(GameObject.Find("Canvas").transform);
        }
    }
}
