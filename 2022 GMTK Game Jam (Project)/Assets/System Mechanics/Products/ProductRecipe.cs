using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName ="New Recipe", menuName ="Scriptable Objects/Recipe", order = 1)]
public class ProductRecipe : ScriptableObject
{
    //--------------------------------------------------
    // Properties
    //--------------------------------------------------
    public IngredientLine[] recipe;
    public int baseValue;
}
