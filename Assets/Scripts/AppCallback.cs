using UnityEngine;
using System.Collections;

public class AppCallback : MonoBehaviour
{
    public TextMesh debugTextMesh;
    public string output = "";
    public string stack = "";
    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }
    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }
    void HandleLog(string logString, string stackTrace, LogType type)
    {
        output = logString;
        stack = stackTrace;
        debugTextMesh.text = logString;
    }
    

}