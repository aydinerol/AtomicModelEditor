using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

//[ExecuteInEditMode]
public class CubeGenerator : MonoBehaviour
{
    // Properties
    public Vector3 cubeDimensions = new Vector3(22, 42, 22);
    public GameObject spherePrefab;
    public string coordinatesFilePath = "Assets/coordinates.txt";
    public float squareSize = 0.5f;

    private List<Vector3> availableSlots;
    private DragAndDropManager dragAndDropManager;

    void Start()
    {        
        dragAndDropManager = FindObjectOfType<DragAndDropManager>();
        if (dragAndDropManager == null)
        {
            Debug.LogError("DragAndDropManager not found in the scene.");
            return;
        }
        GenerateCube();
        ReadCoordinates();
        InstantiateSpheres();
    }
    
    void GenerateCube()
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = cubeDimensions;
        cube.transform.position = cubeDimensions / 2; // Center the cube
        cube.GetComponent<Renderer>().enabled = false; // Hide the cube

        cube.name = "MainCube";
        cube.transform.parent = this.transform;
    }

    void ReadCoordinates()
    {
        availableSlots = new List<Vector3>();

        try
        {
            string[] lines = File.ReadAllLines(coordinatesFilePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 3)
                {
                    float x = float.Parse(parts[0], CultureInfo.InvariantCulture);
                    float y = float.Parse(parts[1], CultureInfo.InvariantCulture);
                    float z = float.Parse(parts[2], CultureInfo.InvariantCulture);
                    availableSlots.Add(new Vector3(x, y, z));
                }
            }

            Debug.Log($"Read {availableSlots.Count} coordinates from file.");

        }
        catch (System.Exception e)
        {
            Debug.LogError("Error reading coordinates: " + e.Message);
        }
        // set snappable slots in DragAndDropManager after reading data
        dragAndDropManager.SetSnappableSlots(availableSlots);
    }
    
    void InstantiateSpheres()
    {
        //GameObject mainCube = GameObject.Find("MainCube");
        
        foreach (Vector3 coord in availableSlots)
        {
            if (IsInsideCube(coord))
            {
                GameObject sphere = Instantiate(spherePrefab, coord, Quaternion.identity);
                sphere.AddComponent<SelectableObject>().dragAndDropManager = dragAndDropManager;
                //sphere.transform.parent = mainCube.transform;
            }
        }
    }
    
    bool IsInsideCube(Vector3 coord)
    {
        return coord.x >= 0 && coord.x <= cubeDimensions.x &&
               coord.y >= 0 && coord.y <= cubeDimensions.y &&
               coord.z >= 0 && coord.z <= cubeDimensions.z;
    }

    public void SetDragAndDropManager(DragAndDropManager manager)
    {
        dragAndDropManager = manager;
    }

    // Debug Drawing
    void OnDrawGizmos()
    {
        if (availableSlots == null) return;

        Gizmos.color = Color.green;

        foreach (Vector3 position in availableSlots)
        {
            DrawSquare(position);
        }
    }

    void DrawSquare(Vector3 position)
    {
        Vector3 offset = new Vector3(squareSize / 2, squareSize / 2, 0);

        Vector3 topLeft = position + new Vector3(-offset.x, offset.y, 0);
        Vector3 topRight = position + new Vector3(offset.x, offset.y, 0);
        Vector3 bottomRight = position + new Vector3(offset.x, -offset.y, 0);
        Vector3 bottomLeft = position + new Vector3(-offset.x, -offset.y, 0);

        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }

    public List<Vector3> GetAvailableSlots()
    {
        return availableSlots;
    }
}
