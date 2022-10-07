using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Resource : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 dragOffset;
    public Transform parentReturn = null;
    public Transform originParent;

    private Request request;

    public enum ResourceType { none, blue, red, yellow, green };
    public ResourceType resourceType = ResourceType.blue;

    public Image image;

    private void Awake()
    {
        originParent = this.transform.parent;
        //RandomizeColor();
    }

    private void Update()
    {
        UpdateColor();
    }

    public void RandomizeColor()
    {
        for (int i = 0; i < 3; i++)
        {
            int colorInt = Mathf.FloorToInt(Random.Range(0, 4));
            if (colorInt == 0)
            {
                image.color = Color.blue;
                resourceType = Resource.ResourceType.blue;
            }
            else if (colorInt == 1)
            {
                image.color = Color.red;
                resourceType = Resource.ResourceType.red;
            }
            else if (colorInt == 2)
            {
                image.color = Color.yellow;
                resourceType = Resource.ResourceType.yellow;
            }
            else if (colorInt == 3)
            {
                image.color = Color.green;
                resourceType = Resource.ResourceType.green;
            }
            else
            {
                Debug.Log("Error choosing color");
            }
        }
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
        if (parentReturn.parent.tag == "Request")
        {
            request = parentReturn.parent.GetComponent<Request>();
            request.OnPickup();
            request = null;
        }
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
        //null Request request
    }
}
