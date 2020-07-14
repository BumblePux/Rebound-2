using BumblePux.Rebound.GameModes;
using BumblePux.Rebound.Interactables;
using UnityEngine;

namespace BumblePux.Rebound.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [Header("Debug Settings")]
        public bool AutoInteract;

        private PlayerInput input;
        private IInteractable interactable;
        private GameModeBase gameMode;


        private void Awake()
        {
            gameMode = GameplayStatics.GetGameMode();
            input = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            if (input.Interact())
            {
                TryInteract();
            }
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