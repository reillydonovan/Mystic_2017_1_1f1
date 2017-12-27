using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HoloToolkit.Unity.InputModule;


public class RectAnim : MonoBehaviour
{

    Animator anim;
    // Use this for initialization
    void Start()
    {

        anim = GetComponent<Animator>();

    }
    // Update is called once per frame
    void Update()
    {

        if (!anim.GetBool("idle"))
        {
            anim.Play("RectMove");

        }
        else if (anim.GetBool("idle"))
        {

            anim.Play("RectIdle");
        }

    }

    void OnSelect()
    {


        if (anim.GetBool("idle"))
        {

            anim.SetBool("idle", false);
        }
        else
        {
            anim.SetBool("idle", true);

        }


    }

    void OnMouseDown()
    {

        OnSelect();
    }

}
