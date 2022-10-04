using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 dragOffset;
    public Transform parentReturn = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");

        dragOffset = new Vector2(eventData.position.x - this.transform.position.x, eventData.position.y - this.transform.position.y);

        parentReturn = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");

        this.transform.position = new Vector2(eventData.position.x - dragOffset.x, eventData.position.y - dragOffset.y);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");

        this.transform.SetParent(parentReturn);

        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
