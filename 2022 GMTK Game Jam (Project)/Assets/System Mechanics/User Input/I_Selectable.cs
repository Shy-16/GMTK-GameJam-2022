using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public interface I_Selectable
{
    LayerMask Mask { get; set; }
    bool Selected { get; set; }

    void OnSelect();
    void OnDeselect();
}
