using System;
using CarRelated;
using Controller;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Interaction
{
    public class Selectable : MonoBehaviour, IPointerClickHandler
    {
        private bool _canSelect;
        private bool _isSelected;
        private GameObject _indicator;
        private SelectionController _selectionController;

        private void Awake()
        {
            _selectionController = FindObjectOfType<SelectionController>();
            
            for (var i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);

                if (child.name.Equals("ClickStatus"))
                {
                    _indicator = child.gameObject;
                    _indicator.SetActive(false);
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            var hit = eventData.pointerCurrentRaycast.gameObject;
            var line = hit.GetComponent<Line>();
            var car = hit.GetComponent<Car>();

            if (line)
            {
                if (_selectionController.IsCarSelected())
                {
                    Debug.Log("Send car to line!");
                }
            }
            else if(car)
            {
                if (_canSelect)
                {
                    SetParameters(true);
                    _selectionController.SetLastSelectedObject(hit);
                }
                else if (_isSelected)
                {
                    SetParameters(false);
                    _selectionController.ResetLastSelectedObject();
                }
            }
            else
            {
                _selectionController.ResetLastSelectedObject();
            }
        }

        public void AllowSelection()
        {
            _canSelect = true;
        }
        
        public void DisallowSelection()
        {
            _canSelect = false;
        }

        public void SetParameters(bool status)
        {
            _isSelected = status;
            _canSelect = !status;
            _indicator.SetActive(status);
        }
    }
}
