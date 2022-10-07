using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResourceRandomizer : MonoBehaviour, IPointerClickHandler
{
    private Resource[] resources;

    public ProgressTracker tracker;
    public GameObject playerHand;

    public void OnPointerClick(PointerEventData eventData)
    {
        resources = playerHand.transform.GetComponentsInChildren<Resource>();
        Debug.Log(resources.Length);
        Debug.Log(name);
        for (int i = 0; i < resources.Length; i++)
        {
            resources[i].RandomizeColor();
        }
        tracker.UpdateMoney(-5);
    }
}
