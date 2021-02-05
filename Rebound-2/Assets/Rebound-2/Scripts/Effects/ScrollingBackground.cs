using UnityEngine;

namespace BumblePux.Rebound2.Effects
{
    public class ScrollingBackground : MonoBehaviour
    {
        [Header("Required References")]
        public SpriteRenderer StarsImage;

        [Header("Settings")]
        public float ScrollSpeed = 10f;

        private Material material;
        private float currentScroll;

        private void Awake()
        {
            material = StarsImage.material;
        }

        private void Update()
        {
            currentScroll += ScrollSpeed * Time.unscaledDeltaTime;
            material.mainTextureOffset = new Vector2(currentScroll, 0f);
        }
    }
}