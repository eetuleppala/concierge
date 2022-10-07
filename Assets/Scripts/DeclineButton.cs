using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeclineButton : MonoBehaviour, IPointerClickHandler
{
    public Request r;
    private void Start()
    {
        r = GetComponentInParent<Request>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        r.DestroyRequest(false, true);
    }
}
