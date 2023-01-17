using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Researcher : Unit
{
    //--------------------------------------------------
    // Properties
    //--------------------------------------------------
    public override Workplace Workplace { get => workplaceObj; set => lab = (ResearchLab)value; }
    public ResearchLab lab;

    private IEnumerator researchCoroutine = null;
    private bool resourcesAvailable = false;

    //--------------------------------------------------
    // Initialization
    //--------------------------------------------------
    protected override void Start()
    {
        base.Start();

        lab = (ResearchLab)Workplace;
    }

    //--------------------------------------------------
    // Update
    //--------------------------------------------------
    protected override void Update()
    {
        base.Update();

        // Assume we have enough resources of each type before parsing through stash
        // If even one resource is short, flip flag
        for (int i = 0; i < lab.currentPlan.requiredResources.Length; ++i)
        {
            resourcesAvailable = true;

            if (ResourceManager._instance.resourceTotals[(int)lab.currentPlan.requiredResources[i].resourceType].count < lab.currentPlan.requiredResources[i].count)
            {
                resourcesAvailable = false;
            }
        }

        if (atWork && researchCoroutine == null)
        {
            researchCoroutine = DoResearch();
            StartCoroutine(researchCoroutine);
        }
        else if (!atWork && researchCoroutine != null)
        {
            StopCoroutine(researchCoroutine);
            researchCoroutine = null;
        }
    }

    //--------------------------------------------------
    // Methods
    //--------------------------------------------------
    private IEnumerator DoResearch()
    {
        yield return new WaitForSeconds(1.0f);

        while (atWork)
        {
            if (resourcesAvailable && die != null && !die.Held)
            {
                if (lab.progressRatio >= 1)
                    break;

                int dieRoll = RollDie();

                if (lab.currentPlan.requiredResearchPoints - lab.researchPoints < dieRoll)
                    dieRoll = lab.currentPlan.requiredResearchPoints - lab.researchPoints;

                // Don't go above 100% research
                for (int i = 0; i < lab.currentPlan.requiredResources.Length; ++i)
                    ResourceManager._instance.resourceTotals[(int)lab.currentPlan.requiredResources[i].resourceType].count -= lab.currentPlan.requiredResources[i].count;

                lab.researchPoints += dieRoll;

                // these casts aren't redunant; vs and unity disagree about this
                lab.progressRatio = (float)lab.researchPoints / (float)lab.currentPlan.requiredResearchPoints;

                Debug.Log(string.Format("Research Points: {0}", lab.researchPoints), this);
                Debug.Log(string.Format("Research Progress: {0}%", lab.progressRatio * 100), this);
            }

            yield return new WaitForSeconds(1.0f);
        }

        Debug.Log(string.Format("<b>Research for {0} has been completed.</b>", lab.currentPlan.researchObj), this);
        // Reward research
    }
}
