using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BumblePux.Rebound2.Saving
{
    public interface ISaveLoader
    {
        void Save(string filePath, SaveData data);
        void Load(string filePath, SaveData data);
        void Delete(string filePath);
    }
}