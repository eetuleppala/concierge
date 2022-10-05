using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Request : MonoBehaviour, IDropHandler
{
    public Resource.ResourceType[] requirements = new Resource.ResourceType[3];
    public Resource.ResourceType[] fulfilledReqs = new Resource.ResourceType[3];

    public Image[] images;
    public Image cardHolder;

    public TMP_Text debugText;

    private Resource resource;

    private void Start()
    {
        CreateRequirements();
    }

    private void Update()
    {
        debugText.text =
            "requirements: "
            + requirements[0]
            + ", " + requirements[1]
            + ", " + requirements[2]
            + "; fulfilledReqs: "
            + fulfilledReqs[0]
            + ", " + fulfilledReqs[1]
            + ", " + fulfilledReqs[2];

        //if (resource != null && resource.pickedUp == true)
        //{
        //    OnPickup();
        //}
    }

    private void CreateRequirements()
    {
        for (int i = 0; i < 3; i++)
        {
            int colorInt = Mathf.FloorToInt(Random.Range(0, 4));
            if (colorInt == 0)
            {
                images[i].color = Color.blue;
                requirements[i] = Resource.ResourceType.blue;
            }
            else if (colorInt == 1)
            {
                images[i].color = Color.red;
                requirements[i] = Resource.ResourceType.red;
            }
            else if (colorInt == 2)
            {
                images[i].color = Color.yellow;
                requirements[i] = Resource.ResourceType.yellow;
            }
            else if (colorInt == 3)
            {
                images[i].color = Color.green;
                requirements[i] = Resource.ResourceType.green;
            }
            else
            {
                Debug.Log("Error choosing color");
            }
        }
    }


    // Drag & Drop

    public void OnDrop(PointerEventData eventData)
    {
        resource = eventData.pointerDrag.GetComponent<Resource>();
        Debug.Log(resource.name + " was dropped on " + gameObject.name);
        if (resource != null)
        {
            for (int i = 0; i < requirements.Length; i++)
            {
                if (requirements[i] == resource.resourceType)
                {
                    fulfilledReqs[i] = requirements[i];
                    requirements[i] = Resource.ResourceType.none;
                    resource.parentReturn = cardHolder.transform;
                    return;
                }
            }
            /*if (resource.parentReturn != cardHolder.transform)
            {
                for (int i = 0; i < requirements.Length; i++)
                {
                    if (fulfilledReqs[i] == resource.resourceType)
                    {
                        requirements[i] = fulfilledReqs[i];
                        fulfilledReqs[i] = Resource.ResourceType.none;
                        resource = null;
                        return;
                    }
                }
            }*/
        }
    }

    public void OnPickup()
    {
        Debug.Log(resource.name + " was picked up from top of " + gameObject.name);
        if (resource != null) // && resource.parentReturn != cardHolder.transform)
        {
            for (int i = 0; i < requirements.Length; i++)
            {
                if (fulfilledReqs[i] == resource.resourceType)
                {
                    requirements[i] = fulfilledReqs[i];
                    fulfilledReqs[i] = Resource.ResourceType.none;
                    return;
                }
            }
        }
    }

    private void DestroyRequest()
    {

    }
}

