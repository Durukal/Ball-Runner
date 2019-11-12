using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.LevelGenerationSctips
{
    public class LevelGenerator : MonoBehaviour
    {
        public GameObject PlayerBall;
        public float CurrentTimeVisualizer;
        public float DeathTimeVisualizer;

        [Header("Road Variables")]
        [Tooltip("Preferred difference between the furthest point of the road and the current player position.")]
        public int GenerationLength;
        [Tooltip("Chance that obstacles will be enabled when an obstacle is enabled.")]
        public int ObstacleCount;
        public int ObstacleCountUpperDeviation;
        public int ObstacleCountLowerDeviation;
        public GameObject RoadPrefab;

        [Header("Time Bonuses")]
        public float RoadPieceTimeBonus;
        public float CoinTimeBonus;

        public List<GameObject> GeneratedObstacles;

        private float _furthestZPoint;
        private float _currentZPoint;
        private int _generationCount;
        private float _roadPrefabZSize;
        
        // Use this for initialization
        [UsedImplicitly]
        private void Awake()
        {
            BallTimer.SetTimeBonuses(RoadPieceTimeBonus,CoinTimeBonus);

            _roadPrefabZSize = 7.5f;
            _generationCount = 1;
            _currentZPoint = PlayerBall.transform.position.z;
            _furthestZPoint = _roadPrefabZSize;

            GenerateRoad();
        }

        // Update is called once per frame
        [UsedImplicitly]
        private void Update()
        {
            DeathTimeVisualizer = BallTimer.DeathTime;
            CurrentTimeVisualizer = BallTimer.CurrentTime;
            // Update time and check for end game
            BallTimer.CurrentTime += Time.deltaTime;
            if (BallTimer.CurrentTime >= BallTimer.DeathTime)
            {
                Debug.LogError("GAME OVER");
                Application.Quit();
            }
            
            // Update player position
            _currentZPoint = PlayerBall.transform.position.z;

            // Check if a new road piece should be added
            if (_currentZPoint > _furthestZPoint - GenerationLength)
            {
                GenerateRoad();
                CleanRedundantRoadPieces();
            }
        }

        private Vector3 CalculatePositionVector()
        {
            Vector3 position = new Vector3(0,0, _roadPrefabZSize * _generationCount);

            return position;
        }

        private void GenerateRoad()
        {
            // Generate a new road piece
            _generationCount++;
            Vector3 positionVector = CalculatePositionVector();
            GameObject generatedObstacle = Instantiate(RoadPrefab, positionVector, Quaternion.identity);
           
            // Update furthest point
            _furthestZPoint += _roadPrefabZSize;

            // Add new piece to hierarchy and list
            generatedObstacle.transform.SetParent(transform);
            GeneratedObstacles.Add(generatedObstacle);

            // Set its obstacles
            ToggleObstacles(generatedObstacle);
        }

        private void ToggleObstacles(GameObject roadPiece)
        {
            RoadPiece piece = roadPiece.GetComponent<RoadPiece>();
            int finalObstacleCount = ObstacleCount + Random.Range(-ObstacleCountLowerDeviation, ObstacleCountUpperDeviation);

            for (int i = 0; i < finalObstacleCount; i++)
            {
                int index = Random.Range(0, piece.Obstacles.Count);
                piece.Obstacles[index].SetActive(true);
                piece.Obstacles[index].transform.rotation = Quaternion.AngleAxis(Random.Range(0, 180), Vector3.up);
            }
        }

        private void CleanRedundantRoadPieces()
        {
            if (!(_currentZPoint > GeneratedObstacles[0].transform.position.z + _roadPrefabZSize)) return;

            Destroy(GeneratedObstacles[0]);
            GeneratedObstacles.Remove(GeneratedObstacles[0]);

            // Add some time to remaining time.
            BallTimer.IncrementTimer(BallTimer.RoadPieceTimeBonus);
        }
    }
}
