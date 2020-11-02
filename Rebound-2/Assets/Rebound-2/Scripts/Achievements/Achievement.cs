using UnityEngine;

namespace BumblePux.Rebound.Achievements
{
    [System.Serializable]
    public class Achievement
    {
        public string Name;
        public string Description;
        public Sprite Icon;

		//--------------------------------------------------------------------------------
        public Achievement(string name, string description, Sprite icon)
        {
            Name = name;
            Description = description;
            Icon = icon;
        }
    }
}