using System.Collections.Generic;
using UnityEngine;

namespace BumblePux.Rebound.Achievements
{
    [CreateAssetMenu(fileName = "New Achievement Database", menuName = "Databases/Achievement Database")]
    public class AchievementDatabase : ScriptableObject
    {
        public List<Achievement> Achievements = new List<Achievement>();

		//--------------------------------------------------------------------------------
    }
}