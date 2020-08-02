using BumblePux.Rebound.Unlockables;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BumblePux.Rebound.UI
{
    public class ShipSelectUI : HUD
    {
        [Header("UI References")]
        public Button LeftButton;
        public Button RightButton;
        public TMP_Text BuySelectText;
        public Image PreviewImage;

        private GameInstance gameInstance;
        private UnlockDataSet shipDataset;

        private int currentIndex;
        private Unlockable currentSelection;


        private void Start()
        {
            gameInstance = GetGameInstance();
            shipDataset = gameInstance.ShipDataSet;

            gameInstance.SelectedShip = shipDataset.Unlockables[currentIndex];

            currentIndex = 0;
            PreviewImage.sprite = shipDataset.Unlockables[currentIndex].PreviewImage;

            UpdateButtonState();
        }

        public void NextShip(bool direction)
        {
            // direction:   false = left
            //              true  = right

            UpdateCurrentSelection(direction);
            UpdatePreviewImage();
            UpdateButtonState();
            UpdateSelectionVisibilty();
        }

        private void UpdateCurrentSelection(bool direction)
        {
            if (!direction)
            {
                if (currentIndex - 1 >= 0)
                {
                    currentIndex--;
                }
            }
            else
            {
                if (currentIndex + 1 < shipDataset.Unlockables.Length)
                {
                    currentIndex++;
                }
            }

            currentSelection = shipDataset.Unlockables[currentIndex];
        }

        private void UpdatePreviewImage()
        {
            PreviewImage.sprite = currentSelection.PreviewImage;
        }

        private void UpdateButtonState()
        {
            if (currentIndex == 0)
            {
                LeftButton.interactable = false;
                RightButton.interactable = true;
            }
            else if (currentIndex == shipDataset.Unlockables.Length - 1)
            {
                RightButton.interactable = false;
                LeftButton.interactable = true;
            }
            else
            {
                RightButton.interactable = true;
                LeftButton.interactable = true;
            }

            string buySelectText;
            Unlockable currentSelection = shipDataset.Unlockables[currentIndex];

            if (gameInstance.SelectedShip == currentSelection)
            {
                buySelectText = "Equipped";
            }
            else
            {
                buySelectText = currentSelection.IsUnlocked ? "Select" : "Buy";
            }

            BuySelectText.SetText(buySelectText);
        }

        private void UpdateSelectionVisibilty()
        {
            if (shipDataset.Unlockables[currentIndex].IsUnlocked)
            {
                PreviewImage.color = Color.white;
            }
            else
            {
                PreviewImage.color = Color.black;
            }
        }

        public void HandleBuySelectButtonPressed()
        {
            Unlockable currentSelection = shipDataset.Unlockables[currentIndex];

            // If already equipped
            if (gameInstance.SelectedShip == currentSelection)
            {
                return;
            }

            // If unlocked, then equip
            if (currentSelection.IsUnlocked)
            {
                gameInstance.SelectedShip = currentSelection;
            }
            else
            {
                // If not unlocked, attempt to purchase/unlock
                Debug.Log("[ShipSelectUI] Would attempt to purchase now.");
            }

            UpdateButtonState();
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}