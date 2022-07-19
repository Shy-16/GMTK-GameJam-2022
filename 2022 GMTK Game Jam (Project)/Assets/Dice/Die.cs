using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Die : MonoBehaviour, I_Draggable
{
    //--------------------------------------------------
    // Properties
    //--------------------------------------------------
    public int numbFaces;
    public int[] faces;

    public bool assigned = false;

    public bool Selected { get; set; }
    public bool Held { get; set; }
    public I_Slottable ParentSlot { get; set; }
    public GameObject ParentSlotObj { get; set; }
    public LayerMask Mask { get; set; }
    public LayerMask SlotMask { get; set; }

    private Canvas worldSpaceCanvas;
    private Image imageComponent;

    //--------------------------------------------------
    // Initialization
    //--------------------------------------------------
    void Start()
    {
        Mask = LayerMask.GetMask(LayerMask.LayerToName(gameObject.layer));
        SlotMask = LayerMask.GetMask("Dice Slots");
        worldSpaceCanvas = GetComponentInChildren<Canvas>();
        imageComponent = GetComponent<Image>();

        worldSpaceCanvas.worldCamera = Camera.main;

        transform.position = ParentSlotObj.transform.position;

        AssignDefaultfaces(ref faces, numbFaces);
    }

    //--------------------------------------------------
    // Update
    //--------------------------------------------------
    private void Update()
    {
        if (ParentSlotObj != null && !Held)
            transform.position = ParentSlotObj.transform.position - Vector3.forward;
    }

    //--------------------------------------------------
    // Methods
    //--------------------------------------------------
    private void AssignDefaultfaces(ref int[] facesArray, int numbOfFaces)
    {
        switch (numbOfFaces)
        {
            default:
                {
                    facesArray = new int[4] { 1, 2, 3, 4 };
                    break;
                }
            case 6:
                {
                    facesArray = new int[6] { 1, 2, 3, 4, 5, 6 };
                    break;
                }
            case 8:
                {
                    facesArray = new int[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
                    break;
                }
            case 10:
                {
                    facesArray = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                    break;
                }
            case 12:
                {
                    facesArray = new int[12] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
                    break;
                }
            case 20:
                {
                    facesArray = new int[20] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
                    break;
                }
        }
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

    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 8);
        SelectionController.holding = true;
        Held = true;
    }

    private void OnMouseUpAsButton()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);

        Physics.Raycast(ray, out hit, 100.0f, SlotMask.value, QueryTriggerInteraction.Collide);

        I_Slottable potentialSlot = null;

        if (hit.collider != null)
            potentialSlot = hit.transform.GetComponent<I_Slottable>();

        if (potentialSlot != null && potentialSlot.SlottedDraggable == null)
        {
            ParentSlot.UpdateSlot(false, null);

            ParentSlot = potentialSlot;
            ParentSlotObj = potentialSlot.UpdateSlot(true, gameObject);

            transform.position = ParentSlotObj.transform.position - Vector3.forward;
        }
        else if (potentialSlot == null || potentialSlot.SlottedDraggable != null)
        {
            transform.position = ParentSlotObj.transform.position - Vector3.forward;
        }

#pragma warning disable CS0252
        if (SelectionController.selectedItem == this)
        {
            SelectionController.holding = false;
            SelectionController.UpdateSelected(this, false);
        }
#pragma warning restore CS0252
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
        Held = false;
    }
}
