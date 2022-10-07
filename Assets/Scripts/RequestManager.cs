using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestManager : MonoBehaviour
{
    public GameObject requestHand;
    public GameObject requestCard;

    [SerializeField] private float minSpawnInterval = 10.0f;
    [SerializeField] private float maxSpawnInterval = 40.0f;
    private float spawnInterval = 10.0f;
    private float spawnTimer = 0;

    private void Start()
    {
        SetSpawnInterval();
    }

    private void Update()
    {
        //Debug.Log(spawnInterval);
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnRequest();
            SetSpawnInterval();
            spawnTimer = 0;
        }
    }

    private void SpawnRequest()
    {
        Debug.Log("Spawning");
        GameObject newCard = Instantiate(requestCard);
        newCard.transform.SetParent(requestHand.transform);
    }

    private void SetSpawnInterval()
    {
        spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

}
