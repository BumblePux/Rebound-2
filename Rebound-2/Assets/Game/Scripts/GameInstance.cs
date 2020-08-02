using BumblePux.Rebound.GameModes;
using BumblePux.Rebound.Unlockables;
using BumblePux.Tools.Singleton;
using UnityEngine;

namespace BumblePux
{
    [DefaultExecutionOrder(-100)]
    public class GameInstance : Singleton<GameInstance>
    {
        [Header("Current References")]
        [SerializeField] private GameModeBase currentGameMode = default;
        public GameModeBase CurrentGameMode { get => currentGameMode; set => currentGameMode = value; }

        [Header("Data Sets")]
        [SerializeField] UnlockDataSet ShipDataSet = default;

        public Unlockable SelectedShip { get; set; }


        protected override void Awake()
        {
            base.Awake();

            currentGameMode = null;

            SelectedShip = ShipDataSet.Unlockables[0];
        }
    }
}