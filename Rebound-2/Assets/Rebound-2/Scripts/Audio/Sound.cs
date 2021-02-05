using UnityEngine;

namespace BumblePux.Rebound2.Audio
{
    [CreateAssetMenu(fileName = "New Sound", menuName = "Data/Audio/Sound")]
    public class Sound : ScriptableObject
    {
        public string Name;
        public AudioClip Clip;
        public bool IsMusicClip;
        [Range(0f, 1f)]
        public float Volume = 1f;
        [Range(-3f, 3f)]
        public float Pitch = 1f;

        [HideInInspector]
        public AudioSource Source;
    }
}