using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropManager : MonoBehaviour
{
    public GameObject spherePrefab;
    //public CubeGenerator cubeGenerator;
    private List<Vector3> snappableSlots { get; set; }
    public GameObject selectedObject;
    private Vector3 offset;
    private bool isDragging = false;

    void Update()
    {
        if (isDragging && selectedObject != null)
        {
            
            // Get the mouse position in world coordinates
            Vector3 mousePosition = GetMouseWorldPosition();
            Debug.Log("Dragging on coordinates: " + mousePosition);
            // Update the position of the created object based on the mouse position and the offset
            //selectedObject.transform.position = mousePosition + offset;
            selectedObject.transform.position = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, 0);
            //selectedObject.transform.position = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, 0);
        }

        //if (Input.GetMouseButtonUp(0) && isDragging)
        //{
        //    isDragging = false;
        //    SnapToSlot();
        //}
    }

    public void StartDragging(GameObject obj)
    {
        selectedObject = obj;
        //offset = selectedObject.transform.position - GetMouseWorldPosition();
        isDragging = true;
    }

    //private Vector3 GetMouseWorldPosition()
    //{
    //    Vector3 screenPosition = Input.mousePosition;
    //    screenPosition.z = 0;
    //    //screenPosition.z = Camera.main.WorldToScreenPoint(selectedObject.transform.position).z;
    //    return Camera.main.ScreenToWorldPoint(screenPosition);
    //}

    private Vector3 GetMouseWorldPosition()
    {
        // Get the mouse position in screen coordinates
        Vector3 mousePos = Input.mousePosition;
        // Convert the screen coordinates to world coordinates
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void SnapToSlot()
    {
        if (snappableSlots == null)
        {
            Debug.LogWarning("Snappable slots not set.");
            return;
        }

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
        Debug.Log("Snappable slots set: " + snappableSlots.Count);
    }

    public void InstantiateObject()
    {
        //if (snappableSlots != null && snappableSlots.Count > 0)
        //{
        
        //Vector3 position = snappableSlots[0]; // Get the first available slot

        //  snappableSlots.RemoveAt(0); // Remove the slot from available slots
        //Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 spawnPos = GetMouseWorldPosition();
        Debug.Log("Initiating object... at " + spawnPos);
        selectedObject = Instantiate(spherePrefab, spawnPos, Quaternion.identity);
        selectedObject.AddComponent<SelectableObject>().dragAndDropManager = this;
        offset = selectedObject.transform.position - spawnPos;
        
        // Make the new object a child of the cube
        GameObject mainCube = GameObject.Find("MainCube");
        selectedObject.transform.parent = mainCube.transform;

            StartDragging(selectedObject);
        //}
        //else
        //{
         //   Debug.LogWarning("No available slots to place the new object.");
        //}
    }
}