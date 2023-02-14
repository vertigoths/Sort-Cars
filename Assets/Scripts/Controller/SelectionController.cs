using CarRelated;
using Interaction;
using UnityEngine;

namespace Controller
{
    public class SelectionController : MonoBehaviour
    {
        private GameObject _lastSelectedObject;

        public bool IsCarSelected()
        {
            return _lastSelectedObject && _lastSelectedObject.GetComponent<Car>();
        }

        public GameObject GetLastSelectedObject()
        {
            return _lastSelectedObject;
        }

        public void SetLastSelectedObject(GameObject selectedObject)
        {
            ResetLastSelectedObject();
            
            _lastSelectedObject = selectedObject;
        }

        public void ResetLastSelectedObject()
        {
            if (_lastSelectedObject)
            {
                var selectable = _lastSelectedObject.GetComponent<Selectable>();
                selectable.SetParameters(false);
            }
        }
    }
}
