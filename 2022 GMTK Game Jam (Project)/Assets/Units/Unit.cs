using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public abstract class Unit : MonoBehaviour, I_Selectable
{
    //--------------------------------------------------
    // Properties
    //--------------------------------------------------
    public Die die;
    public DieSlot slot;
    public virtual Workplace Workplace { get; set; }
    public bool Selected { get; set; }
    public LayerMask Mask { get; set; }

    public Workplace workplaceObj;
    public Vector2 stagingLocation = Vector2.zero;
    public string unitName;

    public bool atWork;

    private Canvas worldSpaceCanvas;
    private IEnumerator transitCoroutine = null;
    private float speed = 0.02f;

    //--------------------------------------------------
    // Initializations
    //--------------------------------------------------
    protected virtual void Start()
    {
        slot = GetComponentInChildren<DieSlot>();

        worldSpaceCanvas = GetComponentInChildren<Canvas>();
        worldSpaceCanvas.worldCamera = Camera.main;

        Mask = LayerMask.GetMask(LayerMask.LayerToName(gameObject.layer));
        //Debug.Log(Mask.value, this);
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
        slot.Shake();

        // The -1 is to get the correct index within the faces array. An upgraded die might not have what you expect at this index
        // The +1 to the random range is to include the highest value on the die
        return Random.Range(die.faces[0], die.faces[die.numbFaces - 1] + 1);
    }

    protected IEnumerator GoToWorkplace()
    {
        //Debug.Log("<i>I am leaving for work</i>");

        while (!atWork)
        {
            transform.position = Vector3.MoveTowards(transform.position, Workplace.transform.position - Vector3.forward, speed);

            if (Vector2.Distance(Workplace.transform.position, transform.position) <= 0.5f)
                atWork = true;

            yield return 0;
        }

        //Debug.Log("<i>I have arrived at work.</i>", this);
    }

    protected IEnumerator LeaveWorkplace()
    {
        atWork = false;
        //Debug.Log("<i>I am leaving work.</i>", this);

        while (Vector2.Distance(stagingLocation, transform.position) > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(stagingLocation.x, stagingLocation.y, 0) - Vector3.forward, speed);

            yield return 0;
        }

        transitCoroutine = null;
        //Debug.Log("<i>I have arrived at the staging area.</i>");
    }

    //--------------------------------------------------
    // Events and Messages
    //--------------------------------------------------
    private void OnMouseEnter()
    {
        SelectionController.UpdateHover(this, true);
    }

    private void OnMouseExit()
    {
        SelectionController.UpdateHover(this, false);
    }

    private void OnMouseDown()
    {
        Selected = SelectionController.UpdateSelected(this);
    }

    public void OnSelect()
    {
        transform.localScale *= 1.25f;
        Selected = true;
    }

    public void OnDeselect()
    {
        transform.localScale = Vector3.one;
        Selected = false;
    }
}
