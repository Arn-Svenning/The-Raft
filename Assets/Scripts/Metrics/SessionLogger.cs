using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SessionLogger : MonoBehaviour
{
    public static SessionLogger Instance { get; private set; }
    private string logFilePath;

    private static int numberOfIneractions = -1;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string fileName = $"SessionLog_{timestamp}.txt";
        string folderPath = Path.Combine(Application.persistentDataPath, "Logs");

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        logFilePath = Path.Combine(folderPath, fileName);
        Log("Session started.");

#if UNITY_EDITOR || DEVELOPMENT_BUILD
        // Open the folder containing the log file in File Explorer
        Application.OpenURL("file://" + folderPath);
#endif
    }


    public void Log(string message)
    {
        numberOfIneractions++;
        string logEntry = $"{DateTime.Now:HH:mm:ss} - {message}, [number Of Interactions: {numberOfIneractions}]";
        File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
        Debug.Log(logEntry);
    }
}
