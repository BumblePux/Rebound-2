using BumblePux.Rebound.Interactables;
using BumblePux.Tools.Singleton;
using UnityEngine;

namespace BumblePux.Rebound.Managers
{
    public class TargetsManager : Singleton<TargetsManager>
    {
        [Header("Target Settings")]
        public Target TargetPrefab;


        public void Initialize()
        {
            var planets = PlanetsManager.Instance.GetAllPlanets();

            for (int i = 0; i < planets.Length; i++)
            {
                var target = Instantiate(TargetPrefab, Vector3.zero, Quaternion.identity, planets[i]);
                target.Initialize();
            }
        }
    }
}