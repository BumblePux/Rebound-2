using BumblePux.Rebound2.Managers;
using UnityEngine;

namespace BumblePux.Rebound2.Actors.Player
{
    public class ShipVisuals : MonoBehaviour
    {
        [Header("References")]
        public Transform SpriteHolder;

        private TrailRenderer trail;

        private void Awake()
        {
            trail = SpriteHolder.GetComponentInChildren<TrailRenderer>();
        }

        private void Start()
        {
            LoadShipModel();
        }

        public void Flip()
        {
            var localScale = SpriteHolder.localScale;
            Vector3 newScale = new Vector3(localScale.x, localScale.y * -1f, localScale.z);
            SpriteHolder.localScale = newScale;
        }

        public void ResetToDefault()
        {
            var localScale = SpriteHolder.localScale;
            Vector3 newScale = new Vector3(Mathf.Abs(localScale.x), Mathf.Abs(localScale.y), Mathf.Abs(localScale.z));
            SpriteHolder.localScale = newScale;
        }

        public void EnableTrail(bool enable)
        {
            if (trail != null)
                trail.enabled = enable;
        }

        public void LoadShipModel()
        {
            int childCount = SpriteHolder.childCount;
            if (childCount > 0)
            {
                for (int i = 0; i < childCount; i++)
                {
                    Destroy(SpriteHolder.transform.GetChild(i).gameObject);
                }
            }

            Instantiate(ShipAssetManager.Instance.EquippedShip.ShipModelPrefab, SpriteHolder);
        }
    }
}