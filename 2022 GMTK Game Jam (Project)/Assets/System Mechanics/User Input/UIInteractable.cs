using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class UIInteractable : MonoBehaviour, I_Draggable
{
    public bool Selected { get; set; }
    public bool Held { get; set; }
    public I_Slottable ParentSlot { get; set; }
    public I_Slottable SlotHovered { get; set; }
    public LayerMask Mask { get; set; }

    public Sprite sprite;
    public Image imageComponent;

    public void OnSelect()
    {
        throw new System.NotImplementedException();
    }

    public void OnDeselect()
    {
        throw new System.NotImplementedException();
    }
}
