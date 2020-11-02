using BumblePux.Rebound.GameModes;
using BumblePux.Rebound.Utils;
using UnityEngine;

namespace BumblePux.Rebound.UI
{
    public class Countdown : MonoBehaviour
    {
        private ReboundGameMode gameMode;

        //--------------------------------------------------------------------------------
        private void Start()
        {
            gameMode = GameUtils.GetGameMode();
        }

        //--------------------------------------------------
        public void CountdownFinished()
        {
            Debug.Log("Countdown Finished!");
            gameMode.CountdownComplete = true;
        }
    }
}