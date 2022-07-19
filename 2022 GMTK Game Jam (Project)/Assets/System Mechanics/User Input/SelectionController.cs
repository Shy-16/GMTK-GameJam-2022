using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class SelectionController : MonoBehaviour
{
    //--------------------------------------------------
    // Properties
    //--------------------------------------------------
    public static I_Selectable selectedItem;
    public static List<I_Selectable> hoveredItems = new();
    
    public static bool holding = false;

    public LayerMask unitLayer;
    public LayerMask uiLayer;

    public DieSlot[] dieSlots = new DieSlot[3];

    //--------------------------------------------------
    // Update
    //--------------------------------------------------
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && hoveredItems.Count == 0)
        {
            if (selectedItem != null)
                selectedItem.OnDeselect();
            selectedItem = null;
            //Debug.Log("Selection is now empty");
        }

        if (Input.GetMouseButtonDown(1) && hoveredItems.Count > 0)
        {
            RaycastHit hit;
            Unit unit = null;
            Ray ray = new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);

            Physics.Raycast(ray, out hit, 100.0f, unitLayer.value, QueryTriggerInteraction.Collide);

            if (hit.collider != null)
                unit = hit.transform.GetComponent<Unit>();
            
            if (unit != null)
            {
                for (int i = 0; i < dieSlots.Length; ++i)
                {
                    if (dieSlots[i].slottedObj == null)
                    {
                        Die die = unit.die;

                        unit.die = null;

                        die.ParentSlot = dieSlots[i];
                        die.ParentSlotObj = dieSlots[i].gameObject;
                        die.transform.position = die.ParentSlotObj.transform.position - Vector3.forward;

                        dieSlots[i].UpdateSlot(true, die.gameObject);
                    }
                }
            }
        }
    }

    //--------------------------------------------------
    // Methods
    //--------------------------------------------------
    public static bool UpdateHover(I_Selectable newHover, bool adding)
    {
        if (adding && !hoveredItems.Contains(newHover))
        {
            if (newHover != null)
            {
                hoveredItems.Add(newHover);
                Debug.Log(string.Format("{0} is being hovered.", newHover));
            }
            else
            {
                Debug.LogError("Tried to Push a null object onto Hover Stack");
                return false;
            }
        }
        else if (!adding && hoveredItems.Contains(newHover))
        {
            hoveredItems.Remove(newHover);
            Debug.Log(string.Format("{0} is no longer being hovered.", newHover));
        }

        return true;
    }

    public static bool UpdateSelected(I_Selectable newSelection, bool selecting = true)
    {
        if (selecting && newSelection != selectedItem)
        {
            //----------DEBUG----------
            //var strBuilder = new StringBuilder();
            //strBuilder.Append("<b>HOVEREDITEMS LIST CONTENTS:</b>");

            //foreach (I_Selectable selectable in hoveredItems)
            //{
            //    strBuilder.Append("\n");
            //    strBuilder.Append(selectable.ToString());
            //}

            //Debug.Log(strBuilder.ToString());
            //-------------------------

            if (!hoveredItems.Contains(newSelection))
            {
                Debug.LogError("Tried to select and item not in HoveredItems list.");
                return false;
            }

            // Mark the old selected item as deselected
            // If the newSelection is on a more forward layer, it is the only choice to select; return
            if (selectedItem != null)
            {
                selectedItem.OnDeselect();
                
                if (selectedItem.Mask.value < newSelection.Mask.value)
                {
                    selectedItem = newSelection;
                    selectedItem.OnSelect();
                    //Debug.Log(string.Format("{0} is now selected", selectedItem));
                    return true;
                }
            }

            // If new and current selections are on the same layer or on a less forward layer, we gotta check the stack of hovered items
            // Bey default, foreach will default to the most recent addition to the hover list if all items are on the same layer
            I_Selectable highestPriority = null;

            foreach (I_Selectable selectable in hoveredItems)
            {
                if (highestPriority == null)
                    highestPriority = selectable;
                else if (highestPriority.Mask.value <= selectable.Mask.value)
                    highestPriority = selectable;
            }

            selectedItem = highestPriority;
            selectedItem.OnSelect();
        }
        else if (!selecting)
        {
            if (selectedItem == newSelection)
                selectedItem = null;

            if (holding)
                holding = false;

            newSelection.OnDeselect();
        }

        //Debug.Log(string.Format("{0} is now selected", selectedItem));
        return true;
    }
}
