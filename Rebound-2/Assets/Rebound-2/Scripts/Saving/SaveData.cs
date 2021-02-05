using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BumblePux.Rebound2.Saving
{
    [Serializable]
    public class SaveData
    {
        public MetaData MetaData = new MetaData();
        public PlayerData PlayerData = new PlayerData();
        public Stats Stats = new Stats();
        public AudioSettings AudioSettings = new AudioSettings();
    }

    [Serializable]
    public class MetaData
    {
        public string Version;
        public string CreationDate;
        public string PlayTime;

        public void Capture()
        {
            Version = Application.version;

            if (string.IsNullOrEmpty(CreationDate)) 
                CreationDate = DateTime.Now.ToString();

            float playTime = GetPlayTimeAsValue();
            playTime += Time.realtimeSinceStartup;
            PlayTime = TimeSpan.FromSeconds(playTime).TotalMilliseconds.ToString();
        }

        public float GetPlayTimeAsValue()
        {
            float playTime = 0f;
            float.TryParse(PlayTime, out playTime);
            return playTime;
        }
    }

    [Serializable]
    public class PlayerData
    {
        public int Coins;
        public string EquippedShip = "BumblePux";
        public List<string> UnlockedShips = new List<string> { "BumblePux" };
    }

    [Serializable]
    public class Stats
    {
        public int LifetimeCoins;
        public int TimedModeHighestCombo;
        public int LifetimeStarsPopped;
    }

    [Serializable]
    public class AudioSettings
    {
        public bool MusicEnabled = true;
        public bool SoundEnabled = true;
    }
}