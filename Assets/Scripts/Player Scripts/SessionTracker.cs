using System;
using System.IO;
using UnityEngine;

public class SessionTracker : MonoBehaviour
{
    // Variables
    #region

    private float sessionTime = 0f;
    private string filePath;

    #endregion

    // Start
    #region

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "SessionTime.txt");

        // Creates text file if its not there
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "Session Log\n");
            Debug.Log("Created session trakcer at: " + filePath);
        }
    }

    #endregion

    // Update
    #region

    // Update is called once per frame
    void Update()
    {
        sessionTime += Time.deltaTime;
    }

    #endregion

    // OnApplicationQuit
    #region

    // Saves the text file when quitting
    void OnApplicationQuit()
    {
        SaveSessionTime();
    }

    #endregion

    // SaveSessionTime
    #region

    // Writes the session time to a text file
    void SaveSessionTime()
    {
        int minutes = Mathf.FloorToInt(sessionTime / 60f);
        int seconds = Mathf.FloorToInt(sessionTime % 60f);

        string timeString = $"{DateTime.Now} - Session Time: {minutes}m {seconds}s";

        File.AppendAllText(filePath, timeString + "\n");

        Debug.Log("Session Tracker updated: " + timeString);
    }

    #endregion
}
