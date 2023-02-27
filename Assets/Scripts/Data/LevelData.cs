using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public static class LevelData
    {
        public static readonly Dictionary<CarType, GameObject> CarPrefabs = new Dictionary<CarType, GameObject>()
        {
            {
                CarType.Coupe, Resources.Load<GameObject>("Prefabs/Coupe")
            },
            {
                CarType.Police, Resources.Load<GameObject>("Prefabs/Police")
            },
            {
                CarType.Sedan, Resources.Load<GameObject>("Prefabs/Sedan")
            },
            {
                CarType.Taxi, Resources.Load<GameObject>("Prefabs/Taxi")
            },
        };

        public static readonly Dictionary<int, CarType[]> CarsToBeSpawned = new Dictionary<int, CarType[]>()
        {
            {
                0, new[]
                {
                    CarType.Taxi,
                    CarType.Police,
                }
            },
            {
                1, new[]
                {
                    CarType.Coupe,
                    CarType.Police,
                    CarType.Coupe,
                    CarType.Police,
                    CarType.Coupe,
                    CarType.Coupe,
                }
            }
        };

        public static readonly Dictionary<int, int> SpawnPointsPerLevel = new Dictionary<int, int>()
        {
            {
                0, 2
            },
            {
                1, 2
            }
        };
        
        public static readonly Dictionary<int, int> LinePerLevel = new Dictionary<int, int>()
        {
            {
                0, 2
            },
            {
                1, 3
            }
        };
    }
}