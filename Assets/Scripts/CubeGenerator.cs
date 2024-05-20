using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CubeGenerator : MonoBehaviour
{
    // Properties
    public Vector3 cubeDimensions = new Vector3(10, 10, 10);
    public GameObject spherePrefab;
    public string coordinatesFilePath = "Assets/coordinates.txt";
    public float squareSize = 0.5f;

    private List<Vector3> availableSlots;
    private List<Vector3> filledSlots = new List<Vector3>();

    void Start()
    {
        ReadAndInstantiate();
    }

    void ReadAndInstantiate()
    {
        CoordinateReader coordinateReader = new CoordinateReader();
        coordinateReader.filePath = coordinatesFilePath;

        availableSlots = coordinateReader.ReadCoordinates();
        filledSlots.Clear();

        GenerateCubeWithSpheres();
    }

    void GenerateCubeWithSpheres()
    {
        // Create the cube (could be a wireframe or a transparent cube for visualization)
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = cubeDimensions;
        cube.transform.position = cubeDimensions / 2; // Center the cube

        // Disable the cube's renderer if you don't want it visible
        cube.GetComponent<Renderer>().enabled = false;

        // Instantiate spheres at the specified coordinates
        foreach (Vector3 coord in availableSlots)
        {
            if (IsInsideCube(coord))
            {
                Instantiate(spherePrefab, coord, Quaternion.identity);
                filledSlots.Add(coord);
            }
        }
    }

    bool IsInsideCube(Vector3 coord)
    {
        return coord.x >= 0 && coord.x <= cubeDimensions.x &&
               coord.y >= 0 && coord.y <= cubeDimensions.y &&
               coord.z >= 0 && coord.z <= cubeDimensions.z;
    }

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
}
