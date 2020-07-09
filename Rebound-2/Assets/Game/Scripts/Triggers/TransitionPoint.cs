using UnityEngine;

namespace BumblePux.Rebound.Triggers
{
    public class TransitionPoint : MonoBehaviour
    {
        private Transform[] linkedPlanets = new Transform[2];


        public void SetLinkedPlanets(Transform planet1, Transform planet2)
        {
            linkedPlanets[0] = planet1;
            linkedPlanets[1] = planet2;
        }

        public Transform[] GetLinkedPlanets()
        {
            return linkedPlanets;
        }
    }
}