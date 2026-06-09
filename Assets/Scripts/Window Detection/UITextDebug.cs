using UnityEngine;
using TMPro; // if using TextMeshPro
using System.Collections.Generic;

public class UILogger : MonoBehaviour
{
    public TextMeshProUGUI logText; // assign in inspector

    private Queue<string> logs = new Queue<string>();
    public int maxLogs = 20;

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
        logs.Enqueue(logString);

        if (logs.Count > maxLogs)
            logs.Dequeue();

        logText.text = string.Join("\n", logs);
    }
}