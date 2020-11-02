using BumblePux.Rebound.Actors.Interactables;
using System.Collections.Generic;
using UnityEngine;

namespace BumblePux.Rebound.Actors
{
    public class Planet : MonoBehaviour
    {
        private static List<int> usedIndexes = new List<int>();

        [Header("References")]
        public Star StarPrefab;

        [Header("Settings")]
        [Range(0f, 1f)]
        public float MinSize = 0.2f;
        [Range(0f, 1f)]
        public float MaxSize = 0.6f;

        [Header("Sprites")]
        public List<Sprite> sprites = new List<Sprite>();

        private Animator animator;
        private SpriteRenderer spriteRenderer;

        //---------------------------------------------------------------------------
        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

            // Assign random sprite from list
            int maxIterations = 10;
            int currentIteration = 0;
            Sprite sprite = GetRandomSprite();
            while (IsSpriteInUse(sprite) || currentIteration <= maxIterations)
            {
                sprite = GetRandomSprite();
                currentIteration++;
            }
            usedIndexes.Add(sprites.IndexOf(sprite));
            spriteRenderer.sprite = sprite;

            // Assign random size for planet sprite
            float size = Random.Range(MinSize, MaxSize);
            spriteRenderer.transform.localScale = new Vector3(size, size, size);

            Instantiate(StarPrefab, transform.position, Quaternion.identity, transform);
        }

        //--------------------------------------------------
        private void OnEnable()
        {
            animator.Play("Appear", -1, 0f);
        }

        //--------------------------------------------------
        private Sprite GetRandomSprite()
        {
            int index = Random.Range(0, sprites.Count);
            return sprites[index];
        }

        //--------------------------------------------------
        private bool IsSpriteInUse(Sprite sprite)
        {
            bool isInUse = false;

            for (int i = 0; i < usedIndexes.Count; i++)
            {
                if (usedIndexes[i] == sprites.IndexOf(sprite))
                {
                    isInUse = true;
                }
            }

            return isInUse;
        }

        //--------------------------------------------------
        private void OnDestroy()
        {
            usedIndexes.Clear();
        }
    }
}