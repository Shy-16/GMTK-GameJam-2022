using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
public enum ResourceType
{
    Canvas,
    Glue,
    Hole,
    Metal,
    Nitroglycerin,
    OldBoot,
    Paint,
    Wheel,
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
    public TextMeshProUGUI[] resourceTexts;
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

        UpdateResourceCount();
    }

    //--------------------------------------------------
    // Methods
    //--------------------------------------------------
    public void UpdateResourceCount()
    {
        for (int i = 0; i < resourceTotals.Length; ++i)
        {
            if (i < resourceTexts.Length)
                resourceTexts[i].text = resourceTotals[i].count.ToString();
        }
    }

    public void AddResources(ResourceType type, int amount)
    {
        resourceTotals[(int)type].count += amount;
        UpdateResourceCount();
    }

    public void SubtractResources(ResourceType type, int amount)
    {
        resourceTotals[(int)type].count -= amount;
        UpdateResourceCount();
    }
}
