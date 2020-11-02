using BumblePux.Rebound.Actors;
using BumblePux.Rebound.Data;
using BumblePux.Rebound.GameModes;
using BumblePux.Rebound.Leaderboards;
using BumblePux.Rebound.Managers;
using UnityEngine;

namespace BumblePux.Rebound.Utils
{
    public class GameUtils : MonoBehaviour
    {
        //---------------------------------------------------------------------------
        public static ReboundGameMode GetGameMode()
        {
            return FindObjectOfType<ReboundGameMode>();
        }

        //--------------------------------------------------
        public static AudioManager GetAudioManager()
        {
            return AudioManager.Instance;
        }

        //--------------------------------------------------
        public static LevelLoader GetLevelLoader()
        {
            return LevelLoader.Instance;
        }

        //--------------------------------------------------
        public static GameData GetGameData()
        {
            return GameData.Instance;
        }

        //--------------------------------------------------
        public static PlanetsManager GetPlanetsManager()
        {
            return FindObjectOfType<PlanetsManager>();
        }

        //--------------------------------------------------
        public static MenuManager GetMenuManager()
        {
            return FindObjectOfType<MenuManager>();
        }

        //--------------------------------------------------
        public static Player GetPlayer()
        {
            return FindObjectOfType<Player>();
        }

        //--------------------------------------------------
        public static LeaderboardBase GetLeaderboard()
        {
            return LeaderboardBase.Instance;
        }

        //--------------------------------------------------
        public static bool GetRandomBool()
        {
            int random = Random.Range(0, 2);
            if (random == 1)
                return true;
            else
                return false;
        }
    }
}