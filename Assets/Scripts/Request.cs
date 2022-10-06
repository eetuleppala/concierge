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
    public TMP_Text propertiesText;
    public ProgressTracker tracker;

    private Resource resource;

    private int timeToComplete;
    private int payAmount;
    private int reputationGain;
    private int reputationLoss;

    private bool isCompleted = false;

    private void Start()
    {
        CreateRequirements();
        CreateProperties();
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

        if (isCompleted == true)
            DestroyRequest(true);

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

    private void CreateProperties()
    {
        timeToComplete = Random.Range(20, 60);

        //hardcoded probabilities for payAmount
        float rand = Random.value;
        if (rand <= .5f)
            payAmount = Random.Range(0, 10);
        if (rand <= .8f)
            payAmount = Random.Range(11, 20);
        else
            payAmount = Random.Range(21, 50);

        //hardcoded probabilities for reputationGain
        rand = Random.value;
        if (rand <= .6f)
            reputationGain = Random.Range(0, 6);
        if (rand <= .9f)
            reputationGain = Random.Range(7, 15);
        else
            reputationGain = Random.Range(16, 30);

        //hardcoded probabilities for reputationLoss
        rand = Random.value;
        if (rand <= .95f)
            reputationLoss = Random.Range(0, 4);
        else
            reputationLoss = Random.Range(5, 10);

        propertiesText.text = "$: " + payAmount + "RepGain: " + reputationGain + "RepLoss: " + reputationLoss;
    }


    // Drag & Drop, Update request status, Assess completion, Destroy

    public void OnDrop(PointerEventData eventData)
    {
        resource = eventData.pointerDrag.GetComponent<Resource>();
        //Debug.Log(resource.name + " was dropped on " + gameObject.name);
        if (resource != null)
        {
            for (int i = 0; i < requirements.Length; i++)
            {
                if (requirements[i] == resource.resourceType)
                {
                    fulfilledReqs[i] = requirements[i];
                    requirements[i] = Resource.ResourceType.none;
                    resource.parentReturn = cardHolder.transform;
                    if (fulfilledReqs[0] != Resource.ResourceType.none && fulfilledReqs[1] != Resource.ResourceType.none && fulfilledReqs[2] != Resource.ResourceType.none)
                        isCompleted = true;
                    return;
                }
            }
            {
                DestroyRequest(true);
            }
        }
    }

    public void OnPickup()
    {
        //Debug.Log(resource.name + " was picked up from top of " + gameObject.name);
        if (resource != null)
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

    private void DestroyRequest(bool completed)
    {
        Resource[] r = cardHolder.GetComponentsInChildren<Resource>();

        tracker = 
        for (int i = 0; i < r.Length; i++)
        {
            Debug.Log("Setting origin parent for " + r[i].name);
            r[i].transform.SetParent(r[i].originParent);
        }

        if (completed == true)
        {
            Debug.Log("Request completed");
            tracker.UpdateMoney(payAmount);
            tracker.UpdateReputation(reputationGain);
            isCompleted = false;
        }
        else if (completed == false)
        {
            tracker.UpdateReputation(-reputationLoss);
        }
        Destroy(this.gameObject);
    }
}

