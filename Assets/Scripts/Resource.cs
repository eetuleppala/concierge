using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Resource : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 dragOffset;
    public Transform parentReturn = null;

    public enum ResourceType { none, blue, red, yellow, green };
    public ResourceType resourceType = ResourceType.blue;

    public Image image;

    public bool isDragged = false;

    private void Update()
    {
        UpdateColor();
    }

    private void UpdateColor()
    {
        if (resourceType == ResourceType.blue)
        {
            image.color = Color.blue;
        }
        else if (resourceType == ResourceType.red)
        {
            image.color = Color.red;
        }
        else if (resourceType == ResourceType.yellow)
        {
            image.color = Color.yellow;
        }
        else if (resourceType == ResourceType.green)
        {
            image.color = Color.green; 
        }
    }




    // Drag & Drop

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");

        dragOffset = new Vector2(eventData.position.x - this.transform.position.x, eventData.position.y - this.transform.position.y);

        parentReturn = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
        //Request request = parentReturn.name;
        isDragged = true;

    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");

        this.transform.position = new Vector2(eventData.position.x - dragOffset.x, eventData.position.y - dragOffset.y);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");

        this.transform.SetParent(parentReturn);

        GetComponent<CanvasGroup>().blocksRaycasts = true;
        isDragged = false;
    }
}
