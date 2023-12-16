using System.IO;
using System;
using UnityEngine;

namespace Assets.Scripts.DataPersistence
{
    public class FileDataHandler
    {
        private string dataDirPath = "";

        private string dataFileName = "";

        public FileDataHandler(string path, string fileName)
        {
            dataDirPath = path;
            dataFileName = fileName;
        }

        public void Load(ref GameData data)
        {
            string fullPath = System.IO.Path.Combine(dataDirPath, dataFileName);

            if(File.Exists(fullPath))
            {
                try
                {
                    string dataLoad = "";

                    // readFile
                    using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            dataLoad = reader.ReadToEnd();
                        }
                    }

                    data = JsonUtility.FromJson<GameData>(dataLoad); 
                }
                catch(Exception e)
                {
                    Debug.LogError(e.Message);
                }
            }

        }

        public void Save(GameData data)
        {
            string fullPath = System.IO.Path.Combine(dataDirPath, dataFileName);
            
            try
            {
                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(fullPath));

                // format to JSON
                string dataStore = JsonUtility.ToJson(data, true);
                Debug.Log(dataStore);
                // write file
                using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    using(StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(dataStore);
                    }
                }

            }
            catch(Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

    }
}
