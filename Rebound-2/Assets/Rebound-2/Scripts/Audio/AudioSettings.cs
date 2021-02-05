using UnityEngine;
using UnityEngine.UI;

namespace BumblePux.Rebound2.Audio
{
    public class AudioSettings : MonoBehaviour
    {
        [Header("Required References")]
        public Button MusicButton;
        public Button SoundButton;

        [Header("Color Settings")]
        public Color OnColor;
        public Color OffColor;

        private void Start()
        {
            UpdateButtonColor(MusicButton, AudioManager.Instance.IsMusicEnabled);
            UpdateButtonColor(SoundButton, AudioManager.Instance.IsSoundEnabled);
        }

        public void ToggleMusic()
        {
            AudioManager.Instance.ToggleMusic();
            UpdateButtonColor(MusicButton, AudioManager.Instance.IsMusicEnabled);
        }

        public void ToggleSound()
        {
            AudioManager.Instance.ToggleSound();
            UpdateButtonColor(SoundButton, AudioManager.Instance.IsSoundEnabled);
        }

        private void UpdateButtonColor(Button button, bool enabled)
        {
            var colorBlock = button.colors;
            colorBlock.normalColor = enabled ? OnColor : OffColor;
            colorBlock.pressedColor = enabled ? OnColor : OffColor;
            colorBlock.selectedColor = enabled ? OnColor : OffColor;
            button.colors = colorBlock;
        }
    }
}