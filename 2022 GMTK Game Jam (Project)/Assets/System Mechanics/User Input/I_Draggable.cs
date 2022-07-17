using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public interface I_Draggable : I_Selectable
{
    bool Held { get; set; }
    I_Slottable ParentSlot { get; set; }
    GameObject ParentSlotObj { get; set; }
    LayerMask SlotMask { get; set; }
}
