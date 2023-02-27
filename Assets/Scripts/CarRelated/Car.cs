using System;
using Controller;
using UnityEngine;
using UnityEngine.UI;

namespace CarRelated
{
    public class Car : MonoBehaviour
    {
        public Spawner Spawner { get; set; }
        public int PointIndex { get; set; }
        
        [SerializeField] private CarType type;
        [SerializeField] private Line.Line currentLine;
        private bool _everMoved;

        private void Start()
        {
            if (currentLine)
            {
                currentLine.SetCarTo(this);
            }
        }

        public void SetLine(Line.Line line)
        {
            currentLine = line;

            if (!_everMoved)
            {
                _everMoved = true;

                if (Spawner)
                {
                    Spawner.OnCarLeaveWaitLine(PointIndex);
                }
            }
        }

        public bool InLine()
        {
            return currentLine;
        }

        public Vector3 GetLineEntryPoint()
        {
            return currentLine.EntryPoint;
        }

        public Line.Line GetLine()
        {
            return currentLine;
        }

        public CarType GetCarType()
        {
            return type;
        }
    }
}
