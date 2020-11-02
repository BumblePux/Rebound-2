using BumblePux.Rebound.GameModes;
using BumblePux.Rebound.Utils;
using TMPro;
using UnityEngine;

namespace BumblePux.Rebound.UI
{
    public class TimerDisplay : MonoBehaviour
    {
        [Header("References")]
        public TMP_Text TimerText;

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
            EventBus.OnTimerChanged += UpdateTimerText;

            animator.Play("Appear", -1, 0f);
        }

        //--------------------------------------------------
        private void OnDisable()
        {
            EventBus.OnGameModeStateChanged -= HandleGameModeChanged;
            EventBus.OnTimerChanged -= UpdateTimerText;
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
            TimerText.gameObject.SetActive(true);            
        }

        //--------------------------------------------------
        private void Hide()
        {
            TimerText.gameObject.SetActive(false);
        }

        //--------------------------------------------------
        private void UpdateTimerText(float time)
        {
            TimerText.SetText(time.ToString("F2"));
        }
    }
}