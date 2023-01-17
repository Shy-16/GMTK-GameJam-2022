using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : Unit
{
    //--------------------------------------------------
    // Properties
    //--------------------------------------------------
    public override Workplace Workplace { get => workplaceObj; set => table = (CraftingTable)value; }
    public CraftingTable table;

    private IEnumerator craftCoroutine = null;
    private bool resourcesAvailable = false;

    //--------------------------------------------------
    // Initialization
    //--------------------------------------------------
    protected override void Start()
    {
        base.Start();

        table = (CraftingTable)Workplace;
    }

    //--------------------------------------------------
    // Updates
    //--------------------------------------------------
    protected override void Update()
    {
        base.Update();

        // Assume we have enough resources of each type before parsing through stash
        // If even one resource is short, flip flag
        for (int i = 0; i < table.productRecipe.recipe.Length; ++i)
        {
            resourcesAvailable = true;

            if (ResourceManager._instance.resourceTotals[(int)table.productRecipe.recipe[i].resourceType].count < table.productRecipe.recipe[i].count)
            {
                resourcesAvailable = false;
            }
        }

        if (atWork && craftCoroutine == null)
        {
            craftCoroutine = CraftProduct();
            StartCoroutine(craftCoroutine);
        }
        else if (!atWork && craftCoroutine != null)
        {
            StopCoroutine(craftCoroutine);
            craftCoroutine = null;
        }
    }

    //--------------------------------------------------
    // Methods
    //--------------------------------------------------
    private IEnumerator CraftProduct()
    {
        yield return new WaitForSeconds(1.0f);

        while (atWork)
        {
            if (resourcesAvailable && die != null && !die.Held)
            {
                int dieRoll = RollDie();

                for (int i = 0; i < table.productRecipe.recipe.Length; ++i)
                    ResourceManager._instance.resourceTotals[(int)table.productRecipe.recipe[i].resourceType].count -= table.productRecipe.recipe[i].count;

                Product newProduct = new Product(table.productRecipe, dieRoll);

                ProductManager._isntance.finishedProducts.Add(newProduct);

                Debug.Log(string.Format("<b>{0} product crafted.</b>", newProduct), this);
            }

            yield return new WaitForSeconds(1.0f);
        }
    }
}
