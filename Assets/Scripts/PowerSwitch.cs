using UnityEngine;
using System.Collections;

public class PowerSwitch : MonoBehaviour {

    public GameObject go;

    void OnSelect()
    {

        if(go.activeSelf)
        {
            go.SetActive(false);
            
        }
        else
        {
            go.SetActive(true);

        }

    }
}
