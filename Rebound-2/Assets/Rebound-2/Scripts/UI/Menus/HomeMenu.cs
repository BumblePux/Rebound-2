using BumblePux.Rebound.Managers;
using BumblePux.Rebound.Utils;

namespace BumblePux.Rebound.UI.Menus
{
    public class HomeMenu : MenuBase
    {
        private LevelLoader levelLoader;

        //---------------------------------------------------------------------------
        private void Start()
        {
            levelLoader = GameUtils.GetLevelLoader();
        }

        //--------------------------------------------------
        public void PlayGame()
        {
            levelLoader.LoadLevel(GameConstants.TIMED_GAME_MODE);            
        }
    }
}