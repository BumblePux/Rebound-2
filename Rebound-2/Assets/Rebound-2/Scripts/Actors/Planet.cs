using BumblePux.Rebound2.Actors.Interactables;
using BumblePux.Rebound2.Data.Collections;
using UnityEngine;

namespace BumblePux.Rebound2.Actors
{
    public class Planet : MonoBehaviour
    {
        private const int MIN_SPRITE_SIZE = 3;
        private const int MAX_SPRITE_SIZE = 6;

        [Header("Required References")]
        public Star StarPrefab;
        public PlanetSpriteCollection SpriteCollection;
        public SpriteRenderer Sprite;
        public Animator Animator;

        private void Awake()
        {
            Sprite.sprite = SpriteCollection.GetRandomSprite();
            int randomSpriteSize = GetRandomSize();
            Sprite.transform.localScale = new Vector3(randomSpriteSize, randomSpriteSize, 0f);

            PlayAppearAnimation();

            Instantiate(StarPrefab, transform.position, Quaternion.identity, transform);
        }

        private int GetRandomSize()
        {
            return Random.Range(MIN_SPRITE_SIZE, MAX_SPRITE_SIZE + 1);
        }

        private void PlayAppearAnimation()
        {
            Animator.Play("Appear", -1, 0f);
        }

        private void OnDestroy()
        {
            SpriteCollection.ClearUsedSprites();
        }
    }
}