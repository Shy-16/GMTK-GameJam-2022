using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    //--------------------------------------------------
    // Properties
    //--------------------------------------------------
    public static ResourceManager _instance;
    public int resources = 0;

    //--------------------------------------------------
    // Initialization
    //--------------------------------------------------
    void Awake()
    {
        _instance = this;
    }

    //--------------------------------------------------
    // Methods
    //--------------------------------------------------

}
