using System;
using CarRelated;
using Controller;
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
            var line = hit.GetComponent<Line.Line>();
            var car = hit.GetComponent<Car>();

            if (!_selectionController.CanSelect())
            {
                return;
            }

            if (line)
            {
                if (_selectionController.IsCarSelected() && !line.IsFull())
                {
                    _selectionController.SetCanSelect(false);
                    car = _selectionController.GetLastSelectedObject().GetComponent<Car>();

                    if (car.GetLine() != line)
                    {
                        line.SendCarTo(car);
                        car.SetLine(line);
                    }
                    else
                    {
                        _selectionController.SetCanSelect(true);
                    }
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
            _selectionController.SetCanSelect(true);
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
