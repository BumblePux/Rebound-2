using UnityEngine;
using UnityEngine.SceneManagement;

namespace BumblePux.Rebound.UI
{
    public class MainMenu : MonoBehaviour
    {

        private void Start()
        {
            GameplayStatics.GetGameManager().CurrentGameMode = null;
        }

        public void PlayGame()
        {
            SceneManager.LoadScene("TimedGameMode");
        }

        public void LoadLeaderboards()
        {
            Debug.Log("Would load leaderboards UI.");
        }

        public void LoadAchievements()
        {
            Debug.Log("Would load achievements UI.");
        }

        public void LoadShipSelectMenu()
        {
            SceneManager.LoadScene("ShipSelectMenu");
        }
    }
}