using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BumblePux.Rebound.Achievements
{
    public class LocalAchievements : AchievementsBase
    {
        private AchievementDatabase localDatabase;

        //--------------------------------------------------------------------------------
        private void Awake()
        {
            localDatabase = Resources.Load<AchievementDatabase>("LocalAchievementsDatabase");
        }

        //--------------------------------------------------------------------------------
        public override void UnlockAchievement()
        {
            throw new System.NotImplementedException();
        }

        //--------------------------------------------------------------------------------
        public override void IncrementAchievement()
        {
            throw new System.NotImplementedException();
        }

        //--------------------------------------------------------------------------------
        public override void RequestAchievements(UnityAction<bool, string> callback)
        {
            if (localDatabase.Achievements.Length > 0)
            {
                callback?.Invoke(true, "At least 1 achievement present.");
            }
            else
            {
                callback?.Invoke(false, "No achievements in database.");
            }
        }

        //--------------------------------------------------------------------------------
        public override Achievement[] GetAchievements()
        {
            return localDatabase.Achievements.ToArray();
        }
    }
}