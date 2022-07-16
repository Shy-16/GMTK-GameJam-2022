using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[System.Serializable]
public struct Product
{
    //--------------------------------------------------
    // Properties
    //--------------------------------------------------
    public ProductRecipe recipe;
    public int dieRoll;

    //--------------------------------------------------
    // Initialization
    //--------------------------------------------------
    public Product(ProductRecipe myRecipe, int roll)
    {
        recipe = myRecipe;
        dieRoll = roll;
    }
}
