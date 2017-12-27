using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowTrigger : MonoBehaviour
{
    
    public GameObject TargetGameObject;
   

    void Start()
    {
      //  rend = ChangeObject.GetComponent<Renderer>();
      //  rend.enabled = true;
     //   rend.sharedMaterial = material[0];
    }

    void OnTriggerEnter(Collider col)
    {
        switch (col.tag)
        {
            case "MainCamera":
                //     Debug.Log("Object entered the trigger");
                TargetGameObject.SetActive(false);
                break;
        }
    }

    void OnTriggerStay(Collider col)
    {
        switch (col.tag)
        {
            case "MainCamera":
               // Debug.Log("Object is within the trigger");
                break;
        }
    }

    void OnTriggerExit(Collider col)
    {
        switch (col.tag)
        {
            case "MainCamera":
                //  Debug.Log("Object exited the trigger");
                //   rend.sharedMaterial = material[0];
                TargetGameObject.SetActive(true);
                break;
        }
    }
}
