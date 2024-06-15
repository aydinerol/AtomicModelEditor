using System.Collections.Generic;
using UnityEngine;

public class AtomicBondGenerator : MonoBehaviour
{
    public GameObject sphereParent; // Parent GameObject containing all sphere objects
    public float bondDistanceThreshold = 2f; // Distance threshold to form a bond
    public Material bondMaterial; // Material for the bond

    private List<GameObject> spheres;

    public AtomicBondGenerator( GameObject parent)
    {
        sphereParent = parent;
    }

    void Start()
    {
        if (sphereParent == null)
        {
            sphereParent = GameObject.Find("MainCube");
        }
        
        // Find all spheres under the sphereParent
        spheres = new List<GameObject>();
        foreach (Transform child in sphereParent.transform)
        {
            spheres.Add(child.gameObject);
        }

        // Generate atomic bonds
        GenerateAtomicBonds();
    }

    void GenerateAtomicBonds()
    {
        for (int i = 0; i < spheres.Count; i++)
        {
            for (int j = i + 1; j < spheres.Count; j++)
            {
                float distance = Vector3.Distance(spheres[i].transform.position, spheres[j].transform.position);
                if (distance < bondDistanceThreshold)
                {
                    DrawBond(spheres[i].transform.position, spheres[j].transform.position);
                }
            }
        }
    }

    void DrawBond(Vector3 startPos, Vector3 endPos)
    {
        GameObject bond = new GameObject("Bond");
        bond.transform.SetParent(transform);

        LineRenderer lineRenderer = bond.AddComponent<LineRenderer>();
        lineRenderer.material = bondMaterial;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }
}
