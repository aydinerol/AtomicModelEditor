using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CoordinateReader : MonoBehaviour
{
    // Properties
    public string filePath = "Assets/coordinates.txt";

    public List<Vector3> ReadCoordinates()
    {
        List<Vector3> coordinates = new List<Vector3>();

        try
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 3)
                {
                    float x = float.Parse(parts[0], CultureInfo.InvariantCulture);
                    float y = float.Parse(parts[1], CultureInfo.InvariantCulture);
                    float z = float.Parse(parts[2], CultureInfo.InvariantCulture);
                    Debug.Log("Coordinates" + (x, y, z));
                    coordinates.Add(new Vector3(x, y, z));
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error reading coordinates: " + e.Message);
        }
        return coordinates;
    }
}
