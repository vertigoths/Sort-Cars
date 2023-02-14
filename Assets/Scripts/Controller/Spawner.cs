using System;
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

        private void Awake()
        {
            SetPoints();
        }

        private void Start()
        {
            InitialSpawn();
        }

        private void InitialSpawn()
        {
            var carsToBeSpawned = LevelData.CarsToBeSpawned[0];
            var spawnPointsPerLevel = LevelData.SpawnPointsPerLevel[0];

            for (var i = 0; i < spawnPointsPerLevel; i++)
            {
                var currentCarType = carsToBeSpawned[i];
                var carPrefab = LevelData.CarPrefabs[currentCarType];
                
                var carObject = Instantiate(carPrefab, _spawnPoints[i], _spawnRotations[i]);
                
                var car = carObject.GetComponent<CarMovement>();
                
                var path = new Vector3[]
                {
                    _waitPoints[i]
                };
                
                car.MoveCarTo(path);
            }
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
    }
}
