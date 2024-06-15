using UnityEngine;

public class Chain : MonoBehaviour
{
    public GameObject atomPrefab; // Assign a sphere prefab with Atom script attached in the Inspector

    //private Vector3 initialPosition;
    //private Quaternion initialRotation = Quaternion.identity;
    //private Vector3 nextPosition;
    //private Quaternion nextRotation;

    //private float bondLength = 1.2f; // Typical bond length in angstroms
    //private float bondAngle = 109.5f; // Tetrahedral bond angle in degrees
    private bool isDragging = false;
    private Vector3 offset;

    void Start()
    {
        // Create buttons for "Create GlyAla" and "Create AlaGly"
        // Configure buttons to call CreatePeptide("GlyAla") and CreatePeptide("AlaGly")
    }

    private void Update()
    {
        if (isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                transform.position = hit.point + offset;
            }

            if (Input.GetMouseButtonDown(0)) // Left mouse button to release
            {
                isDragging = false;
            }
        }
    }

    public void CreatePeptide(string sequence)
    {
        // Clear existing atoms
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Get the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 startPosition = hit.point;
            offset = transform.position - startPosition;

            if (sequence == "AlaGly")
            {
                CreateAlaGly(startPosition, Quaternion.identity);
            }
            else if (sequence == "GlyAla")
            {
                CreateGlyAla(startPosition, Quaternion.identity);
            }

            isDragging = true; // Start dragging
        }
    }

    private void CreateGlyAla(Vector3 position, Quaternion rotation)
    {
        // Example positions from provided data
        Vector3[] positions = new Vector3[]
        {
            new Vector3(8.67f, 41.061f, 2.544f), // N ALA
            new Vector3(7.655f, 41.228f, 2.677f), // HN ALA
            new Vector3(9.217f, 39.815f, 3.067f), // CA ALA
            new Vector3(10.281f, 39.782f, 2.813f), // HA ALA
            new Vector3(9.035f, 39.752f, 4.583f), // CB ALA
            new Vector3(9.429f, 38.813f, 4.987f), // HB1 ALA
            new Vector3(9.554f, 40.582f, 5.072f), // HB2 ALA
            new Vector3(7.972f, 39.826f, 4.842f), // HB3 ALA
            new Vector3(8.497f, 38.613f, 2.455f), // C ALA
            new Vector3(7.248f, 38.605f, 2.35f),  // O ALA
            new Vector3(9.271f, 37.559f, 2.12f),  // N GLY
            new Vector3(10.3f, 37.633f, 2.232f),  // HN GLY
            new Vector3(8.695f, 36.324f, 1.613f), // CA GLY
            new Vector3(8.744f, 36.307f, 0.509f), // HA1 GLY
            new Vector3(7.639f, 36.288f, 1.902f), // HA2 GLY
            new Vector3(9.446f, 35.096f, 2.111f), // C GLY
            new Vector3(10.697f, 35.042f, 2.06f)  // O GLY
        };

        Atom.AtomType[] types = new Atom.AtomType[]
        {
            Atom.AtomType.Nitrogen, Atom.AtomType.Hydrogen, Atom.AtomType.Carbon, Atom.AtomType.Hydrogen,
            Atom.AtomType.Carbon, Atom.AtomType.Hydrogen, Atom.AtomType.Hydrogen, Atom.AtomType.Hydrogen,
            Atom.AtomType.Carbon, Atom.AtomType.Oxygen, Atom.AtomType.Nitrogen, Atom.AtomType.Hydrogen,
            Atom.AtomType.Carbon, Atom.AtomType.Hydrogen, Atom.AtomType.Hydrogen, Atom.AtomType.Carbon,
            Atom.AtomType.Oxygen
        };

        for (int i = 0; i < positions.Length; i++)
        {
            CreateAtom(types[i], position + positions[i], rotation);
        }
    }

    private void CreateAlaGly(Vector3 position, Quaternion rotation)
    {
        Vector3[] positions = new Vector3[]
        {
            new Vector3(3.979f, 0.939f, 2.64f), // N ALA
            new Vector3(2.964f, 0.771f, 2.505f), // HN ALA
            new Vector3(4.526f, 2.185f, 2.116f), // CA ALA
            new Vector3(5.591f, 2.217f, 2.368f), // HA ALA
            new Vector3(4.34f, 2.249f, 0.6f), // CB ALA
            new Vector3(4.735f, 3.188f, 0.196f), // HB1 ALA
            new Vector3(4.858f, 1.419f, 0.109f), // HB2 ALA
            new Vector3(3.277f, 2.177f, 0.343f), // HB3 ALA
            new Vector3(3.806f, 3.388f, 2.727f), // C ALA
            new Vector3(2.558f, 3.397f, 2.829f), // O ALA
            new Vector3(4.58f, 4.44f, 3.066f), // N GLY
            new Vector3(5.609f, 4.368f, 2.953f), // HN GLY
            new Vector3(4.003f, 5.675f, 3.572f), // CA GLY
            new Vector3(4.053f, 5.692f, 4.676f), // HA1 GLY
            new Vector3(2.947f, 5.71f, 3.283f), // HA2 GLY
            new Vector3(4.754f, 6.903f, 3.072f), // C GLY
            new Vector3(6.005f, 6.952f, 3.122f) // O GLY
        };

        Atom.AtomType[] types = new Atom.AtomType[]
        {
            Atom.AtomType.Nitrogen, Atom.AtomType.Hydrogen, Atom.AtomType.Carbon, Atom.AtomType.Hydrogen,
            Atom.AtomType.Carbon, Atom.AtomType.Hydrogen, Atom.AtomType.Hydrogen, Atom.AtomType.Hydrogen,
            Atom.AtomType.Carbon, Atom.AtomType.Oxygen, Atom.AtomType.Nitrogen, Atom.AtomType.Hydrogen,
            Atom.AtomType.Carbon, Atom.AtomType.Hydrogen, Atom.AtomType.Hydrogen, Atom.AtomType.Carbon,
            Atom.AtomType.Oxygen
        };

        for (int i = 0; i < positions.Length; i++)
        {
            CreateAtom(types[i], position + positions[i], rotation);
        }
    }

    private void CreateAtom(Atom.AtomType atomType, Vector3 position, Quaternion rotation)
    {
        GameObject atom = Instantiate(atomPrefab, position, rotation, transform);
        Atom atomScript = atom.GetComponent<Atom>();
        atomScript.Initialize(atomType, position, rotation);
    }
}