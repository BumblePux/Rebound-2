using System.Collections.Generic;
using BumblePux.Rebound.Managers;
using Cinemachine;
using UnityEngine;

namespace BumblePux.Rebound.Camera
{
    public class TargetGroupUpdater : MonoBehaviour
    {
        public float TargetRadius = 1f;

        private CinemachineTargetGroup targetGroup;


        private void Awake()
        {
            targetGroup = GetComponent<CinemachineTargetGroup>();
        }

        private void OnEnable()
        {
            if (PlanetsManager.Instance)
                PlanetsManager.Instance.OnPlanetsUpdated += UpdateTargets;
        }

        private void OnDisable()
        {
            if (PlanetsManager.Instance)
                PlanetsManager.Instance.OnPlanetsUpdated -= UpdateTargets;
        }

        public void UpdateTargets(List<Transform> targets)
        {
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