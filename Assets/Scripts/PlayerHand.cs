using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerHand : MonoBehaviour, IDropHandler
{
    public void OnDrop (PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);

        Resource d = eventData.pointerDrag.GetComponent<Resource>();
        if (d != null)
        {
            d.parentReturn = this.transform;
        }
    }
}
