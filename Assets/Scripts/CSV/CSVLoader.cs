using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CSV
{
    public class CSVLoader : MonoBehaviour
    {
        void LoadCSV(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    bool firstLine = true;
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (firstLine) { firstLine = false; continue; } // Skip header
                        string[] values = line.Split(',');
                        Debug.Log($"ID: {values[0]}, Category: {values[1]}, Description: {values[2]}");
                    }
                }
            }
            else
            {
                Debug.LogError("CSV file not found!");
            }
        }
    }
}

