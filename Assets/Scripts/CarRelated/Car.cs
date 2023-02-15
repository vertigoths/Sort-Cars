using UnityEngine;
using UnityEngine.UI;

namespace CarRelated
{
    public class Car : MonoBehaviour
    {
        [SerializeField] private CarType type;
        private Line.Line _currentLine;
        private int _lineIndex;

        public void SetLine(Line.Line line)
        {
            _currentLine = line;
        }

        public bool InLine()
        {
            return _currentLine;
        }

        public Vector3 GetLineEntryPoint()
        {
            return _currentLine.EntryPoint;
        }

        public Line.Line GetLine()
        {
            return _currentLine;
        }
    }
}
