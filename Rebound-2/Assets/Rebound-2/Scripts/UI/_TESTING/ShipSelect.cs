using BumblePux.Rebound.Data;
using BumblePux.Rebound.Utils;
using UnityEngine;

namespace BumblePux.Rebound
{
    public class ShipSelect : MonoBehaviour
    {
        [Header("References")]
        public GameObject Content;

        private GameData gameData;

        //--------------------------------------------------------------------------------
        private void Start()
        {
            gameData = GameUtils.GetGameData();
            PopulateShips();
        }

        //--------------------------------------------------
        private void PopulateShips()
        {
            foreach (var ship in gameData.Ships)
            {

            }
        }
    }
}