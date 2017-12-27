using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePingo : MonoBehaviour
{

    public GameObject[] togglePingo;
    bool toggle;
    bool toggle2;

    void Start()
    {

        toggle = false;
        toggle2 = true;
    }
    // Use this for initialization
    public void togglePingoObject()
    {
        toggle = !toggle;
        toggle2 = !toggle2;
        togglePingo[0].SetActive(toggle);
        togglePingo[1].SetActive(toggle2);

    }
}
