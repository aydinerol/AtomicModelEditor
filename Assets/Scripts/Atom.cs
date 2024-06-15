using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atom : MonoBehaviour
{
    //ATOM NO BAÐ TÜR PEPTÝT AD   ZÝNCÝR ETÝKET   PEPTÝT NO   X Y   Z?	?	ATOM ADI

    private string name; // 
    private string connectionType;
    private string peptideType;
    private string peptideGroup;

    private float xCoordinate;
    private float yCoordinate;
    private float zCoordinate;

    public enum AtomType { Carbon, Oxygen, Nitrogen, Hydrogen }
    public AtomType type;
    public int availableBonds;

    public void Initialize(AtomType atomType, Vector3 position, Quaternion rotation)
    {
        type = atomType;
        transform.position = position;
        transform.rotation = rotation;
        SetColor();
        SetAvailableBonds();
    }

    private void SetColor()
    {
        Renderer renderer = GetComponent<Renderer>();
        switch (type)
        {
            case AtomType.Carbon:
                renderer.material.color = Color.black;
                break;
            case AtomType.Oxygen:
                renderer.material.color = Color.red;
                break;
            case AtomType.Nitrogen:
                renderer.material.color = Color.blue;
                break;
            case AtomType.Hydrogen:
                renderer.material.color = Color.white;
                break;
        }
    }

    private void SetAvailableBonds()
    {
        switch (type)
        {
            case AtomType.Carbon:
                availableBonds = 4;
                break;
            case AtomType.Oxygen:
                availableBonds = 2;
                break;
            case AtomType.Nitrogen:
                availableBonds = 3;
                break;
            case AtomType.Hydrogen:
                availableBonds = 1;
                break;
        }
    }
}
