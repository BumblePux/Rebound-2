using BumblePux.Tools.Singleton;
using UnityEngine;

namespace BumblePux.Rebound.Managers
{
    public class PlanetsManager : Singleton<PlanetsManager>
    {
        private const int MIN_PLANETS = 1;
        private const int MAX_PLANETS = 3;

        [Header("Settings")]
        [Range(MIN_PLANETS, MAX_PLANETS)]
        public int PlanetCount = MIN_PLANETS;
        public bool AlwaysSpawnThirdPlanetOnRight;

        [Header("Required Prefabs")]
        public GameObject PlanetPrefab;
        public GameObject PointPrefab;

        private Vector2[] planetPositions = new Vector2[MAX_PLANETS];
        private Vector2[] pointPositions = new Vector2[MAX_PLANETS];

        private Transform[] planets = new Transform[MAX_PLANETS];
        private Transform[] points = new Transform[MAX_PLANETS];

        private float distanceBetweenPlanets;
        private int lastPlanetCount;
        private bool isInitialized;



        public void Initialize()
        {
            distanceBetweenPlanets = GameManager.Instance.CurrentGameMode.PlayerOffset;

            CalculatePlanetPositions();
            CalculatePointPositions();
            InstantiatePlanetsAndPoints();
            LinkPointsToPlanets();
            EnablePlanets();

            isInitialized = true;
        }

        private void CalculatePlanetPositions()
        {
            planetPositions[0] = Vector3.zero;
            planetPositions[1] = Vector3.up * (distanceBetweenPlanets * 2f);

            float planet3X;

            if (ShouldPlanetSpawnOnRight() || AlwaysSpawnThirdPlanetOnRight)
                planet3X = Mathf.Sqrt(Mathf.Pow(distanceBetweenPlanets * 2f, 2) - Mathf.Pow(distanceBetweenPlanets, 2));
            else
                planet3X = -Mathf.Sqrt(Mathf.Pow(distanceBetweenPlanets * 2f, 2) - Mathf.Pow(distanceBetweenPlanets, 2));

            planetPositions[2] = new Vector3(planet3X, distanceBetweenPlanets);
        }

        private bool ShouldPlanetSpawnOnRight()
        {
            int spawnOnRight = Random.Range(0, 2);
            if (spawnOnRight == 1)
                return true;
            else
                return false;
        }

        private void CalculatePointPositions()
        {
            pointPositions[0] = (planetPositions[0] + planetPositions[1]) * 0.5f;
            pointPositions[1] = (planetPositions[0] + planetPositions[2]) * 0.5f;
            pointPositions[2] = (planetPositions[1] + planetPositions[2]) * 0.5f;
        }

        private void InstantiatePlanetsAndPoints()
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

            if (PlanetCount != lastPlanetCount)
            {
                if (PlanetCount > lastPlanetCount)
                {
                    EnablePlanets();
                }
                else if (PlanetCount < lastPlanetCount)
                {
                    DisablePlanets();
                }

                UpdateTranistionPoints();

                lastPlanetCount = PlanetCount;
            }
        }

        private void EnablePlanets()
        {
            for (int i = lastPlanetCount; i < PlanetCount; i++)
            {
                planets[i].gameObject.SetActive(true);
            }
        }

        private void DisablePlanets()
        {
            for (int i = lastPlanetCount; i > PlanetCount; i--)
            {
                planets[i - 1].gameObject.SetActive(false);
            }
        }

        private void UpdateTranistionPoints()
        {
            for (int i = 0; i < MAX_PLANETS; i++)
            {
                points[i].gameObject.SetActive(false);
            }

            switch (PlanetCount)
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