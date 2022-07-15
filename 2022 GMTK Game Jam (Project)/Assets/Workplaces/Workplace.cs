using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public enum WorkplaceType
{
    ResourceVein,
    Refinery,
    CraftingTable
}

public class Workplace : MonoBehaviour
{
    //--------------------------------------------------
    // Properties
    //--------------------------------------------------
    public WorkplaceType type;
    public int resources = 1000;

    //--------------------------------------------------
    // Initialization
    //--------------------------------------------------
    void Start()
    {

    }

    //--------------------------------------------------
    // Updates
    //--------------------------------------------------
    void Update()
    {
        
    }

    //--------------------------------------------------
    // Methods
    //--------------------------------------------------

}
