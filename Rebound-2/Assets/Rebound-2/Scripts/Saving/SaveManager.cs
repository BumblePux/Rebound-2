using BumblePux.Rebound2.Utils;
using System.IO;
using UnityEngine;

namespace BumblePux.Rebound2.Saving
{
    public class SaveManager : Singleton<SaveManager>
    {
        [Header("File Settings")]
        [TextArea(1, 4)]
        [SerializeField] private string FilePath;

        private SaveData saveData = new SaveData();
        private ISaveLoader saveLoader;

        private void Start()
        {
            FilePath = Application.persistentDataPath;

            saveLoader = GetComponent<ISaveLoader>();
        }

        public void Save()
        {
            saveData.MetaData.Capture();
            CaptureSaveables();
            saveLoader.Save(FilePath, saveData);
        }

        public void Load()
        {
            saveLoader.Load(FilePath, saveData);
            RestoreSaveables();
        }

        public void Delete()
        {
            saveLoader.Delete(FilePath);
            ResetSaveData();
        }

        private void ResetSaveData()
        {
            saveData = new SaveData();
        }

        private void CaptureSaveables()
        {
            var saveables = FindObjectsOfType<Saveable>();
            foreach (var saveable in saveables)
            {
                saveable.CaptureState(saveData);
            }
        }

        private void RestoreSaveables()
        {
            var saveables = FindObjectsOfType<Saveable>();
            foreach (var saveable in saveables)
            {
                saveable.RestoreState(saveData);
            }
        }
    }
}