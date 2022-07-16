using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Scriptable Objects/Research Plan", order = 1)]

public class ResearchPlan : ScriptableObject
{
    //--------------------------------------------------
    // Properties
    //--------------------------------------------------
    public int requiredResearchPoints;

    public IngredientLine[] requiredResources;

    public GameObject researchObj;   // A prefab of the pre-made object being researched
}
