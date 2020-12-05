using BumblePux.Rebound.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace BumblePux.Rebound.Leaderboards
{
    public class DreamloLeaderboard : LeaderboardBase
    {
        private bool isHandlingRequest = false;

        //--------------------------------------------------------------------------------
        public override void ReportScore(LeaderboardType leaderboard, string userName, int score)
        {
            string privateId = GetPrivateId(leaderboard);
            string url = GameConstants.DREAMLO_LEADERBOARD_URL + privateId + "/add/" + UnityWebRequest.EscapeURL(userName) + "/" + score;
            StartCoroutine(HandleRequest(url, null));
        }

        //--------------------------------------------------
        public override void RequestScores(LeaderboardType leaderboard, UnityAction<bool, string> callback)
        {
            if (isHandlingRequest)
            {
                callback?.Invoke(false, "Already requesting scores.");
                Debug.Log("Already requesting scores.");
                return;
            }

            isHandlingRequest = true;

            string publicId = GetPublicId(leaderboard);
            string url = GameConstants.DREAMLO_LEADERBOARD_URL + publicId + "/json";
            StartCoroutine(HandleRequest(url, (success, json) =>
            {
                if (success)
                {
                    DreamloData dreamloData = JsonUtility.FromJson<DreamloData>(json);
                    List<HighScore> scoresList = new List<HighScore>();
                    for (int i = 0; i < dreamloData.dreamlo.leaderboard.entry.Length; i++)
                    {
                        Entry entry = dreamloData.dreamlo.leaderboard.entry[i];
                        if (entry.score == 0) continue;
                        scoresList.Add(new HighScore(entry.name, entry.score));
                    }

                    highScores = scoresList.ToArray();

                    callback?.Invoke(true, "Scores downloaded.");
                }
                else
                {
                    callback?.Invoke(false, json);
                }

                isHandlingRequest = false;
            }));
        }

        //--------------------------------------------------
        public override HighScore[] GetScores()
        {
            return highScores;
        }

        //--------------------------------------------------
        private IEnumerator HandleRequest(string url, UnityAction<bool, string> callback)
        {
            yield return new WaitForSeconds(1.1f); // Wait just over a second before starting request.

            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                // Request and wait for result
                yield return webRequest.SendWebRequest();

                string[] pages = url.Split('/');
                int page = pages.Length - 1;

                if (webRequest.isNetworkError)
                {
#if UNITY_EDITOR
                    Debug.Log(pages[page] + ": Error: " + webRequest.error);
#endif
                    callback?.Invoke(false, webRequest.error);
                }
                else
                {
#if UNITY_EDITOR
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
#endif
                    callback?.Invoke(true, webRequest.downloadHandler.text);
                }
            }
        }

        //--------------------------------------------------
        private string GetPrivateId(LeaderboardType leaderboard)
        {
            string id = "";
            switch (leaderboard)
            {
                case LeaderboardType.TimedMode:
                    id = GameConstants.DREAMLO_TIMED_MODE_LEADERBOARD_PRIVATE;
                    break;
                default:
                    break;
            }

            return id;
        }

        //--------------------------------------------------
        private string GetPublicId(LeaderboardType leaderboard)
        {
            string id = "";
            switch (leaderboard)
            {
                case LeaderboardType.TimedMode:
                    id = GameConstants.DREAMLO_TIMED_MODE_LEADERBOARD_PUBLIC;
                    break;
                default:
                    break;
            }

            return id;
        }

        //--------------------------------------------------------------------------------
        // Dreamlo JSON Data Structs
        //--------------------------------------------------------------------------------
        [System.Serializable]
        public struct DreamloData
        {
            public Dreamlo dreamlo;
        }

        //--------------------------------------------------
        [System.Serializable]
        public struct Dreamlo
        {
            public Leaderboard leaderboard;
        }

        //--------------------------------------------------
        [System.Serializable]
        public struct Leaderboard
        {
            public Entry[] entry;
        }

        //--------------------------------------------------
        [System.Serializable]
        public struct Entry
        {
            public string name;
            public int score;
        }
    }
}