using BumblePux.Rebound.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace BumblePux.Rebound.Achievements
{
    public abstract class AchievementsBase : MonoBehaviour
    {
        public abstract void UnlockAchievement();
        public abstract void IncrementAchievement();
        public abstract void RequestAchievements(UnityAction<bool, string> callback);
        public abstract Achievement[] GetAchievements();
    }
}