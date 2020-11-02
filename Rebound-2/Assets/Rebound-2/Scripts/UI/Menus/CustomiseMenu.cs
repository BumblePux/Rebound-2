using BumblePux.Rebound.Data;
using BumblePux.Rebound.Managers;
using BumblePux.Rebound.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BumblePux.Rebound.UI.Menus
{
    public class CustomiseMenu : MenuBase
    {
        [Header("References")]
        public Button PreviousButton;
        public Button NextButton;
        public Button EquipBuyButton;
        public Image ShipPreview;
        public TMP_Text ShipNameText;

        [Header("Audio")]
        public AudioClip ArrowButtonClip;
        public AudioClip EquipBuyButtonClip;

        private AudioManager audioManager;
        private GameData gameData;
        private int currentShipIndex;

        //---------------------------------------------------------------------------
        private void Start()
        {
            audioManager = GameUtils.GetAudioManager();
            gameData = GameUtils.GetGameData();

            currentShipIndex = gameData.Ships.IndexOf(gameData.ActiveShip);
            UpdateSelection();
        }

        //--------------------------------------------------
        public void NextShip()
        {
            if (currentShipIndex < gameData.Ships.Count)
            {
                currentShipIndex++;
            }

            UpdateSelection();
        }

        //--------------------------------------------------
        public void PreviousShip()
        {
            if (currentShipIndex > 0)
            {
                currentShipIndex--;
            }

            UpdateSelection();
        }

        //--------------------------------------------------
        public void TryEquipOrBuy()
        {
            Ship ship = gameData.Ships[currentShipIndex];

            if (gameData.ActiveShip == ship)
                return;

            if (ship.IsUnlocked)
            {
                gameData.ActiveShip = ship;
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log("[CustomiseMenu] Would attempt to buy now. Not yet implemented!");
#endif
            }

            UpdateButtons();
        }

        //--------------------------------------------------
        private void UpdateSelection()
        {
            UpdatePreviewImage();
            UpdatePreviewText();
            UpdateButtons();
            UpdatePreviewVisibility();
        }

        //--------------------------------------------------
        private void UpdatePreviewImage()
        {
            Ship ship = gameData.Ships[currentShipIndex];
            ShipPreview.sprite = ship.ShipPrefab.GetComponent<SpriteRenderer>().sprite;
        }

        //--------------------------------------------------
        private void UpdatePreviewText()
        {
            Ship ship = gameData.Ships[currentShipIndex];
            ShipNameText.SetText(ship.Name);
        }

        //--------------------------------------------------
        private void UpdateButtons()
        {
            if (currentShipIndex == 0)
            {
                PreviousButton.interactable = false;
                NextButton.interactable = true;
            }
            else if (currentShipIndex == gameData.Ships.Count - 1)
            {
                PreviousButton.interactable = true;
                NextButton.interactable = false;
            }
            else
            {
                PreviousButton.interactable = true;
                NextButton.interactable = true;
            }

            UpdateEquipBuyButtonText();
        }

        //--------------------------------------------------
        private void UpdateEquipBuyButtonText()
        {
            string equipBuyButtonText;
            Ship ship = gameData.Ships[currentShipIndex];

            if (ship == gameData.ActiveShip)
            {
                equipBuyButtonText = GameConstants.SHIP_EQUIPPED;
            }
            else
            {
                equipBuyButtonText = ship.IsUnlocked ? GameConstants.SHIP_EQUIP : GameConstants.SHIP_BUY;
            }

            EquipBuyButton.GetComponentInChildren<TMP_Text>().SetText(equipBuyButtonText);
        }

        //--------------------------------------------------
        private void UpdatePreviewVisibility()
        {
            if (gameData.Ships[currentShipIndex].IsUnlocked)
                ShipPreview.color = Color.white;
            else
                ShipPreview.color = Color.black;
        }
    }
}