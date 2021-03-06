using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public enum ResourceType
{
    Cotton,
    Coal,
    Gemstone,
    Glass,
    MetalOre,
    Metal,
    Oil,
    Plastic,
    Sand,
    Wood
}

[System.Serializable]
public struct IngredientLine
{
    public ResourceType resourceType;
    public int count;
}
public class ResourceManager : MonoBehaviour
{
    //--------------------------------------------------
    // Properties
    //--------------------------------------------------
    public static ResourceManager _instance;
    public IngredientLine[] resourceTotals = new IngredientLine[10];

    //--------------------------------------------------
    // Initialization
    //--------------------------------------------------
    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        for (int i = 0; i < resourceTotals.Length; ++i)
        {
            resourceTotals[i].resourceType = (ResourceType)i;
            resourceTotals[i].count = 0;
        }
    }

    //--------------------------------------------------
    // Methods
    //--------------------------------------------------

}
