using BumblePux.Rebound.Utils;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BumblePux.Rebound.Managers
{
    public class LevelLoader : Singleton<LevelLoader>
    {
        [Header("Settings")]
        public Animator Transition;
        public float TransitionTime = 1f;

		//--------------------------------------------------------------------------------
        public void LoadLevel(string sceneName)
        {
            StartCoroutine(Load(sceneName));
        }

        //--------------------------------------------------
        public void LoadLevel(int sceneIndex)
        {
            LoadLevel(SceneManager.GetSceneByBuildIndex(sceneIndex).name);
        }

        //--------------------------------------------------
        private IEnumerator Load(string sceneName)
        {
            if (Transition)
            {
                Transition.SetTrigger("Start");
                yield return new WaitForSeconds(TransitionTime);
            }
            else
            {
                yield return null;
            }

            SceneManager.LoadScene(sceneName);
        }

        //--------------------------------------------------
        private void OnEnable()
        {
            SceneManager.sceneLoaded += HandleSceneLoaded;
        }

        //--------------------------------------------------
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= HandleSceneLoaded;
        }

        //--------------------------------------------------
        private void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Transition.Play("CrossFade_End", -1, 0f);
        }
    }
}