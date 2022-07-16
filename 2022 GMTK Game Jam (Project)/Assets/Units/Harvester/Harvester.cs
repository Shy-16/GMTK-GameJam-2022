using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Harvester : Unit
{
    //--------------------------------------------------
    // Properties
    //--------------------------------------------------
    public override Workplace Workplace { get => workplaceObj; set => vein = (ResourceVein)value; }
    public ResourceVein vein;

    private IEnumerator harvestCoroutine = null;

    //--------------------------------------------------
    // Initialization
    //--------------------------------------------------
    protected override void Start()
    {
        base.Start();

        vein = (ResourceVein)Workplace;
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
            if (vein.resourcesRemaining > 0)
            {
                int dieRoll = RollDie();

                // Prevent accidentally sending remaining resources in a mine to below 0
                if (vein.resourcesRemaining < dieRoll)
                    dieRoll = vein.resourcesRemaining;

                ResourceManager._instance.resourceTotals[(int)vein.resourceType].count += dieRoll;
                vein.resourcesRemaining -= dieRoll;

                Debug.Log(string.Format("{0} resources harvested.", dieRoll), this);

                yield return new WaitForSeconds(1.0f);
            }
        }
    }
}
