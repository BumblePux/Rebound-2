using BumblePux.Rebound.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace BumblePux.Rebound.Data
{
    public class GameData : Singleton<GameData>
    {
        public Ship ActiveShip;
        public List<Ship> Ships = new List<Ship>();

        //--------------------------------------------------------------------------------
        private void OnEnable()
        {
            SetupActiveShip();
        }

        //--------------------------------------------------
        private void SetupActiveShip()
        {
            if (ActiveShip == null && Ships.Count > 0)
            {
                ActiveShip = Ships[0];
            }
        }
    }
}