using DG.Tweening;
using Interaction;
using UnityEngine;

namespace CarRelated
{
    public class CarMovement : MonoBehaviour
    {
        public void MoveCarTo(Vector3[] path)
        {
            var selectable = GetComponent<Selectable>();
            selectable.DisallowSelection();

            transform.DOPath(path, 3f)
                .SetEase(Ease.Linear)
                .SetLookAt(0.08f)
                .OnComplete(selectable.AllowSelection)
                .SetSpeedBased(true);
        }
    }
}
