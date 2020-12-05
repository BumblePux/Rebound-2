using BumblePux.Rebound.Achievements;
using BumblePux.Rebound.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BumblePux.Rebound.Managers
{
    public class AchievementsManager : Singleton<AchievementsManager>
    {
        public AchievementsBase platform;

        //--------------------------------------------------------------------------------
        private void Awake()
        {
#if UNITY_EDITOR || UNITY_STANDLONE
            platform = gameObject.AddComponent<LocalAchievements>();
#endif
        }

        //--------------------------------------------------------------------------------
        public void UnlockAchievement()
        {
            platform.UnlockAchievement();
        }

        //--------------------------------------------------------------------------------
        public void IncrementAchievement()
        {
            platform.IncrementAchievement();
        }

        //--------------------------------------------------------------------------------
        public void RequestAchievements(UnityAction<bool, string> callback)
        {
            platform.RequestAchievements(callback);
        }

        //--------------------------------------------------------------------------------
        public Achievement[] GetAchievements()
        {
            return platform.GetAchievements();
        }

    }
}