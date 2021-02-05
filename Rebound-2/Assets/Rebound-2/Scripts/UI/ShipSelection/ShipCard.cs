using BumblePux.Rebound2.Data;
using BumblePux.Rebound2.Managers;
using BumblePux.Rebound2.Utils;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BumblePux.Rebound2.UI.ShipSelection
{
    public class ShipCard : MonoBehaviour
    {
        public event Action<ShipData> OnCardButtonClicked;

        [Header("Required References")]
        public Image ShipPreviewImage;
        public TMP_Text ButtonText;

        private ShipData shipData;

        public void UpdateCard(ShipData ship)
        {
            shipData = ship;
            ShipPreviewImage.sprite = shipData.ShipPreviewSprite;

            if (shipData.IsUnlocked)
            {
                if (ShipAssetManager.Instance.EquippedShip == shipData)
                {
                    ButtonText?.SetText(GameConstants.Text.TEXT_EQUIPPED);
                }
                else
                {
                    ButtonText?.SetText(GameConstants.Text.TEXT_EQUIP);
                }
            }
            else
            {
                ButtonText?.SetText(shipData.Cost.ToString());
            }
        }

        public void HandleButtonClicked()
        {
            OnCardButtonClicked?.Invoke(shipData);
        }
    }
}