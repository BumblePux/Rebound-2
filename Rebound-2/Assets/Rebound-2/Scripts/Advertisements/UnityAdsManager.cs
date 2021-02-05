using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace BumblePux.Rebound2.Advertisements
{
    public class UnityAdsManager : MonoBehaviour, IUnityAdsListener
    {
        public static UnityAdsManager Instance;

        [Header("Settings")]
        public bool TestMode = true;

#if UNITY_IOS
        private readonly string gameID = "3953336";
#elif UNITY_ANDROID
        private readonly string gameID = "3953337";
#endif

        private const string rewardedVideoAd = "rewardedVideo";

        private event Action<bool> OnAdFinished;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            Advertisement.AddListener(this);
            Advertisement.Initialize(gameID, TestMode);
        }

        public void PlayRewardedVideoAd(Action<bool> adFinished)
        {
            OnAdFinished = adFinished;

            if (Advertisement.IsReady(rewardedVideoAd))
            {
                Advertisement.Show(rewardedVideoAd);
            }
        }

        public void OnUnityAdsReady(string placementId)
        {
            if (placementId == rewardedVideoAd)
            {
                // Do something.
            }
        }

        public void OnUnityAdsDidError(string message)
        {
            // Log Error?
        }

        public void OnUnityAdsDidStart(string placementId)
        {
            // Optional actions to take when the end-user triggers an advertisement.
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            if (placementId == rewardedVideoAd)
            {
                switch (showResult)
                {
                    case ShowResult.Failed:
                        OnAdFinished?.Invoke(false);
                        OnAdFinished = null;
                        break;
                    case ShowResult.Skipped:
                        OnAdFinished?.Invoke(false);
                        OnAdFinished = null;
                        break;
                    case ShowResult.Finished:
                        OnAdFinished?.Invoke(true);
                        OnAdFinished = null;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}