using BumblePux.Rebound2.Data.Collections;
using BumblePux.Rebound2.Saving;
using System;
using UnityEngine;
using UnityEngine.Audio;

namespace BumblePux.Rebound2.Audio
{
    public class AudioManager : MonoBehaviour, ISaveable
    {
        [Header("Audio Sources")]
        public AudioSource MusicSource;
        public AudioSource SfxSource;
        public AudioMixer Mixer;

        [Header("Sounds")]
        public SoundCollection Sounds;

        public static AudioManager Instance;

        private const string masterMixer = "Master";
        private const string musicMixer = "Music";
        private const string sfxMixer = "SFX";

        public bool IsMusicEnabled { get; private set; } = true;
        public bool IsSoundEnabled { get; private set; } = true;

        private void Awake()
        {
            // Initialize singleton
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                Instance = this;
            }
        }

        public void Play(string clipName)
        {
            var sound = Array.Find(Sounds.Sounds, s => s.Name == clipName);
            if (sound == null)
            {
                Debug.Log($"[AudioManager] Can't find clip named {clipName}.");
                return;
            }

            sound.Source = sound.IsMusicClip ? MusicSource : SfxSource;
            sound.Source.clip = sound.Clip;
            sound.Source.volume = sound.Volume;
            sound.Source.pitch = sound.Pitch;
            sound.Source.loop = sound.IsMusicClip;
            sound.Source.Play();
        }

        public void StopMusic()
        {
            if (MusicSource.isPlaying)
                MusicSource.Stop();
        }

        public void ToggleMusic()
        {
            IsMusicEnabled = !IsMusicEnabled;
            SetMusicVolume(IsMusicEnabled ? 1f : 0f);
        }

        public void ToggleSound()
        {
            IsSoundEnabled = !IsSoundEnabled;
            SetSfxVolume(IsSoundEnabled ? 1f : 0f);
        }

        public void SetMasterVolume(float volume)
        {
            float volumeInDb = GetVolumeInDb(volume);
            Mixer.SetFloat(masterMixer, volumeInDb);
        }

        public void SetMusicVolume(float volume)
        {
            float volumeInDb = GetVolumeInDb(volume);
            Mixer.SetFloat(musicMixer, volumeInDb);
        }

        public void SetSfxVolume(float volume)
        {
            float volumeInDb = GetVolumeInDb(volume);
            Mixer.SetFloat(sfxMixer, volumeInDb);
        }

        public void SetMusicEnabled(bool enabled)
        {
            IsMusicEnabled = enabled;
            SetMusicVolume(IsMusicEnabled ? 1f : 0f);
        }

        public void SetSoundEnabled(bool enabled)
        {
            IsSoundEnabled = enabled;
            SetSfxVolume(IsSoundEnabled ? 1f : 0f);
        }

        public float GetMasterVolume()
        {
            Mixer.GetFloat(masterMixer, out var volumeInDb);
            return Mathf.Pow(10f, volumeInDb / 20f);
        }

        private float GetVolumeInDb(float volume)
        {
            if (volume <= 0f)
                volume = 0.0001f;

            return Mathf.Log10(volume) * 20f;
        }

        #region ===== SAVE STATE =====

        public void CaptureState(SaveData data)
        {
            data.AudioSettings.MusicEnabled = IsMusicEnabled;
            data.AudioSettings.SoundEnabled = IsSoundEnabled;
        }

        public void RestoreState(SaveData data)
        {
            IsMusicEnabled = data.AudioSettings.MusicEnabled;
            IsSoundEnabled = data.AudioSettings.SoundEnabled;

            SetMusicEnabled(IsMusicEnabled);
            SetSoundEnabled(IsSoundEnabled);
        }

        #endregion
    }
}