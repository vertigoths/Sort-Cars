using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Interaction
{
    public class Selectable : MonoBehaviour, IPointerClickHandler
    {
        private bool _canSelect;
        private bool _isSelected;

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("SELECTED!");
            
            if (_canSelect)
            {
                _isSelected = true;
                
                Debug.Log("SELECTED!");
            }
        }

        public void DOAllowSelection()
        {
            _canSelect = true;
        }
        
        public void DODisallowSelection()
        {
            _canSelect = false;
        }
    }
}
