using UnityEngine;

namespace BumblePux.Rebound.UI
{
    public abstract class HUD : Actor
    {
        public virtual void Show() { }
        public virtual void Hide() { }
    }
}