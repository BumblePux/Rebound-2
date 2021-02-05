using BumblePux.Rebound2.GooglePlay;
using BumblePux.Rebound2.Saving;
using BumblePux.Rebound2.Utils;
using UnityEngine;

namespace BumblePux.Rebound2.Managers
{
    public class Boot : MonoBehaviour
    {
        [Header("Debug")]
        public bool DeleteSaveOnStartup;

        private void Start()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            PCBoot();
#elif UNITY_ANDROID && !UNITY_EDITOR
            GooglePlayBoot();
#endif
        }

        private void GooglePlayBoot()
        {
            GPGServices.Initialize();
            GPGServices.SignIn((success) =>
            {
                InitializeSaveData();
                SceneLoader.LoadScene(GameConstants.Scenes.SCENE_MAIN_MENU);
            });            
        }

        private void PCBoot()
        {
            InitializeSaveData();
            SceneLoader.LoadScene(GameConstants.Scenes.SCENE_MAIN_MENU);
        }

        private void InitializeSaveData()
        {
            SaveManager.Instance.Load();
        }
    }
}