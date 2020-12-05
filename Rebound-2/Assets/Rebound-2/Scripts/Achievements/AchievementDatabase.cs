using Malee.List;
using System.Collections.Generic;
using UnityEngine;

namespace BumblePux.Rebound.Achievements
{
    [CreateAssetMenu(fileName = "New Achievement Database", menuName = "Databases/Achievement Database")]
    public class AchievementDatabase : ScriptableObject
    {
        [Reorderable]
        public AchievementsArray Achievements;

		//--------------------------------------------------------------------------------
        [System.Serializable]
        public class AchievementsArray : ReorderableArray<Achievement> { }
    }
}