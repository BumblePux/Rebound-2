using UnityEngine;

namespace BumblePux.Rebound.UI
{
    public abstract class HUD : Actor
    {
        public abstract void Show();
        public abstract void Hide();
    }
}