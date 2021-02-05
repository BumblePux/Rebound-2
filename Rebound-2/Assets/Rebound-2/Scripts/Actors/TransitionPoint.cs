using UnityEngine;

namespace BumblePux.Rebound2.Actors
{
    public class TransitionPoint : MonoBehaviour
    {
        [Header("Linked Planets")]
        [SerializeField] private Transform planet1 = default;
        [SerializeField] private Transform planet2 = default;

        public void SetLinkedPlanets(Transform planet1, Transform planet2)
        {
            this.planet1 = planet1;
            this.planet2 = planet2;
        }

        public Transform[] GetLinkedPlanets()
        {
            Transform[] linkedPlanets = { planet1, planet2 };
            return linkedPlanets;
        }
    }
}