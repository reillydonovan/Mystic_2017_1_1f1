using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowTrigger : MonoBehaviour
{
    
    public GameObject TerrainGameObject;
    public GameObject SourceGameObject;
    public GameObject PuddleGameObject;


    void Start()
    {
        //  rend = ChangeObject.GetComponent<Renderer>();
        //  rend.enabled = true;
        //   rend.sharedMaterial = material[0];
        SourceGameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider col)
    {
        switch (col.tag)
        {
            case "MainCamera":
                //     Debug.Log("Object entered the trigger");
                TerrainGameObject.SetActive(false);
                PuddleGameObject.SetActive(false);
                SourceGameObject.SetActive(true);
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
                PuddleGameObject.SetActive(true);
                TerrainGameObject.SetActive(true);
                SourceGameObject.SetActive(false);
                break;
        }
    }
}
