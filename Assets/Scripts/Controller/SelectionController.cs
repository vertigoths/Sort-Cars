using CarRelated;
using Interaction;
using UnityEngine;

namespace Controller
{
    public class SelectionController : MonoBehaviour
    {
        private GameObject _lastSelectedObject;
        private bool _canSelect;

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
                _lastSelectedObject = null;
            }
        }

        public bool CanSelect()
        {
            return _canSelect;
        }

        public void SetCanSelect(bool canSelect)
        {
            _canSelect = canSelect;
        }
    }
}
