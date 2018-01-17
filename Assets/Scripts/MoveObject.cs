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

    public void MoveForward()
    {
        Vector3 ObjectPosition = ObjectToMove.transform.position;
        ObjectPosition.z += 0.1f;
        ObjectToMove.transform.position = ObjectPosition;
        //  Debug.Log("moved down" + ObjectPosition.y);
    }

    public void MoveBack()
    {
        Vector3 ObjectPosition = ObjectToMove.transform.position;
        ObjectPosition.z -= 0.1f;
        ObjectToMove.transform.position = ObjectPosition;
        //  Debug.Log("moved down" + ObjectPosition.y);
    }

    public void MoveLeft()
    {
        Vector3 ObjectPosition = ObjectToMove.transform.position;
        ObjectPosition.x += 0.1f;
        ObjectToMove.transform.position = ObjectPosition;
        //  Debug.Log("moved down" + ObjectPosition.y);
    }
    public void MoveRight()
    {
        Vector3 ObjectPosition = ObjectToMove.transform.position;
        ObjectPosition.x -= 0.1f;
        ObjectToMove.transform.position = ObjectPosition;
        //  Debug.Log("moved down" + ObjectPosition.y);
    }
}
