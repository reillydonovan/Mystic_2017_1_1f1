using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour {
    public GameObject ObjectToMove;
    
    // Use this for initialization

    public void MoveUp()
    {
        Vector3 ObjectPosition = ObjectToMove.transform.position;
        ObjectPosition.y += 0.1f;
        ObjectToMove.transform.position = ObjectPosition;
      //  Debug.Log("moved up" + ObjectPosition.y);
    }
    public void MoveDown()
    {
        Vector3 ObjectPosition = ObjectToMove.transform.position;
        ObjectPosition.y -= 0.1f;
        ObjectToMove.transform.position = ObjectPosition;
      //  Debug.Log("moved down" + ObjectPosition.y);
    }
}
