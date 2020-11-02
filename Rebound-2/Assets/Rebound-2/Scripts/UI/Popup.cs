using BumblePux.Rebound.Actors;
using BumblePux.Rebound.Utils;
using TMPro;
using UnityEngine;

namespace BumblePux.Rebound.UI
{
    public class Popup : MonoBehaviour
    {
        [Header("References")]
        public TMP_Text PopupText;

        [Header("Settings")]
        public float InitialSpeed = 8f;
        public float Deceleration = 8f;
        public float AppearDuration = 2f;
        public float DisappearDuration = 1f;

        private Color textColor;
        private Vector3 movement;
        private float duration;

        //---------------------------------------------------------------------------
        public void SetText(string text)
        {
            PopupText.SetText(text);
            movement = GetDirection() * InitialSpeed;
        }

        //--------------------------------------------------
        public void SetStartPoint(Vector3 startPosition)
        {
            transform.position = startPosition;
        }

        //--------------------------------------------------
        private void OnEnable()
        {
            textColor = PopupText.color;
            textColor.a = 1f;
            duration = AppearDuration;
        }

        //--------------------------------------------------
        private void Update()
        {
            transform.position += movement * Time.deltaTime;
            movement -= movement * Deceleration * Time.deltaTime;

            duration -= Time.deltaTime;
            if (duration < 0f)
            {
                textColor.a -= DisappearDuration * Time.deltaTime;
                PopupText.color = textColor;

                if (textColor.a < 0f)
                    Destroy(gameObject);
            }
        }

        //--------------------------------------------------
        private Vector3 GetDirection()
        {
            Player player = GameUtils.GetPlayer();
            Transform planet = player.OrbitTarget;
            return (planet.position - player.transform.position) * -1;
        }
    }
}