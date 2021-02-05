using BumblePux.Rebound2.Gameplay.Managers;
using Cinemachine;
using UnityEngine;

namespace BumblePux.Rebound2.Camera
{
    public class CameraTargetGroupUpdater : MonoBehaviour
    {
        [Header("Settings")]
        public float TargetRadius = 1f;

        private CinemachineTargetGroup targetGroup;
        private PlanetsManager planetsManager;

        private void Start()
        {
            targetGroup = GetComponent<CinemachineTargetGroup>();
            planetsManager = FindObjectOfType<PlanetsManager>();
        }

        private void Update()
        {
            if (targetGroup.m_Targets.Length != planetsManager.NumPlanets)
            {
                UpdateTargets(planetsManager.GetActivePlanets());
            }
        }

        private void UpdateTargets(Transform[] targets)
        {
            if (targets.Length == 0) return;

            // Clear current targets
            foreach (var target in targetGroup.m_Targets)
            {
                targetGroup.RemoveMember(target.target);
            }

            // Update with new targets
            foreach (var target in targets)
            {
                targetGroup.AddMember(target, 1f, TargetRadius);
            }
        }
    }
}