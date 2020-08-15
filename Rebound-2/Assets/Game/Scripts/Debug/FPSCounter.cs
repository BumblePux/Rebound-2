using System;
using TMPro;
using UnityEngine;

namespace BumblePux.Rebound.Debugging
{
    public class FPSCounter : MonoBehaviour
    {
        public int frameRange = 60;

        public TMP_Text FPSAverageText;
        public TMP_Text FPSLowestText;
        public TMP_Text FPSHighestText;


        private float[] fpsBuffer;
        private int fpsBufferIndex;

        private float averageFPS;
        private float lowestFPS;
        private float highestFPS;

        private void Update()
        {
            if (fpsBuffer == null || fpsBuffer.Length != frameRange)
            {
                InitializeBuffer();
            }

            UpdateBuffer();
            CalculateFPS();
            UpdateLabels();
        }

        private void InitializeBuffer()
        {
            if (frameRange <= 0)
                frameRange = 1;

            fpsBuffer = new float[frameRange];
            fpsBufferIndex = 0;
        }

        private void UpdateBuffer()
        {
            fpsBuffer[fpsBufferIndex++] = 1f / Time.unscaledDeltaTime;

            if (fpsBufferIndex >= frameRange)
                fpsBufferIndex = 0;
        }

        private void CalculateFPS()
        {
            var sum = 0f;
            highestFPS = 0f;
            lowestFPS = float.MaxValue;

            for (int i = 0; i < frameRange; i++)
            {
                float fps = fpsBuffer[i];
                sum += fps;

                if (fps > highestFPS)
                    highestFPS = fps;

                if (fps < lowestFPS)
                    lowestFPS = fps;
            }

            averageFPS = sum / frameRange;
        }

        private void UpdateLabels()
        {
            FPSAverageText.SetText(averageFPS.ToString("F2"));
            FPSHighestText.SetText(highestFPS.ToString("F2"));
            FPSLowestText.SetText(lowestFPS.ToString("F2"));
        }
    }
}