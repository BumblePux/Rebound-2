using Cinemachine;
using UnityEngine;

namespace BumblePux.Rebound2.Camera
{
    public class CinemachineShake : MonoBehaviour
    {
        public static CinemachineShake Instance { get; private set; }

        private CinemachineVirtualCamera virtualCam;
        private CinemachineBasicMultiChannelPerlin perlin;

        private float shakeDuration;
        private float shakeTimeTotal;
        private float shakeStartIntensity;

        private void Awake()
        {
            Instance = this;
            virtualCam = GetComponent<CinemachineVirtualCamera>();
            perlin = virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        public void Shake(float intensity, float duration)
        {
            perlin.m_AmplitudeGain = intensity;

            shakeStartIntensity = intensity;
            shakeDuration = duration;
            shakeTimeTotal = duration;
        }

        private void Update()
        {
            if (shakeDuration > 0f)
            {
                shakeDuration -= Time.deltaTime;
                perlin.m_AmplitudeGain = Mathf.Lerp(shakeStartIntensity, 0f, 1 - (shakeDuration / shakeTimeTotal));
            }
        }
    }
}