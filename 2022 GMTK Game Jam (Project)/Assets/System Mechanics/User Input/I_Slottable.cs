using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public interface I_Slottable
{
    I_Draggable SlottedDraggable { get; set; }
    public GameObject UpdateSlot(bool inserting, GameObject newDraggable);
}
