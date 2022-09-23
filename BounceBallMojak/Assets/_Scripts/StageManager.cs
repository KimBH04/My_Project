using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject gameManager;
    private int stars;

    private void Awake()
    {
        stars = GameObject.FindGameObjectsWithTag("Star").Length;
    }

    private void FixedUpdate()
    {
    }

    public void StarCount()
    {
        if (--stars == 0)
        {
            gameManager.GetComponent<GameManager>().StagePass();
        }
    }
}
