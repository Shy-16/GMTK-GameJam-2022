using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Harvester : Unit
{
    //--------------------------------------------------
    // Properties
    //--------------------------------------------------
    public override Workplace Workplace { get => workplaceObj; set => bag = (BagOfHolding)value; }
    public BagOfHolding bag;

    private IEnumerator harvestCoroutine = null;

    //--------------------------------------------------
    // Initialization
    //--------------------------------------------------
    protected override void Start()
    {
        base.Start();

        bag = (BagOfHolding)Workplace;
    }

    //--------------------------------------------------
    // Updates
    //--------------------------------------------------
    protected override void Update()
    {
        base.Update();

        if (atWork && harvestCoroutine == null)
        {
            harvestCoroutine = HarvestResource();
            StartCoroutine(harvestCoroutine);
        }
        else if (!atWork && harvestCoroutine != null)
        {
            StopCoroutine(harvestCoroutine);
            harvestCoroutine = null;
        }
    }

    //--------------------------------------------------
    // Methods
    //--------------------------------------------------
    private IEnumerator HarvestResource()
    {
        yield return new WaitForSeconds(1.0f);

        while (atWork)
        {
            if (bag.resourcesRemaining > 0 && die != null && !die.Held)
            {
                int dieRoll = RollDie();

                // Prevent accidentally sending remaining resources in a mine to below 0
                if (bag.resourcesRemaining < dieRoll)
                    dieRoll = bag.resourcesRemaining;

                ResourceManager._instance.AddResources(bag.resourceType, dieRoll);
                bag.resourcesRemaining -= dieRoll;

                Debug.Log(string.Format("{0} resources harvested.", dieRoll), this);
            }

            yield return new WaitForSeconds(1.0f);
        }
    }
}
