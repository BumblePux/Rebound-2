using BumblePux.Rebound2.Audio;
using UnityEngine;

namespace BumblePux.Rebound2.Data.Collections
{
    [CreateAssetMenu(fileName = "New Sound Collection", menuName = "Data/Collections/Sounds")]
    public class SoundCollection : ScriptableObject
    {
        public Sound[] Sounds;
    }
}