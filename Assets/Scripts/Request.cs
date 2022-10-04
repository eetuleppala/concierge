using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Request : MonoBehaviour
{

    private Color[] colors = new Color[3];
    public GameObject[] images = new GameObject[3];

    private void Start()
    {
        CreateColorArray();
        AssignColors();
    }

    private void CreateColorArray()
    {
        for (int i = 0; i < 3; i++)
        {
            int colorInt = Mathf.FloorToInt(Random.Range(0, 4));
            if (colorInt == 0)
            {
                colors[i] = Color.blue;
            }
            else if (colorInt == 1)
            {
                colors[i] = Color.red;
            }
            else if (colorInt == 2)
            {
                colors[i] = Color.yellow;
            }
            else if (colorInt == 3)
            {
                colors[i] = Color.green;
            }
            else
            {
                Debug.Log("Error choosing color");
            }
        }
    }

    private void AssignColors()
    {
        for (int i = 0; i < 3; i++)
        {
            images[i].GetComponent<Image>().color = colors[i];
        }
    }
}
