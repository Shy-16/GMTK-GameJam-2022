using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Die : MonoBehaviour
{
    //--------------------------------------------------
    // Properties
    //--------------------------------------------------
    public int numbFaces;
    public int[] faces;

    public bool assigned = false;

    //--------------------------------------------------
    // Initialization
    //--------------------------------------------------
    void Start()
    {
        AssignDefaultfaces(ref faces, numbFaces);
    }

    //--------------------------------------------------
    // Methods
    //--------------------------------------------------
    private void AssignDefaultfaces(ref int[] facesArray, int numbOfFaces)
    {
        switch (numbOfFaces)
        {
            default:
                {
                    facesArray = new int[4] { 1, 2, 3, 4 };
                    break;
                }
            case 6:
                {
                    facesArray = new int[6] { 1, 2, 3, 4, 5, 6 };
                    break;
                }
            case 8:
                {
                    facesArray = new int[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
                    break;
                }
            case 10:
                {
                    facesArray = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                    break;
                }
            case 12:
                {
                    facesArray = new int[12] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
                    break;
                }
            case 20:
                {
                    facesArray = new int[20] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
                    break;
                }
        }
    }
}
