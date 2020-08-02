using BumblePux.Rebound.GameModes;
using BumblePux.Rebound.Interactables;
using UnityEngine;

namespace BumblePux.Rebound.Player
{
    public class PlayerInteraction : Actor
    {
        [Header("Debug Settings")]
        public bool AutoInteract;

        private PlayerInput input;
        private IInteractable interactable;


        private void Awake()
        {
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
                GetGameMode().TargetMissed();
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