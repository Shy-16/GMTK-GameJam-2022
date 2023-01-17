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
    public Unit unit;

    private Coroutine shakeRoutine;

    //--------------------------------------------------
    // Update
    //--------------------------------------------------
    private void Awake()
    {
        unit = GetComponentInParent<Unit>();

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
    public GameObject UpdateSlot(bool inserting, GameObject obj)
    {
        if (inserting)
        {
            I_Draggable draggable = obj.GetComponent<I_Draggable>();

            if (draggable != null)
            {
                SlottedDraggable = draggable;
                slottedObj = obj;

                if (unit != null)
                    unit.die = obj.GetComponent<Die>();

                return gameObject;
            }
        }
        else
        {
            SlottedDraggable = null;
            slottedObj = null;

            if (unit != null)
                unit.die = null;

            return gameObject;
        }

        return null;
    }

    public void Shake()
    {
        if (shakeRoutine != null)
        {
            StopCoroutine(shakeRoutine);
            shakeRoutine = null;
        }

        shakeRoutine = StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        int i = 0;
        float shakeMagnitudeScalar = 1f;
        float shakeDecayRate = 0.5f;

        Vector2 newOffset;

        while (shakeMagnitudeScalar >= 0.01f)
        {
            newOffset = Random.insideUnitCircle;
            transform.localPosition = (newOffset * shakeMagnitudeScalar) / 2;
            
            i++;
            shakeMagnitudeScalar = Mathf.Pow(shakeDecayRate, i);
            yield return new WaitForSeconds(0.025f);
        }
        
        Debug.Log("Coroutine Over");
    }    
}
