using BumblePux.Rebound.GameModes;
using BumblePux.Rebound.Utils;
using TMPro;
using UnityEngine;

namespace BumblePux.Rebound.UI
{
    public class ScoreDisplay : MonoBehaviour
    {
        [Header("References")]
        public TMP_Text ScoreText;

        private Animator animator;

        //---------------------------------------------------------------------------
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        //--------------------------------------------------
        private void OnEnable()
        {
            EventBus.OnGameModeStateChanged += HandleGameModeChanged;
            EventBus.OnScoreChanged += UpdateScoreText;

            animator.Play("Appear", -1, 0f);
        }

        //--------------------------------------------------
        private void OnDisable()
        {
            EventBus.OnGameModeStateChanged -= HandleGameModeChanged;
            EventBus.OnScoreChanged -= UpdateScoreText;
        }

        //--------------------------------------------------
        private void HandleGameModeChanged(GameModeBase.State newState)
        {
            if (newState == GameModeBase.State.GameOver)
                Hide();
            else
                Show();
        }

        //--------------------------------------------------
        private void Show()
        {
            ScoreText.gameObject.SetActive(true);            
        }

        //--------------------------------------------------
        private void Hide()
        {
            ScoreText.gameObject.SetActive(false);
        }

        //--------------------------------------------------
        private void UpdateScoreText(int score)
        {
            ScoreText.SetText(score.ToString());
        }
    }
}