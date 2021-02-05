using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace BumblePux.Rebound2.Saving
{
    public class JsonSaveLoader : MonoBehaviour, ISaveLoader
    {
        [Header("File Settings")]
        [SerializeField] private string FileName = "Save";
        [SerializeField] private string FileExtension = "json";
        [SerializeField] private bool UsePrettyPrint = true;

        public void Save(string filePath, SaveData data)
        {
            var json = JsonUtility.ToJson(data, UsePrettyPrint);
            File.WriteAllText(GetSaveFromFilePath(filePath), json);
        }

        public void Load(string filePath, SaveData data)
        {
            if (!File.Exists(GetSaveFromFilePath(filePath))) return;

            var json = File.ReadAllText(GetSaveFromFilePath(filePath));
            JsonUtility.FromJsonOverwrite(json, data);
        }

        public void Delete(string filePath)
        {
            if (!File.Exists(GetSaveFromFilePath(filePath))) return;

            File.Delete(GetSaveFromFilePath(filePath));
        }

        private string GetSaveFromFilePath(string filePath)
        {
            return Path.Combine(filePath, FileName, ".", FileExtension);
        }
    }
}