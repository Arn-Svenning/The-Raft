using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CSV
{
    public class CSVWriter
    {
        private string filePath;

        public List<string[]> data = new List<string[]>();

        public CSVWriter(string path)
        {
            filePath = Path.Combine(Application.streamingAssetsPath, path);
            // Ensure file exists with headers if not created
            if (!File.Exists(filePath))
            {
                using (StreamWriter writer = new StreamWriter(filePath, false)) // Overwrite mode to add headers
                {
                    writer.WriteLine("ID,Category,Description"); // Add headers
                }
            }

        }

        /// <summary>
        /// Add data to the CSV
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Category"></param>
        /// <param name="Description"></param>
        public void AddCSVData(int ID, string Category, string Description)
        {
            data.Add(new string[] { ID.ToString(), Category, Description });

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"{ID},{Category},{Description}");
            }
        }
    }
}

