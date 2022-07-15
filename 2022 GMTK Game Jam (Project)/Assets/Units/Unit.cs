using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    //--------------------------------------------------
    // Properties
    //--------------------------------------------------
    public Die die;
    public Workplace workplace;
    public Vector2 stagingLocation = Vector2.zero;
    public string unitName;

    public bool atWork;

    private IEnumerator transitCoroutine = null;
    private float speed = 0.02f;

    //--------------------------------------------------
    // Initialization
    //--------------------------------------------------
    void Start()
    {

    }

    //--------------------------------------------------
    // Updates
    //--------------------------------------------------
    protected virtual void Update()
    {
        if (die != null && !atWork)
        {
            // If you were on your way back to the staging area when you received a die, stop returning to staging area
            if (transitCoroutine == LeaveWorkplace())
            {
                StopCoroutine(transitCoroutine);
                transitCoroutine = null;
            }

            if (transitCoroutine == null)
            {
                transitCoroutine = GoToWorkplace();
                StartCoroutine(transitCoroutine);
            }
        }
        else if (die == null && atWork)
        {
            if (transitCoroutine != null)
            {
                StopCoroutine(transitCoroutine);
                transitCoroutine = null;
            }

            transitCoroutine = LeaveWorkplace();
            StartCoroutine(transitCoroutine);
        }
    }

    //--------------------------------------------------
    // Methods
    //--------------------------------------------------
    protected int RollDie()
    {
        // The -1 is to get the correct index within the faces array. An upgraded die might not have what you expect at this index
        // The +1 to the random range is to include the highest value on the die
        return Random.Range(die.faces[0], die.faces[die.numbFaces - 1] + 1);
    }

    protected IEnumerator GoToWorkplace()
    {
        Debug.Log("I am leaving for work");

        while (!atWork)
        {
            transform.position = Vector3.MoveTowards(transform.position, workplace.transform.position, speed);

            if (Vector2.Distance(workplace.transform.position, transform.position) == 0)
                atWork = true;

            yield return 0;
        }

        Debug.Log("I have arrived at work.", this);
    }

    protected IEnumerator LeaveWorkplace()
    {
        atWork = false;
        Debug.Log("I am leaving work.", this);

        while (Vector2.Distance(stagingLocation, transform.position) > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, stagingLocation, speed);

            yield return 0;
        }

        Debug.Log("I have arrived at the staging area.");
    }
}
