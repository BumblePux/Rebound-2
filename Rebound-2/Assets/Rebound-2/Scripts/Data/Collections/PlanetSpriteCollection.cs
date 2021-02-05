using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BumblePux.Rebound2.Data.Collections
{
    [CreateAssetMenu(fileName = "New Planet Sprite Collection", menuName = "Data/Collections/Planet Sprites")]
    public class PlanetSpriteCollection : ScriptableObject
    {
        [Header("Planet Sprites")]
        public Sprite[] Sprites;

        private List<int> usedSpriteIndexes = new List<int>();
        private int maxIterations = 50;

        public Sprite GetRandomSprite()
        {
            int currentIteration = 0;
            int randomIndex = 0;

            do
            {
                randomIndex = Random.Range(0, Sprites.Length);
                currentIteration++;
            }
            while (IsInUse(randomIndex) || currentIteration < maxIterations);

            UpdateUsedSpritesIndex(randomIndex);
            return Sprites[randomIndex];
        }

        private bool IsInUse(int index)
        {
            bool isInUse = false;

            if (usedSpriteIndexes.Count == 0)
            {
                isInUse = false;
            }
            else
            {
                foreach(var spriteIndex in usedSpriteIndexes)
                {
                    isInUse = spriteIndex == index ? true : false;
                }
            }

            return isInUse;
        }

        private void UpdateUsedSpritesIndex(int randomIndex)
        {
            if (!usedSpriteIndexes.Contains(randomIndex))
                usedSpriteIndexes.Add(randomIndex);
        }

        public void ClearUsedSprites()
        {
            usedSpriteIndexes.Clear();
        }
    }
}