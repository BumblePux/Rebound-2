using BumblePux.Rebound2.Data;
using BumblePux.Rebound2.Data.Collections;
using BumblePux.Rebound2.Saving;
using System.Collections.Generic;
using UnityEngine;

namespace BumblePux.Rebound2.Managers
{
    public class ShipAssetManager : MonoBehaviour, ISaveable
    {
        public static ShipAssetManager Instance { get; private set; }

        public ShipCollection ShipCollection;
        public ShipData EquippedShip;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public void CaptureState(SaveData data)
        {
            SaveEquippedShip(data);
            SaveUnlockedShips(data);
        }

        public void ResetUnlocks()
        {
            SetDefaults();

            foreach (var ship in ShipCollection.Ships)
            {
                if (ship.Name == "BumblePux") ship.IsUnlocked = true;
                else ship.IsUnlocked = false;
            }
        }

        private void SaveEquippedShip(SaveData data)
        {
            if (EquippedShip == null)
            {
                SetDefaults();
            }

            data.PlayerData.EquippedShip = EquippedShip.Name;
        }

        private void SetDefaults()
        {
            for (int i = 0; i < ShipCollection.Ships.Count; i++)
            {
                if (ShipCollection.Ships[i].Name == "BumblePux")
                {
                    EquippedShip = ShipCollection.Ships[i];
                    break;
                }
            }
        }

        private void SaveUnlockedShips(SaveData data)
        {
            var unlockedShips = new List<string>();

            foreach (var ship in ShipCollection.Ships)
            {
                if (ship.IsUnlocked) unlockedShips.Add(ship.Name);
            }

            data.PlayerData.UnlockedShips = unlockedShips;
        }

        public void RestoreState(SaveData data)
        {
            LoadEquippedShip(data);
            LoadUnlockedShips(data);
        }

        private void LoadEquippedShip(SaveData data)
        {
            for (int i = 0; i < ShipCollection.Ships.Count; i++)
            {
                if (ShipCollection.Ships[i].Name == data.PlayerData.EquippedShip)
                {
                    EquippedShip = ShipCollection.Ships[i];
                    break;
                }
            }
        }

        private void LoadUnlockedShips(SaveData data)
        {
            int unlockedShipCount = data.PlayerData.UnlockedShips.Count;
            if (unlockedShipCount == 0) return;

            for (int i = 0; i < unlockedShipCount; i++)
            {
                for (int j = 0; j < ShipCollection.Ships.Count; j++)
                {
                    if (data.PlayerData.UnlockedShips[i] == ShipCollection.Ships[j].Name)
                    {
                        ShipCollection.Ships[j].IsUnlocked = true;
                        break;
                    }
                }
            }
        }
    }
}