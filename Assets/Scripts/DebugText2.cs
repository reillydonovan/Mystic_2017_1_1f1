/*
 * ==================================================================
 *  DebugText.cs
 *
 *  Created by Parag Padubidri
 *  Copyright (c) 2016, Parag Padubidri. All rights reserved.
 * ==================================================================
 */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class DebugText2 : MonoBehaviour
{

    //public static Text debugTextUI;
    public static int linecount;
    public static List<string> debugStringList = new List<string>();
    public static int logLength = 10;

    //Countdown Timer variables
    private static float debugDisplayTimer = 3.0f;
    private static float debugDisplayTimerCountdown = 0.0f;

    // Update is called once per frame
    void Update()
    {

        //Check if timer is set
        if (debugDisplayTimerCountdown > 0)
        {
            //Start Counting down the timer
            debugDisplayTimerCountdown -= Time.deltaTime;

            //Reset timer if it reaches 0 & remove 1st list message
            if (debugDisplayTimerCountdown <= 0)
            {
                debugDisplayTimerCountdown = debugDisplayTimer;
                if (debugStringList.Count != 0)
                {
                    debugStringList.RemoveAt(0);
                }
                //reset timer also if list is empty 
                else if (debugStringList.Count == 0)
                {
                    debugDisplayTimerCountdown = 0;
                }
            }
        }
    }

    //Static method to set List message
    public static void Text(object message)
    {
        debugStringList.Add(message.ToString());

        debugDisplayTimerCountdown = debugDisplayTimer;

        if (debugStringList.Count > logLength)
        {
            debugStringList.RemoveAt(0);
        }
    }

    //Immediate GUI
    void OnGUI()
    {
        for (int i = 0; i < debugStringList.Count; i++)
        {
            GUI.contentColor = Color.red;
            GUI.skin.GetStyle("label").fontSize = 14;
            GUILayout.Label(debugStringList[i]);
        }


    }
}