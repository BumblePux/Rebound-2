using BumblePux.Rebound.Managers;
using BumblePux.Rebound.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace BumblePux.Rebound.UI.Menus
{
    public class SettingsMenu : MenuBase
    {
        [Header("References")]
        public Slider MasterVolumeSlider;

        private AudioManager audioManager;        

        //---------------------------------------------------------------------------
        private void Start()
        {
            audioManager = GameUtils.GetAudioManager();

            MasterVolumeSlider.value = audioManager.GetMasterVolume() * MasterVolumeSlider.maxValue;
        }

        //--------------------------------------------------
        public void OnMasterVolumeChanged(float volume)
        {
            volume = volume / MasterVolumeSlider.maxValue;
            audioManager.SetMasterVolume(volume);
        }
    }
}