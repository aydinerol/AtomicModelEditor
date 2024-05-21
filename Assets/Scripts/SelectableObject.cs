using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableObject : MonoBehaviour, IPointerDownHandler
{
    public DragAndDropManager dragAndDropManager;

    public void OnPointerDown(PointerEventData eventData)
    {
        dragAndDropManager.StartDragging(gameObject);
    }
}