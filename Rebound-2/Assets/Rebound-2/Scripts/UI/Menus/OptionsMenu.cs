using BumblePux.Rebound2.Managers;
using BumblePux.Rebound2.Saving;
using BumblePux.Rebound2.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BumblePux.Rebound2.UI.Menus
{
    public class OptionsMenu : MenuBase
    {


        public void OpenPrivacyPolicy()
        {
            Application.OpenURL(GameConstants.Links.LINK_PRIVACY_POLICY);
        }

        public void DeleteSaveData()
        {
            //SaveManager.Delete();
            //ShipAssetManager.Instance.ResetUnlocks();
            //SaveManager.ResetData();

            //SaveManager.Save();
            //SaveManager.Load();

            SceneLoader.LoadScene(GameConstants.Scenes.SCENE_MAIN_MENU);
        }
    }
}