using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class ResearchLab : Workplace
{
    //--------------------------------------------------
    // Properties
    //--------------------------------------------------
    public ResearchPlan currentPlan;
    public ResearchPlan[] allPlans;

    public int researchPoints = 0;
    public float progressRatio = 0.0f;

    public Slider progressSlider;

    //--------------------------------------------------
    // Initializations
    //--------------------------------------------------
    protected override void Start()
    {
        base.Start();

        progressSlider = GetComponentInChildren<Slider>();
        progressSlider.value = progressRatio;
    }

    //--------------------------------------------------
    // Methods
    //--------------------------------------------------
    public void UpdateProgress(float newRatio)
    {
        if (newRatio > 1)
            progressRatio = 1;
        else if (newRatio < 0)
            progressRatio = 0;
        else
            progressRatio = newRatio;

        progressSlider.value = progressRatio;
    }
}
