using BumblePux.Rebound2.Actors.Interactables;
using System;
using UnityEngine;

namespace BumblePux.Rebound2.Actors.Player
{
    public class ShipInteraction : MonoBehaviour
    {
        [Header("Debug Settings")]
        public bool AutoInteract;

        private IInteractable interactable;

        public void TryInteract(Action<bool> interactSuccess = null)
        {
            if (interactable != null)
            {
                interactable.Interact();
                interactSuccess?.Invoke(true);
            }
            else
            {
                interactSuccess?.Invoke(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Interactable"))
            {
                interactable = other.GetComponentInParent<IInteractable>();
                if (interactable != null && AutoInteract)
                {
                    TryInteract();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Interactable"))
            {
                interactable = null;
            }
        }
    }
}