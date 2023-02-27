using System.Collections.Generic;
using CarRelated;
using UnityEngine;

namespace Line
{
    public class Line : MonoBehaviour
    {
        [SerializeField] private int maxCapacity;
        [SerializeField] private Vector3 offset;
        private LinkedList<Car> _cars;
        private Vector3 _startPoint;
        public Vector3 EntryPoint { get; private set; }

        [SerializeField] private int lineIndex;

        private void Awake()
        {
            _cars = new LinkedList<Car>();
            var childCount = transform.parent.childCount;

            for (var i = 0; i < childCount; i++)
            {
                var child = transform.parent.GetChild(i);

                switch (child.name)
                {
                    case "StartPoint":
                        _startPoint = child.transform.localPosition;
                        break;
                    case "EntryPoint":
                        EntryPoint = child.transform.localPosition;
                        break;
                }
            }
        }

        public void SendCarTo(Car car)
        {
            var carMovement = car.GetComponent<CarMovement>();

            if (car.InLine())
            {            
                var currentLine = car.GetLine();
                currentLine.RemoveCarFrom(car);
                
                var path = new Vector3[]
                {
                    car.GetLineEntryPoint(),
                    EntryPoint,
                    GetParkingPosition()
                };
            
                carMovement.MoveCarTo(path);
            }
            else
            {
                var path = new Vector3[]
                {
                    EntryPoint,
                    GetParkingPosition()
                };
            
                carMovement.MoveCarTo(path);
            }
            
            _cars.AddLast(car);

            if (IsFull() && AreAllSameCars())
            {
                car.Spawner.ChangeLineStatus(lineIndex, true);
            }
            else
            {
                car.Spawner.ChangeLineStatus(lineIndex, false);
            }
        }
        
        public void SetCarTo(Car car)
        {
            _cars.AddLast(car);
        }

        public bool IsFull()
        {
            return maxCapacity == _cars.Count;
        }

        private void RemoveCarFrom(Car car)
        {
            _cars.Remove(car);
        }

        private Vector3 GetParkingPosition()
        {
            return _startPoint + (_cars.Count * offset);
        }

        public Car GetLastCar()
        {
            return _cars.Last.Value;
        }

        private bool AreAllSameCars()
        {
            var firstCar = _cars.First.Value;
            
            foreach (var car in _cars)
            {
                if (firstCar.GetCarType() != car.GetCarType())
                {
                    return false;
                }
            }
            
            return true;
        }
    }
}
