using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropManager : MonoBehaviour
{
    private List<Vector3> snappableSlots { get; set; }
    public GameObject selectedObject;
    private Vector3 offset;
    private bool isDragging = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (isDragging && selectedObject != null)
        {
            Vector3 mousePosition = GetMouseWorldPosition();
            selectedObject.transform.position = mousePosition + offset;
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            SnapToSlot();
        }
    }
    
    public void InstantiateObject(GameObject obj)
    {
        Debug.Log("InstantiateObject called");

        if (snappableSlots == null)
        {
            Debug.LogError("Snappable slots is null.");
            return;
        }

        if (snappableSlots.Count > 0)
        {
            Vector3 position = snappableSlots[0]; // Get the first available slot
            snappableSlots.RemoveAt(0); // Remove the slot from available slots

            GameObject newObject = Instantiate(obj, position, Quaternion.identity);
            newObject.AddComponent<SelectableObject>().dragAndDropManager = this;

            StartDragging(newObject);
        }
        else
        {
            Debug.LogWarning("No available slots to place the new object.");
        }
    }

    public void StartDragging(GameObject obj)
    {
        selectedObject = obj;
        offset = selectedObject.transform.position - GetMouseWorldPosition();
        isDragging = true;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 screenPosition = Input.mousePosition;
        screenPosition.z = Camera.main.WorldToScreenPoint(selectedObject.transform.position).z;
        return Camera.main.ScreenToWorldPoint(screenPosition);
    }

    private void SnapToSlot()
    {
        Vector3 closestSlot = Vector3.zero;
        float minDistance = float.MaxValue;

        foreach (Vector3 slot in snappableSlots)
        {
            float distance = Vector3.Distance(selectedObject.transform.position, slot);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestSlot = slot;
            }
        }

        if (minDistance < 0.5f) // Snap distance threshold
        {
            selectedObject.transform.position = closestSlot;
        }
    }
    
    public void SetSnappableSlots(List<Vector3> slots) 
    {
        snappableSlots = slots;
    }
}