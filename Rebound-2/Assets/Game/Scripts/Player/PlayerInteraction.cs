using BumblePux.Rebound.GameModes;
using BumblePux.Rebound.Interactables;
using BumblePux.Rebound.Managers;
using UnityEngine;

namespace BumblePux.Rebound.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [Header("Debug Settings")]
        public bool AutoInteract;

        private IInteractable interactable;
        private GameModeBase gameMode;


        private void Awake()
        {
            gameMode = GameManager.Instance.CurrentGameMode;
        }

        public void TryInteract()
        {
            if (interactable != null)
            {
                interactable.Interact();
            }
            else
            {
                gameMode.TargetMissed();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            interactable = other.GetComponentInParent<IInteractable>();

            if (interactable != null && AutoInteract)
            {
                TryInteract();
            }
        }

        private void OnTriggerExit2D(Collider2D pther)
        {
            interactable = null;
        }
    }
}