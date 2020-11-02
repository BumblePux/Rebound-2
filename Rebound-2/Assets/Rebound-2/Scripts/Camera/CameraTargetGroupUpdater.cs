using BumblePux.Rebound.Managers;
using BumblePux.Rebound.Utils;
using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

namespace BumblePux.Rebound
{
    public class CameraTargetGroupUpdater : MonoBehaviour
    {
        [Header("Settings")]
        public float TargetRadius = 1f;

        private CinemachineTargetGroup targetGroup;
        private PlanetsManager planetsManager;

        //---------------------------------------------------------------------------
        private void Start()
        {
            targetGroup = GetComponent<CinemachineTargetGroup>();
            planetsManager = GameUtils.GetPlanetsManager();
        }

        //--------------------------------------------------
        private void Update()
        {
            if (targetGroup.m_Targets.Length != planetsManager.NumPlanets)
            {
                UpdateTargets(planetsManager.GetActivePlanets());
            }
        }

        //--------------------------------------------------
        private void UpdateTargets(List<Transform> targets)
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