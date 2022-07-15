using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Harvester : Unit
{
    //--------------------------------------------------
    // Properties
    //--------------------------------------------------
    private IEnumerator harvestCoroutine = null;

    //--------------------------------------------------
    // Initialization
    //--------------------------------------------------
    void Start()
    {
        
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
        yield return new WaitForSecondsRealtime(1.0f);

        while (workplace.resources > 0)
        {
            int dieRoll = RollDie();

            // Prevent accidentally sending remaining resources in a mine to below 0
            if (workplace.resources < dieRoll)
                dieRoll = workplace.resources;

            ResourceManager._instance.resources = dieRoll;
            workplace.resources -= dieRoll;

            Debug.Log(string.Format("{0} resources harvested.", dieRoll), this);

            yield return new WaitForSecondsRealtime(1.0f);
        }
    }
}
