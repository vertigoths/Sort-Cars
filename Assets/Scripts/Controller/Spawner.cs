using System;
using System.Collections.Generic;
using System.Linq;
using CarRelated;
using Data;
using UnityEngine;

namespace Controller
{
    public class Spawner : MonoBehaviour
    {
        private Vector3[] _spawnPoints;
        private Quaternion[] _spawnRotations;
        private Vector3[] _waitPoints;

        private int _lastIndex;
        private CarType[] _carsToBeSpawned;

        private Dictionary<int, bool> _lineStats;

        private void Awake()
        {
            _lineStats = new Dictionary<int, bool>();

            SetPoints();
        }

        private void Start()
        {
            InitialSpawn();
        }

        private void InitialSpawn()
        {
            var currentLevel = PlayerPrefs.GetInt("CurrentLevel");
            
            _carsToBeSpawned = LevelData.CarsToBeSpawned[currentLevel];
            var spawnPointsPerLevel = LevelData.SpawnPointsPerLevel[currentLevel];

            for (var i = 0; i < spawnPointsPerLevel; i++)
            {
                SpawnCar(i, i);
            }

            _lastIndex = _carsToBeSpawned.Length - spawnPointsPerLevel;
        }

        private void SetPoints()
        {
            var parent = GameObject.FindGameObjectWithTag("SpawnPoints");

            var childCount = parent.transform.childCount;
            _spawnPoints = new Vector3[childCount];
            _waitPoints = new Vector3[childCount];
            _spawnRotations = new Quaternion[childCount];

            for (var i = 0; i < childCount; i++)
            {
                var child = parent.transform.GetChild(i);
                var childTransform = child.transform;
                _spawnPoints[i] = childTransform.localPosition;
                _waitPoints[i] = childTransform.GetChild(0).position;
                _spawnRotations[i] = childTransform.rotation;
            }
        }

        public void OnCarLeaveWaitLine(int pointIndex)
        {
            if (_lastIndex < _carsToBeSpawned.Length)
            {
                SpawnCar(_lastIndex++, pointIndex);
            }
        }

        private void SpawnCar(int carIndex, int pointIndex)
        {
            var currentCarType = _carsToBeSpawned[carIndex];
            var carPrefab = LevelData.CarPrefabs[currentCarType];
                
            var carObject = Instantiate(carPrefab, _spawnPoints[pointIndex], _spawnRotations[pointIndex]);
                
            var carMovement = carObject.GetComponent<CarMovement>();
            var car = carObject.GetComponent<Car>();
            car.Spawner = this;
            car.PointIndex = pointIndex;

            var path = new Vector3[]
            {
                _waitPoints[pointIndex]
            };
                
            carMovement.MoveCarTo(path);
        }

        private bool CheckIfAllLinesCorrect()
        {
            foreach (var status in _lineStats.Values)
            {
                Debug.Log(status + " | ");
            }
            
            return _lineStats.Values.All(status => status);
        }

        public void ChangeLineStatus(int index, bool status)
        {
            _lineStats[index] = status;

            if (CheckIfAllLinesCorrect())
            {
                FindObjectOfType<LevelController>().OnWin();
            }
        }
    }
}
