using BumblePux.Rebound2.Audio;
using BumblePux.Rebound2.GooglePlay;
using BumblePux.Rebound2.Managers;
using BumblePux.Rebound2.Utils;
using UnityEngine;

namespace BumblePux.Rebound2.UI.Menus
{
    public class MainMenu : MenuBase
    {
        [Header("Menus")]
        public MenuBase ShopMenu;
        public MenuBase OptionsMenu;

        private void Start()
        {
            AudioManager.Instance.Play("MainMenuTheme");
        }

        public void LoadTimedGameMode()
        {
            SceneLoader.LoadScene(GameConstants.Scenes.SCENE_GAMEMODE_TIMED);
        }

        public void ShowLeaderboards()
        {
            GPGServices.ShowLeaderboardsUI();
        }

        public void ShowAchievements()
        {
            GPGServices.ShowAchievementsUI();
        }

        public void ShowShop()
        {
            Hide();
            ShopMenu.Show();
        }

        public void ShowOptions()
        {
            Hide();
            OptionsMenu.Show();
        }
    }
}