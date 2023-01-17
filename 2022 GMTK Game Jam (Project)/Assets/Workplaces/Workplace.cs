using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public abstract class Workplace : MonoBehaviour, I_Selectable
{
    //--------------------------------------------------
    // Properties
    //--------------------------------------------------
    public bool Selected { get; set; }
    public LayerMask Mask { get; set; }

    //--------------------------------------------------
    // Initializations
    //--------------------------------------------------
    protected virtual void Start()
    {
        Mask = LayerMask.GetMask(LayerMask.LayerToName(gameObject.layer));
        //Debug.Log(Mask.value, this);
    }

    //--------------------------------------------------
    // Methods
    //--------------------------------------------------

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
