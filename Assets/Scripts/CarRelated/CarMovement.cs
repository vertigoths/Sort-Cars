using DG.Tweening;
using Interaction;
using UnityEngine;

namespace CarRelated
{
    public class CarMovement : MonoBehaviour
    {
        public void DOMoveCarTo(Vector3[] path)
        {
            var selectable = GetComponent<Selectable>();
            selectable.DODisallowSelection();

            transform.DOPath(path, 3.5f)
                .SetEase(Ease.Linear)
                .SetLookAt(0.01f)
                .OnComplete(selectable.DOAllowSelection)
                .SetSpeedBased(true);
        }
    }
}
