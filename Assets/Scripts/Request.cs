using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Request : MonoBehaviour, IDropHandler
{
    private Resource.ResourceType[] requirements;
    private Resource.ResourceType[] fulfilledReqs;

    public Image[] requirementCols;
    public Image cardHolder;
    public Image declineButton;

    public TMP_Text debugText;
    public TMP_Text propertiesText;
    private ProgressTracker tracker;

    private Resource resource;

    public float requestTime = 60f;
    public Slider requestTimer;
    public float completeTime = 30f;
    public Slider completeTimer;

    private int payAmount;
    private int reputationGain;
    private int reputationLoss;

    private bool hasRequirements = false;
    private bool isCompleted = false;

    private void Start()
    {
        requirements = new Resource.ResourceType[3];
        fulfilledReqs = new Resource.ResourceType[3];
        tracker = GameObject.Find("ProgressTracker").GetComponent<ProgressTracker>();
        requestTimer.maxValue = requestTime;
        completeTimer.maxValue = completeTime;

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

        if (requestTimer.value < requestTimer.maxValue)
            requestTimer.value += Time.deltaTime;
        else
            DestroyRequest(false, false);

        if (hasRequirements == true)
        {
            requestTimer.gameObject.SetActive(false);
            completeTimer.gameObject.SetActive(true);
            if (completeTimer.value < completeTimer.maxValue)
                completeTimer.value += Time.deltaTime;
            else if (completeTimer.value >= completeTimer.maxValue)
                isCompleted = true;
        }
        else
        {
            requestTimer.gameObject.SetActive(true);
            completeTimer.gameObject.SetActive(false);
        }


        if (isCompleted == true)
            DestroyRequest(true, false);

    }

    private void CreateRequirements()
    {
        for (int i = 0; i < requirementCols.Length; i++)
        {
            int colorInt = Mathf.FloorToInt(Random.Range(0, 4));
            if (colorInt == 0)
            {
                requirementCols[i].color = Color.blue;
                requirements[i] = Resource.ResourceType.blue;
            }
            else if (colorInt == 1)
            {
                requirementCols[i].color = Color.red;
                requirements[i] = Resource.ResourceType.red;
            }
            else if (colorInt == 2)
            {
                requirementCols[i].color = Color.yellow;
                requirements[i] = Resource.ResourceType.yellow;
            }
            else if (colorInt == 3)
            {
                requirementCols[i].color = Color.green;
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
            reputationGain = Random.Range(0, 2);
        if (rand <= .9f)
            reputationGain = Random.Range(3, 4);
        else
            reputationGain = Random.Range(5, 6);

        //hardcoded probabilities for reputationLoss
        rand = Random.value;
        if (rand <= .6f)
            reputationLoss = Random.Range(1, 3);
        if (rand <= .9f)
            reputationLoss = Random.Range(4, 5);
        else
            reputationLoss = Random.Range(6, 7);

        propertiesText.text = "$: " + payAmount + " RepGain: " + reputationGain + " RepLoss: " + reputationLoss + " Time: " + completeTime;
    }


    // Drag & Drop, Update request status, Assess completion, Destroy

    // TO DO: create UpdateRequirements method that has both OnDrop and OnPickup contents, those only call that method with certain property

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
                        hasRequirements = true;
                    return;
                }
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

    public void DestroyRequest(bool completed, bool declined)
    {
        Resource[] r = cardHolder.GetComponentsInChildren<Resource>();

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
        else if (completed == false && declined == false)
        {
            Debug.Log("Request failed");
            tracker.UpdateReputation(-reputationLoss);
        }
        else if (declined == true)
            tracker.UpdateReputation((-reputationLoss) / 2);

        Destroy(this.gameObject);
    }
}

