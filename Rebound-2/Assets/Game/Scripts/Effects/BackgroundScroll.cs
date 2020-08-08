using System;
using UnityEngine;
using UnityEngine.UI;

namespace BumblePux.Rebound.Effects
{
    public class BackgroundScroll : MonoBehaviour
    {
        [SerializeField] Image ScrollImage = default;
        [SerializeField] Vector2 OffsetPerSecond = default;
        [SerializeField] int MaxOffsetToReset = 1;

        private void Update()
        {
            var offset = ScrollImage.material.GetTextureOffset("_MainTex");

            offset += OffsetPerSecond * Time.deltaTime;
            ScrollImage.material.SetTextureOffset("_MainTex", new Vector2(offset.x, offset.y));

            var tilingX = Mathf.Abs(offset.x);
            var tilingY = Mathf.Abs(offset.y);


            if (tilingX >= MaxOffsetToReset || tilingY >= MaxOffsetToReset)
            {
                ScrollImage.material.SetTextureOffset("_MainTex", Vector2.zero);
            }
        }

        private void OnDisable()
        {
            ScrollImage.material.SetTextureOffset("_MainTex", Vector2.zero);
        }
    }
}