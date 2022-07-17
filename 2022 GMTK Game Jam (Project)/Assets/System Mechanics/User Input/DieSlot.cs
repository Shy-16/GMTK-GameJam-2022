using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class DieSlot : MonoBehaviour, I_Slottable
{
    //--------------------------------------------------
    // Properties
    //--------------------------------------------------
    public I_Draggable SlottedDraggable { get; set; }
    public GameObject slottedObj;

    //--------------------------------------------------
    // Update
    //--------------------------------------------------
    private void Awake()
    {
        if (slottedObj != null)
        {
            I_Draggable childDraggable = slottedObj.GetComponent<I_Draggable>();

            childDraggable.ParentSlot = this;
            childDraggable.ParentSlotObj = gameObject;
            SlottedDraggable = childDraggable;
        }
    }

    //--------------------------------------------------
    // Methods
    //--------------------------------------------------
}
