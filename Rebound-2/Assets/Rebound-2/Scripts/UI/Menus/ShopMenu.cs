using BumblePux.Rebound2.Achievements;
using BumblePux.Rebound2.Actors.Player;
using BumblePux.Rebound2.Data;
using BumblePux.Rebound2.Data.Collections;
using BumblePux.Rebound2.Managers;
using BumblePux.Rebound2.Saving;
using BumblePux.Rebound2.UI.ShipSelection;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BumblePux.Rebound2.UI.Menus
{
    public class ShopMenu : MenuBase
    {
        [Header("Required Data")]
        public ShipCollection ShipCollection;
        public ShipCard ShipCardPrefab;

        [Header("Required External References")]
        public ShipVisuals ShipVisuals;

        [Header("Required Internal References")]
        public TMP_Text CurrentCoinsText;
        public Transform ScrollViewContent;

        public List<ShipCard> shipCards = new List<ShipCard>();

        private void OnEnable()
        {
            UpdateCurrentCoinsText();
            LoadShopItems();
        }

        private void UpdateCurrentCoinsText()
        {
            //CurrentCoinsText?.SetText(SaveManager.Data.Coins.ToString());
        }

        private void LoadShopItems()
        {
            // Delete any gameobjects under Content
            int contentCount = ScrollViewContent.childCount;

            if (contentCount > 0)
            {
                for (int i = 0; i < contentCount; i++)
                {
                    ScrollViewContent.transform.GetChild(i).GetComponent<ShipCard>().OnCardButtonClicked -= HandleCardButtonClicked;
                    Destroy(ScrollViewContent.transform.GetChild(i).gameObject);
                }
            }

            // Load all available ship cards
            int shipCount = ShipCollection.Ships.Count;
            if (shipCount == 0) return;

            for (int i = 0; i < shipCount; i++)
            {
                var card = Instantiate(ShipCardPrefab, ScrollViewContent);
                card.UpdateCard(ShipCollection.Ships[i]);
                card.OnCardButtonClicked += HandleCardButtonClicked;

                shipCards.Add(card);
            }
        }

        private void HandleCardButtonClicked(ShipData ship)
        {
            if (ship == ShipAssetManager.Instance.EquippedShip) return;

            if (ship.IsUnlocked)
            {
                ShipAssetManager.Instance.EquippedShip = ShipCollection.Ships[ShipCollection.Ships.IndexOf(ship)];
            }
            else
            {
                //if (ship.Cost <= SaveManager.Data.Coins)
                //{
                //    //SaveManager.Data.Coins -= ship.Cost;
                //    ShipCollection.Ships[ShipCollection.Ships.IndexOf(ship)].IsUnlocked = true;
                    
                //    AchievementsManager.Instance.TryUnlockBoughtFirstShip();
                //}
            }

            for (int i = 0; i < shipCards.Count; i++)
            {
                shipCards[i].UpdateCard(ShipCollection.Ships[i]);
            }

            UpdateCurrentCoinsText();

            ShipVisuals.LoadShipModel();

            //SaveManager.Instance.Save();
        }
    }
}