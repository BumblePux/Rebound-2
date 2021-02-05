using BumblePux.Rebound2.Actors;
using BumblePux.Rebound2.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BumblePux.Rebound2.Gameplay.Managers
{
    public class PlanetsManager : MonoBehaviour
    {
        private const int MIN_PLANETS = 1;
        private const int MAX_PLANETS = 3;

        [Header("Required References")]
        public GameObject PlanetPrefab;
        public TransitionPoint PointPrefab;

        [Header("Settings")]
        [Range(MIN_PLANETS, MAX_PLANETS)]
        public int NumPlanets = MIN_PLANETS;

        [Header("Debug")]
        public bool AlwaysSpawnThirdPlanetOnRight;

        private Transform[] planets = new Transform[MAX_PLANETS];
        private Transform[] points = new Transform[MAX_PLANETS];

        private Vector2[] planetPositions = new Vector2[MAX_PLANETS];
        private Vector2[] pointPositions = new Vector2[MAX_PLANETS];

        [HideInInspector]
        public float DistanceBetweenPlanets;

        private int lastNumPlanets;
        private bool isInitialized;

        
        public Transform[] GetActivePlanets()
        {
            var activePlanets = new List<Transform>();

            foreach(var planet in planets)
            {
                if (planet.gameObject.activeInHierarchy)
                    activePlanets.Add(planet);
            }

            return activePlanets.ToArray();
        }

        public Transform GetRandomActivePlanet()
        {
            var activePlanets = GetActivePlanets();
            int randomIndex = Random.Range(0, activePlanets.Length);
            return activePlanets[randomIndex];
        }

        public void Initialize(float playerDistanceFromPlanet)
        {
            DistanceBetweenPlanets = playerDistanceFromPlanet * 2;

            CalculatePlanetPositions();
            CalculatePointPositions();
            SpawnPlanetsAndPoints();
            LinkPointsToPlanets();
            EnablePlanets();

            isInitialized = true;
        }

        private void CalculatePlanetPositions()
        {
            planetPositions[0] = Vector2.zero;
            planetPositions[1] = Vector2.up * DistanceBetweenPlanets;

            Vector2 thirdPlanetPosition;
            thirdPlanetPosition.y = DistanceBetweenPlanets * 0.5f;

            if (GameUtils.CoinFlip() || AlwaysSpawnThirdPlanetOnRight)
                thirdPlanetPosition.x = Mathf.Sqrt(Mathf.Pow(DistanceBetweenPlanets, 2) - Mathf.Pow(DistanceBetweenPlanets * 0.5f, 2));
            else
                thirdPlanetPosition.x = -Mathf.Sqrt(Mathf.Pow(DistanceBetweenPlanets, 2) - Mathf.Pow(DistanceBetweenPlanets * 0.5f, 2));

            planetPositions[2] = thirdPlanetPosition;
        }

        private void CalculatePointPositions()
        {
            pointPositions[0] = (planetPositions[0] + planetPositions[1]) * 0.5f;
            pointPositions[1] = (planetPositions[0] + planetPositions[2]) * 0.5f;
            pointPositions[2] = (planetPositions[1] + planetPositions[2]) * 0.5f;
        }

        private void SpawnPlanetsAndPoints()
        {
            for (int i = 0; i < MAX_PLANETS; i++)
            {
                var planet = Instantiate(PlanetPrefab, planetPositions[i], Quaternion.identity, transform).transform;
                planets[i] = planet;
                planet.gameObject.SetActive(false);
                planet.name = $"Planet: {i + 1}";

                var point = Instantiate(PointPrefab, pointPositions[i], Quaternion.identity, transform).transform;
                points[i] = point;
                point.gameObject.SetActive(false);
                point.name = $"Point: {i + 1}";
            }
        }

        private void LinkPointsToPlanets()
        {
            points[0].GetComponent<TransitionPoint>().SetLinkedPlanets(planets[0].transform, planets[1].transform);
            points[1].GetComponent<TransitionPoint>().SetLinkedPlanets(planets[0].transform, planets[2].transform);
            points[2].GetComponent<TransitionPoint>().SetLinkedPlanets(planets[1].transform, planets[2].transform);
        }

        private void Update()
        {
            if (!isInitialized) return;

            if (NumPlanets != lastNumPlanets)
            {
                if (NumPlanets > lastNumPlanets)
                    EnablePlanets();
                else
                    DisablePlanets();

                UpdateEnabledPoints();

                lastNumPlanets = NumPlanets;
            }
        }

        private void EnablePlanets()
        {
            for (int i = lastNumPlanets; i < NumPlanets; i++)
            {
                planets[i].gameObject.SetActive(true);
            }
        }

        private void DisablePlanets()
        {
            for (int i = lastNumPlanets; i > NumPlanets; i--)
            {
                planets[i - 1].gameObject.SetActive(false);
            }
        }

        private void UpdateEnabledPoints()
        {
            for (int i = 0; i < MAX_PLANETS; i++)
            {
                points[i].gameObject.SetActive(false);
            }

            switch (NumPlanets)
            {
                case 2:
                    points[0].gameObject.SetActive(true);
                    break;
                case 3:
                    points[0].gameObject.SetActive(true);
                    points[1].gameObject.SetActive(true);
                    points[2].gameObject.SetActive(true);
                    break;
            }
        }
    }
}